using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;

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

    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject Objective;
    public GameObject MainMenuButton;
    public Text timeText;

    public GameObject FirstMotion;
    public GameObject SecondMotion;
    public GameObject ThirdMotion;
    public GameObject FourthMotion;

    public void Pause(bool paused)
    {
        if(paused) {
            gameState = StateType.PAUSING;
        } else {
            gameState = StateType.PLAYING;
        }
    }

    public StateType getState()
    {
        return gameState;
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = StateType.PLAYING;
        remainingTime = timeToComplete;

        FirstMotion.SetActive(true);
        SecondMotion.SetActive(true);
        ThirdMotion.SetActive(true);
        FourthMotion.SetActive(true);
        MainMenuButton.SetActive(false);
        // Setting up text file
        shape = "square";
        string[] lines = {shape, "False", "False", "0"};
        for (int i = 0; i < 100; i++) {
            try {
                using (StreamWriter sw = new StreamWriter(new FileStream("Assets/" + file_path, FileMode.OpenOrCreate, FileAccess.Write))) {
                    sw.WriteLine(lines[0]);
                    sw.WriteLine(lines[1]);
                    sw.WriteLine(lines[2]);
                    sw.WriteLine(lines[3]);
                }
                return;
            } catch (Exception e) {
                Debug.Log(e);
                System.Threading.Thread.Sleep(250);
            }
        }
        Debug.Log("Could not write shape");
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
                break;
            case StateType.PAUSING:
                break;
            case StateType.WIN:
                Objective.SetActive(false);
                WinScreen.SetActive(true);
                LoseScreen.SetActive(false);
                MainMenuButton.SetActive(true);
                timeText.enabled = false;
                string[] lines = {"N/A", "False", "True", "0"};

                try {
                    using (StreamWriter sw = new StreamWriter(new FileStream("Assets/" + file_path, FileMode.OpenOrCreate, FileAccess.Write))) {
                        sw.WriteLine(lines[0]);
                        sw.WriteLine(lines[1]);
                        sw.WriteLine(lines[2]);
                        sw.WriteLine(lines[3]);
                    }
                } catch (Exception e) {
                    Debug.Log(e);
                }
                break;
            case StateType.LOSE:
                Objective.SetActive(false);
                WinScreen.SetActive(false);
                LoseScreen.SetActive(true);
                FirstMotion.SetActive(false);
                SecondMotion.SetActive(false);
                ThirdMotion.SetActive(false);
                FourthMotion.SetActive(false);
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
                string[] lines = {"N/A", "False", "True", "0"};

                try {
                    using (StreamWriter sw = new StreamWriter(new FileStream("Assets/" + file_path, FileMode.OpenOrCreate, FileAccess.Write))) {
                        sw.WriteLine(lines[0]);
                        sw.WriteLine(lines[1]);
                        sw.WriteLine(lines[2]);
                        sw.WriteLine(lines[3]);
                    }
                } catch (Exception e) {
                    Debug.Log(e);
                }
            }

            try {
                using (StreamReader sr = new StreamReader(new FileStream("Assets/" + file_path, FileMode.OpenOrCreate, FileAccess.Read))) {
                    Debug.Log("Reading from text file");
                    sr.ReadLine();
                    string line = sr.ReadLine();
                    if (line.Contains("True")) {
                        gameState = StateType.WIN;
                    }
                    sr.ReadLine();
                    line = sr.ReadLine();
                    if (line.Contains("1")) {
                        FirstMotion.SetActive(false);
                    } else if (line.Contains("2")) {
                        SecondMotion.SetActive(false);
                    } else if (line.Contains("3")) {
                        ThirdMotion.SetActive(false);
                    } else if (line.Contains("4")) {
                        FourthMotion.SetActive(false);
                        gameState = StateType.WIN;
                    }
                }
            } catch (Exception e) {
                Debug.Log(e);
            }
        }
    }
}