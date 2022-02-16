using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
using System.Text.RegularExpressions;

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
    private int x;

    private StateType gameState = StateType.DEFAULT;
    private float remainingTime = 0;
    private string shape = "square";
    private string file_path = "IMUCommsTxt.txt";
    private string[] sequence = new string[] {"L","R","U","L","D","R","U","L","D","R","L","D","U","R","L"};
    private int step_num = 0;

    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject Objective;
    public GameObject MainMenuButton;
    public Text timeText;

    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject upArrow;
    public GameObject downArrow;

    public GameObject FullCake;
    public GameObject cakeSlice1;
    public GameObject cakeSlice2;
    public GameObject cakeSlice3;
    public GameObject cakeSlice4;

    Rigidbody c1_Rigidbody;
    Rigidbody c2_Rigidbody;
    Rigidbody c3_Rigidbody;
    Rigidbody c4_Rigidbody;

    public GameObject darkFirst;
    public GameObject darkSecond;
    public GameObject darkThird;
    public GameObject darkFourth;

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

        timeText.text = "Timer: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Start is called before the first frame update
    void Start()
    {
        cakeSlice1.SetActive(false);
        cakeSlice2.SetActive(false);
        cakeSlice3.SetActive(false);
        cakeSlice4.SetActive(false);

        darkFirst.SetActive(false);
        darkSecond.SetActive(false);;
        darkThird.SetActive(false);;
        darkFourth.SetActive(false);;

        gameState = StateType.PLAYING;
        remainingTime = timeToComplete;

        //FirstMotion.SetActive(true);
        //SecondMotion.SetActive(true);
        //ThirdMotion.SetActive(true);
        //FourthMotion.SetActive(true);
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

        x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        x++;

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

                //CAKE ANIMATION AFTER CHOPPING IS COMPLETED
                FullCake.SetActive(false);
                cakeSlice1.SetActive(true);
                cakeSlice2.SetActive(true);
                cakeSlice3.SetActive(true);
                cakeSlice4.SetActive(true);

                c1_Rigidbody = cakeSlice1.GetComponent<Rigidbody>();
                c2_Rigidbody = cakeSlice2.GetComponent<Rigidbody>();
                c3_Rigidbody = cakeSlice3.GetComponent<Rigidbody>();
                c4_Rigidbody = cakeSlice4.GetComponent<Rigidbody>();

                c1_Rigidbody.constraints = RigidbodyConstraints.None;
                c2_Rigidbody.constraints = RigidbodyConstraints.None;
                c3_Rigidbody.constraints = RigidbodyConstraints.None;
                c4_Rigidbody.constraints = RigidbodyConstraints.None;

                cakeSlice2.SetActive(true);
                cakeSlice3.SetActive(true);
                cakeSlice4.SetActive(true);


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
                downArrow.SetActive(false);
                upArrow.SetActive(false);
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);
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

            if (x % 2 == 0)
            {
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

                        step_num = Int32.Parse(Regex.Match(line, @"\d+").Value);
                        Debug.Log(step_num);

                        if (step_num == sequence.Length) {
                            rightArrow.SetActive(false);
                            leftArrow.SetActive(false);
                            upArrow.SetActive(false);
                            downArrow.SetActive(false);
                            gameState = StateType.WIN;
                        }
                        else {
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
                                gameState = StateType.DEFAULT;
                            }
                        }

                        /*
                        if (line.Contains("1")) {
                            FirstMotion.SetActive(false);
                            darkFirst.SetActive(false);
                            darkSecond.SetActive(true);
                        } else if (line.Contains("2")) {
                            FirstMotion.SetActive(false);
                            SecondMotion.SetActive(false);
                            darkFirst.SetActive(false);
                            darkSecond.SetActive(false);
                            darkThird.SetActive(true);
                        } else if (line.Contains("3")) {
                            FirstMotion.SetActive(false);
                            SecondMotion.SetActive(false);
                            ThirdMotion.SetActive(false);
                            darkFirst.SetActive(false);
                            darkSecond.SetActive(false);
                            darkThird.SetActive(false);
                            darkFourth.SetActive(true);
                        } else if (line.Contains("4")) {
                            FirstMotion.SetActive(false);
                            SecondMotion.SetActive(false);
                            ThirdMotion.SetActive(false);
                            FourthMotion.SetActive(false);
                            darkFirst.SetActive(false);
                            darkSecond.SetActive(false);
                            darkThird.SetActive(false);
                            darkFourth.SetActive(false);
                            gameState = StateType.WIN;
                        } */
                    }
                } catch (Exception e) {
                    Debug.Log(e);
                }
            }
        }
    }
}