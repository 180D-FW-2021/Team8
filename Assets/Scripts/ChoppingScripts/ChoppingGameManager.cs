using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
using System.Text.RegularExpressions;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;

public enum StateType
{
    DEFAULT,    // Fall-back state, should never happen
    PLAYING,    // Player actively tracing shape
    PAUSING,    // Player pausing game
    WIN,        // Player has won the game
    LOSE        // Player has lost the game
}

public class ChoppingGameManager : MonoBehaviour
{
    private int timeToComplete = 40;

    private StateType gameState = StateType.DEFAULT;
    private float remainingTime = 0;
    private string shape = "square";
    private string file_path = "IMUCommsTxt.txt";
    private string[] sequence = new string[] {"L","R","U","L","D","R","U","L","D","R","L","D","U","R","L"};
    private string[] fakeSequence = new string[] {"U","U","U","U","U","U","U","U","U","U","U","U","U","U","U"};
    private int step_num = 0;

    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject Objective;
    public GameObject MainMenuButton;
    public Text timeText;
    public Text scoreText;

    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject upArrow;
    public GameObject downArrow;

    public GameObject fakeRight;
    public GameObject fakeLeft;
    public GameObject fakeUp;
    public GameObject fakeDown;

    private System.Random rnd;
    private MqttClient client;
    private string username = "com";
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {

        fakeRight.SetActive(false);
        fakeUp.SetActive(false);
        fakeLeft.SetActive(false);
        fakeDown.SetActive(false);

        gameState = StateType.PLAYING;
        remainingTime = timeToComplete;

        MainMenuButton.SetActive(false);

        rnd = new System.Random();

        // Setting up text file
        shape = "square";
        string[] lines = {shape, "False", "False", "0"};

        username = PlayerPrefs.GetString("Username");
        Debug.Log("mqtt " + username);

        client = new MqttClient("test.mosquitto.org");
        client.MqttMsgPublished += client_MqttMsgPublished;
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        string clientId = Guid.NewGuid().ToString();

        client.Connect(clientId);
        client.Subscribe(new string[] { "ece180d/team8/imu" + username },
            new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });


        randomizeFakeArrowSequence();
        client.Publish("ece180d/team8/unity", Encoding.UTF8.GetBytes("start"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);

    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case StateType.PLAYING:
                Objective.SetActive(true);
                WinScreen.SetActive(false);
                LoseScreen.SetActive(false);
                timeText.enabled = true;
                scoreText.enabled = false;
                break;
                
            case StateType.PAUSING:
                break;

            case StateType.WIN:
                Objective.SetActive(false);
                WinScreen.SetActive(true);
                LoseScreen.SetActive(false);

                downArrow.SetActive(false);
                upArrow.SetActive(false);
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);

                fakeRight.SetActive(false);
                fakeUp.SetActive(false);
                fakeLeft.SetActive(false);
                fakeDown.SetActive(false);

                score = (int) (remainingTime * 150f + 5f * 15f);
                scoreText.enabled = true;
                scoreText.text = "Score: " + score;

                MainMenuButton.SetActive(true);
                timeText.enabled = false;
                string[] lines = {"N/A", "False", "True", "0"};
                break;

            case StateType.LOSE:
                Objective.SetActive(false);
                WinScreen.SetActive(false);
                LoseScreen.SetActive(true);

                downArrow.SetActive(false);
                upArrow.SetActive(false);
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);

                fakeRight.SetActive(false);
                fakeUp.SetActive(false);
                fakeLeft.SetActive(false);
                fakeDown.SetActive(false);

                score = (sequence.Length - step_num) * 5;
                scoreText.enabled = true;
                scoreText.text = "Score: " + score;

                MainMenuButton.SetActive(true);
                timeText.enabled = false;
                break;

            default:
                Debug.Log("ERROR: Unknown game state");
                break;
        }

        if (getState() == StateType.PLAYING)
        {
            if (remainingTime > 0 && getState() != StateType.PAUSING) {
                remainingTime -= Time.deltaTime;
                DisplayTime(remainingTime);
            }
            else if (remainingTime <= 0 && getState() != StateType.LOSE) {
                remainingTime = 0;
                gameState = StateType.LOSE;

                client.Publish("ece180d/team8/unity", Encoding.UTF8.GetBytes("stop"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
            }

            if (step_num > sequence.Length) {
                client.Publish("ece180d/team8/unity", Encoding.UTF8.GetBytes("stop"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                gameState = StateType.LOSE;
            }


            if (step_num == sequence.Length) {
                rightArrow.SetActive(false);
                leftArrow.SetActive(false);
                upArrow.SetActive(false);
                downArrow.SetActive(false);
                client.Publish("ece180d/team8/unity", Encoding.UTF8.GetBytes("stop"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                gameState = StateType.WIN;
            }
            else {
                // Display the correct arrow
                if (sequence[step_num] == "R") {
                    rightArrow.SetActive(true);
                    leftArrow.SetActive(false);
                    upArrow.SetActive(false);
                    downArrow.SetActive(false);
                } else if (sequence[step_num] == "L") {
                    rightArrow.SetActive(false);
                    leftArrow.SetActive(true);
                    upArrow.SetActive(false);
                    downArrow.SetActive(false);
                } else if (sequence[step_num] == "U") {
                    rightArrow.SetActive(false);
                    leftArrow.SetActive(false);
                    upArrow.SetActive(true);
                    downArrow.SetActive(false);
                } else if (sequence[step_num] == "D") {
                    rightArrow.SetActive(false);
                    leftArrow.SetActive(false);
                    upArrow.SetActive(false);
                    downArrow.SetActive(true);
                } else {
                    client.Publish("ece180d/team8/unity", Encoding.UTF8.GetBytes("stop"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                    gameState = StateType.DEFAULT;
                }

                // Display the dummy arrow
                if (fakeSequence[step_num] == "R") {
                    fakeRight.SetActive(true);
                    fakeLeft.SetActive(false);
                    fakeUp.SetActive(false);
                    fakeDown.SetActive(false);
                } else if (fakeSequence[step_num] == "L") {
                    fakeRight.SetActive(false);
                    fakeLeft.SetActive(true);
                    fakeUp.SetActive(false);
                    fakeDown.SetActive(false);
                } else if (fakeSequence[step_num] == "U") {
                    fakeRight.SetActive(false);
                    fakeLeft.SetActive(false);
                    fakeUp.SetActive(true);
                    fakeDown.SetActive(false);
                } else if (fakeSequence[step_num] == "D") {
                    fakeRight.SetActive(false);
                    fakeLeft.SetActive(false);
                    fakeUp.SetActive(false);
                    fakeDown.SetActive(true);
                } else {
                    client.Publish("ece180d/team8/unity", Encoding.UTF8.GetBytes("stop"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                    gameState = StateType.DEFAULT;
                }
            }
        }
    }

    // Helper Functions

    public StateType getState()
    {
        return gameState;
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = "Timer: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void randomizeFakeArrowSequence()
    {
        string[] directions = {"U","L","D","R"};
        for(int i = 0; i < sequence.Length; i++)
        {
            do {
                fakeSequence[i] = directions[rnd.Next(0,3)];
            } while(fakeSequence[i] == sequence[i]);
        }
    }


    // MQTT Functions

    void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
    {
        Debug.Log("Subscribed for id = " + e.MessageId);
    }

    void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
    {
        Debug.Log("inside client_MqttMsgPublished");
        Debug.Log("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
        Debug.Log("MessageId = " + e.MessageId);
    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        //this function is called everytime you receive message
        //e.Message is a byte[]
        var str = System.Text.Encoding.UTF8.GetString(e.Message);

        //Debug.Log("received a message");

        if (String.Equals(e.Topic, "ece180d/team8/imu" + username))
        {
            if (str == "X"){
                gameState = StateType.WIN;
            } else {
                step_num = Convert.ToInt32(str);
            }
        }
    }

    void OnDestroy()
    {
        client.Publish("ece180d/team8/unity", Encoding.UTF8.GetBytes("stop"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
    }
}