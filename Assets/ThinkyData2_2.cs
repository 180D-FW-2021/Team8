using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
using TMPro;

public class ThinkyData2_2 : MonoBehaviour
{
    private int timeToComplete = 11;
    private float remainingTime = 0;

    string transcriptRec;
    public GameObject speechObj;
    public StreamingRecognizer streamRec;
    // public HighScores scoreboard;

    // public ScoreCounter counter;

    // public GameObject leaderboard;
    public ScoreCounter counter;

    public Text timeText;
    public GameObject recipe;
    public GameObject task1;
    public GameObject task2;
    public GameObject task3;
    public GameObject task4;
    //public GameObject food5;
    public GameObject gameOver;
    public GameObject winner;
    public GameObject loser;
    public GameObject eatIt;

    public GameObject knife;
    public GameObject roll;
    public GameObject pan;
    public GameObject cutter;

    bool task1Picked = false;
    bool task2Picked = false;
    bool task3Picked = false;
    bool task4Picked = false;
    //bool food5Picked = false;
    bool timeUp = false;
    string transcriptRecOld = "";
    bool gameStarted = false;
    int numPicked = 0;

    public GameObject leaderboard;
    public PlayFabManager1 scoreScript;
    public TMP_Text score_text;

    bool finished = false;

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = "Timer: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Start is called before the first frame update
    void Start()
    {
        remainingTime = timeToComplete;
        transcriptRec = "";
    }

    // Update is called once per frame
    void Update()
    {
        timeText.enabled = true;

        // transcript originally assigned here

        if (transcriptRec != "" && transcriptRec != transcriptRecOld)
        {
            Debug.Log(transcriptRec);
            transcriptRecOld = transcriptRec;
        }

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            DisplayTime(remainingTime);
        }
        else if (remainingTime <= 0)
        {
            if (gameStarted == false)
            {
                remainingTime = 21;
                timeToComplete = 21;
                gameStarted = true;
            }
            else
            {
                remainingTime = 0;
                gameOver.SetActive(true);
                if (numPicked == 4)
                {
                    winner.SetActive(true);
                }
                else
                {
                    loser.SetActive(true);
                }
            }
        }

        if (remainingTime <= timeToComplete && gameStarted == true)
        {
            speechObj.SetActive(true);
            transcriptRec = streamRec.transcript;
            recipe.SetActive(false);
            if (task1Picked == false)
                task1.SetActive(false);
            if (task2Picked == false)
                task2.SetActive(false);
            if (task4Picked == false)
                task4.SetActive(false);
            if (task3Picked == false)
                task3.SetActive(false);
            //if (food5Picked == false)
            //   food5.SetActive(false);
            if ((transcriptRec.Contains("Chop food") || transcriptRec.Contains("chop food") || transcriptRec.Contains("top food") || transcriptRec.Contains("Chopped food")) && task1Picked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                task1Picked = true;
                task1.SetActive(true);
                numPicked++;
                knife.SetActive(true);
            }

            if ((transcriptRec.Contains("roll dough") || transcriptRec.Contains("rolled up") || transcriptRec.Contains("roll the") || transcriptRec.Contains("Waldo")) && task2Picked == false && task1Picked == true)
            {
                knife.SetActive(false);
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                task2Picked = true;
                task2.SetActive(true);
                numPicked++;
                roll.SetActive(true);
            }

            if ((transcriptRec.Contains("combine") || transcriptRec.Contains("come") || transcriptRec.Contains("come by")) && task4Picked == false && task1Picked == true && task2Picked == true)
            {
                roll.SetActive(false);
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                task4Picked = true;
                task4.SetActive(true);
                numPicked++;
                pan.SetActive(true);
            }

            if ((transcriptRec.Contains("baked") || transcriptRec.Contains("fake") || transcriptRec.Contains("bake") || transcriptRec.Contains("make") || transcriptRec.Contains("faked")) && task3Picked == false && task1Picked == true && task2Picked == true && task4Picked == true)
            {
                pan.SetActive(false);
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                task3Picked = true;
                task3.SetActive(true);
                numPicked++;
                cutter.SetActive(true);
            }
            /*
            if ((transcriptRec.Contains("Tomato") || transcriptRec.Contains("tomato")) && food5Picked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                //food5Picked = true;
                //food5.SetActive(true);
                numPicked++;
            }*/
        }


        if (numPicked == 4 && finished == false)
        {
            gameOver.SetActive(true);
            winner.SetActive(true);
            timeText.enabled = false;
            recipe.SetActive(true);
            eatIt.SetActive(true);

            leaderboard.SetActive(true);
            score_text.text = Convert.ToString(counter.score);
            scoreScript.SendLeaderboard(counter.score);
            System.Threading.Thread.Sleep(2000);
            scoreScript.GetLeaderboard();
            finished = true;
            //playfabManager.SendLeaderboard(counter.score);

        }
        if (remainingTime <= 0 && gameStarted == true && finished == false)
        {

            recipe.SetActive(true);
            timeText.enabled = false;
            if (task1Picked == false) // transcriptRec != ""
            {
                counter.IncreaseScore(-200);
                task1.SetActive(true);
                task1Picked = true;
            }
            if (task2Picked == false)
            {
                counter.IncreaseScore(-200);
                task2.SetActive(true);
                task2Picked = true;
            }
            if (task4Picked == false)
            {
                counter.IncreaseScore(-200);
                task4.SetActive(true);
                task4Picked = true;
            }
            if (task3Picked == false)
            {
                counter.IncreaseScore(-200);
                task3.SetActive(true);
                task3Picked = true;
            }
            leaderboard.SetActive(true);
            score_text.text = Convert.ToString(counter.score);
            scoreScript.SendLeaderboard(counter.score);
            System.Threading.Thread.Sleep(2000);
            scoreScript.GetLeaderboard();
            finished = true;
            /*if (food5Picked == false)
            {
                counter.IncreaseScore(-200);
                food5.SetActive(true);
                food5Picked = true;
            }*/
        }
    }
}
