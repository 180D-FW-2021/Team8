using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
using PlayFab;
using PlayFab.ClientModels;
//using GoogleCloudStreamingSpeechToText;
using TMPro;

public class MemoryRead : MonoBehaviour
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
    public GameObject smoothieRecipe;
    public GameObject food1;
    public GameObject food2;
    public GameObject food3;
    public GameObject food4;
    public GameObject gameOver;
    public GameObject winner;
    public GameObject loser;

    //public PlayFabManager1 playfabManager;

    bool food1Picked = false;
    bool food2Picked = false;
    bool food3Picked = false;
    bool food4Picked = false;
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
        //playfabManager.SetActive(false);
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
                // smoothieRecipe.SetActive(true);
                gameOver.SetActive(true);
                if(numPicked == 4)
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
            smoothieRecipe.SetActive(false);
            if(food1Picked == false)
                food1.SetActive(false);
            if(food2Picked == false)
                food2.SetActive(false);
            if(food4Picked == false)
                food4.SetActive(false);
            if(food3Picked == false)
                food3.SetActive(false);
            if ((transcriptRec.Contains("Apple") || transcriptRec.Contains("apple")) && food1Picked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                food1Picked = true;
                food1.SetActive(true);
                numPicked++;
            }

            if ((transcriptRec.Contains("Banana") || transcriptRec.Contains("banana")) && food2Picked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                food2Picked = true;
                food2.SetActive(true);
                numPicked++;
            }

            if ((transcriptRec.Contains("Yogurt") || transcriptRec.Contains("yogurt")) && food4Picked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                food4Picked = true;
                food4.SetActive(true);
                numPicked++;
            }

            if ((transcriptRec.Contains("ice cream") || transcriptRec.Contains("Ice cream")) && food3Picked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                food3Picked = true;
                food3.SetActive(true);
                numPicked++;
            }
        }

        if (numPicked == 4 && finished == false)
        {

            gameOver.SetActive(true);
            winner.SetActive(true);
            timeText.enabled = false;
            smoothieRecipe.SetActive(true);

            int timeBonus = (int)Math.Round(remainingTime);
            timeBonus = timeBonus * 100;
            counter.IncreaseScore(timeBonus);

            leaderboard.SetActive(true);
            score_text.text = Convert.ToString(counter.score);
            scoreScript.SendLeaderboard(counter.score);
            System.Threading.Thread.Sleep(2000);
            scoreScript.GetLeaderboard();
            finished = true;
            //playfabManager.SetActive(true);
            //playfabManager.SendLeaderboard(counter.score);

            //playfabManager.SendLeaderboard(counter.score);
            //playfabManager.GetLeaderboard();
            //playfabManager.displayScores();
        }
        if (remainingTime <= 0 && gameStarted == true && finished == false)
        {
            timeText.enabled = false;
            if (food1Picked == false) // transcriptRec != ""
            {
                counter.IncreaseScore(-200);
                food1.SetActive(true);
                food1Picked = true;
            }
            if (food2Picked == false)
            {
                counter.IncreaseScore(-200);
                food2.SetActive(true);
                food2Picked = true;
            }
            if (food4Picked == false)
            {
                counter.IncreaseScore(-200);
                food4.SetActive(true);
                food4Picked = true;
            }
            if (food3Picked == false)
            {
                counter.IncreaseScore(-200);
                food3.SetActive(true);
                food3Picked = true;
            }
            leaderboard.SetActive(true);
            score_text.text = Convert.ToString(counter.score);
            scoreScript.SendLeaderboard(counter.score);
            System.Threading.Thread.Sleep(2000);
            scoreScript.GetLeaderboard();
            finished = true;
        }
    }


        // At the beginning of the game show the recipe card
        // Bold text for important things
        // Start off with gather the ingredients
        // 
}
