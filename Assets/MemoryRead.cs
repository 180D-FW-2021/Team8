using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
using GoogleCloudStreamingSpeechToText;

public class MemoryRead : MonoBehaviour
{
    private int timeToComplete = 30;
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
    public GameObject apple;
    public GameObject banana;
    public GameObject icecream;
    public GameObject yogurt;

    bool applePicked = false;
    bool bananaPicked = false;
    bool icecreamPicked = false;
    bool yogurtPicked = false;
    bool timeUp = false;
    string transcriptRecOld = "";

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
    }

    // Update is called once per frame
    void Update()
    {
        timeText.enabled = true;

        transcriptRec = streamRec.transcript;
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
            remainingTime = 0;
        }

        if (remainingTime <= timeToComplete - 5)
        {
            smoothieRecipe.SetActive(false);
            if(applePicked == false)
                apple.SetActive(false);
            if(bananaPicked == false)
                banana.SetActive(false);
            if(yogurtPicked == false)
                yogurt.SetActive(false);
            if(icecreamPicked == false)
                icecream.SetActive(false);
            if ((transcriptRec.Contains("Apple") || transcriptRec.Contains("apple")) && applePicked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                applePicked = true;
                apple.SetActive(true);
            }

            if ((transcriptRec.Contains("Banana") || transcriptRec.Contains("banana")) && bananaPicked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                bananaPicked = true;
                banana.SetActive(true);
            }

            if ((transcriptRec.Contains("Yogurt") || transcriptRec.Contains("yogurt")) && yogurtPicked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                yogurtPicked = true;
                yogurt.SetActive(true);
            }

            if ((transcriptRec.Contains("ice cream") || transcriptRec.Contains("Ice cream")) && icecreamPicked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                icecreamPicked = true;
                icecream.SetActive(true);
            }
        }
        /*
        if (remainingTime < timeToComplete - 5)
        {
            // if (remainingTime < timeToComplete - 20)
            //{
            if ((transcriptRec.Contains("Apple") || transcriptRec.Contains("apple")) && applePicked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                applePicked = true;
                apple.SetActive(true);
            }

            if ((transcriptRec.Contains("Banana") || transcriptRec.Contains("banana")) && bananaPicked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                bananaPicked = true;
                banana.SetActive(true);
            }

            if ((transcriptRec.Contains("Yogurt") || transcriptRec.Contains("yogurt")) && bananaPicked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                yogurtPicked = true;
                yogurt.SetActive(true);
            }

            if ((transcriptRec.Contains("ice cream") || transcriptRec.Contains("Ice cream")) && bananaPicked == false)
            {
                counter.IncreaseScore(500);
                // Debug.Log(transcriptRec);
                icecreamPicked = true;
                icecream.SetActive(true);
            }
        }
        */
            //}
        if (remainingTime <= 0)
        {
            if (applePicked == false) // transcriptRec != ""
            {
                counter.IncreaseScore(-200);
                apple.SetActive(true);
            }
            if (bananaPicked == false)
            {
                counter.IncreaseScore(-200);
                banana.SetActive(true);
            }
            if (yogurtPicked == false)
            {
                counter.IncreaseScore(-200);
                yogurt.SetActive(true);
            }
            if (icecreamPicked == false)
            {
                counter.IncreaseScore(-200);
                icecream.SetActive(true);
            }
        }
    }


        // At the beginning of the game show the recipe card
        // Bold text for important things
        // Start off with gather the ingredients
        // 
}
