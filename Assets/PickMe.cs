using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
using TMPro;
//using GoogleCloudStreamingSpeechToText;
//using GoogleCloudStreamingSpeechToText;
using CursorObject;

public class PickMe : MonoBehaviour
{
    private int timeToComplete = 90;
    private float remainingTime = 0;

    //string transcriptRec;
    public bool positioner = false;

    public GameObject cursor;
    //public GameObject speechObj;
    //public StreamingRecognizer streamRec;
    public fs_read_hands_csv handReader;
    //public fs_read_hands_csv handReader;
    //public read_hands_tcp handReader;
    public GameObject bread;
    public GameObject carrot;
    public GameObject apple;
    public GameObject lemon;
    public GameObject banana;
    public GameObject tomato;
    public GameObject watermelon;
    public GameObject grape;
    public GameObject mushroom;
    //public GameObject include;
    //public GameObject noInclude;

    public HighScores scoreboard;

    public ScoreCounter counter;

	public TMP_Text yourScore;
	
    bool breadFound = false;
    bool carrotFound = false;
    bool appleFound = false;
    bool lemonFound = false;
    bool bananaFound = false;
    bool tomatoFound = false;
    bool watermelonFound = false;
    bool grapeFound = false;
    bool mushroomFound = false;
    public Text timeText;

    bool gameOver = false;

    int numFoods = 0;
	public GameObject leaderboard; // the visual leaderboard to show or hide
	public PlayFabManager1 pfManager; // the playfab interface

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = "Timer: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Test");
        remainingTime = timeToComplete;
        //counter.IncreaseScore(5000);
        //counter.UpdateScoreDisplay();
        //counter.IncreaseScore(5000);
        //entry1.SetActive(true);
        leaderboard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //done.SetActive(true);
        timeText.enabled = true;
        Transform cursy = handReader.Cursor;


        //transcriptRec = streamRec.transcript;
        //Debug.Log(transcriptRec);


        // TODO: NEED A WAY TO SAY THAT YOU ARE INCLUDING FOOD FROM RECIPE

        //if (((cursy.position.x < done.transform.position.x + 1) && (cursy.position.x > done.transform.position.x - 1)) && gameOver == false)
        if (numFoods == 3 && gameOver == false)
        {
            //over.SetActive(true);
            Debug.Log("Done");
            // Send to high score
            // Make final score big on the screen or something
            int timeBonus = (int)Math.Round(remainingTime);
            timeBonus = timeBonus * 100;
            counter.IncreaseScore(timeBonus);

            //scoreboard.AddNewScore("Player1", counter.score);

            Debug.Log("Game over, added all food");
            cursor.SetActive(false);
            //entry1.SetActive(true);
            //leaderboard.SetActive(true);
            timeText.enabled = false;
            gameOver = true;
            remainingTime = 0;
			leaderboard.SetActive(true);
			yourScore.text = Convert.ToString(counter.score);
			pfManager.SendLeaderboard(counter.score);
			System.Threading.Thread.Sleep(2000);
			pfManager.GetLeaderboard();
        }
        if(gameOver == false)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                DisplayTime(remainingTime);
            }
            else if (remainingTime <= 0)
            {
                remainingTime = 0;
                gameOver = true;
				Debug.Log("Game over, no time");
				leaderboard.SetActive(true);
				yourScore.text = Convert.ToString(counter.score);
				pfManager.SendLeaderboard(counter.score);
				System.Threading.Thread.Sleep(2000);
				pfManager.GetLeaderboard();
            }

            if (((cursy.position.x < bread.transform.position.x + 1) && (cursy.position.x > bread.transform.position.x - 1))
                   && ((cursy.position.y < bread.transform.position.y + 1) && (cursy.position.y > bread.transform.position.y - 1)))
            {
                if (breadFound == false)
                {
                    Debug.Log("Touching bread");
                    //include.SetActive(true);
                    //if (transcriptRec.Contains("yes") && breadFound == false)
                    //{
                    counter.IncreaseScore(-500);
                    breadFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    bread.SetActive(false);
                    Debug.Log("Added bread");
                    numFoods++;
                }
                //}
                /*
                else if (transcriptRec.Contains("no") && breadFound == false)
                {
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    breadFound = true;
                    Debug.Log(counter.score);
                    bread.SetActive(false);
                    Debug.Log("Did not add bread");
                    numFoods++;
                }*/
            }

            if (((cursy.position.x < carrot.transform.position.x + 1) && (cursy.position.x > carrot.transform.position.x - 1))
                   && ((cursy.position.y < carrot.transform.position.y + 1) && (cursy.position.y > carrot.transform.position.y - 1)))
            {
                if (carrotFound == false)
                {
                    Debug.Log("Touching carrot");
                    //include.SetActive(true);
                    //if (transcriptRec.Contains("yes") && carrotFound == false)
                    //{
                    counter.IncreaseScore(-500);
                    carrotFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    carrot.SetActive(false);
                    Debug.Log("Added carrot");
                    numFoods++;
                }

                //}
                /*else if (transcriptRec.Contains("no") && carrotFound == false)
                {
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    carrotFound = true;
                    Debug.Log(counter.score);
                    carrot.SetActive(false);
                    Debug.Log("Did not add carrot");
                    numFoods++;
                }*/
            }

            if (((cursy.position.x < apple.transform.position.x + 1) && (cursy.position.x > apple.transform.position.x - 1))
                   && ((cursy.position.y < apple.transform.position.y + 1) && (cursy.position.y > apple.transform.position.y - 1)))
            {
                if (appleFound == false)
                {
                    Debug.Log("Touching apple");
                    //include.SetActive(true);
                    //if (transcriptRec.Contains("yes") && appleFound == false)
                    //{
                    counter.IncreaseScore(1000);
                    appleFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    apple.SetActive(false);
                    Debug.Log("Added apple");
                    numFoods++;
                }

                //}
                /*else if (transcriptRec.Contains("no") && appleFound == false)
                {
                    counter.IncreaseScore(-500);
                    counter.UpdateScoreDisplay();
                    appleFound = true;
                    Debug.Log(counter.score);
                    apple.SetActive(false);
                    Debug.Log("Did not add apple");
                    numFoods++;
                }*/
            }

            if (((cursy.position.x < lemon.transform.position.x + 1) && (cursy.position.x > lemon.transform.position.x - 1))
                   && ((cursy.position.y < lemon.transform.position.y + 1) && (cursy.position.y > lemon.transform.position.y - 1)))
            {
                if (lemonFound == false)
                {
                    Debug.Log("Touching lemon");
                    //include.SetActive(true);
                    //if (transcriptRec.Contains("yes") && lemonFound == false)
                    //{
                    counter.IncreaseScore(-500);
                    lemonFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    lemon.SetActive(false);
                    Debug.Log("Added lemon");
                    numFoods++;
                }

                //
                /*else if (transcriptRec.Contains("no") && lemonFound == false)
                {
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    lemonFound = true;
                    Debug.Log(counter.score);
                    lemon.SetActive(false);
                    Debug.Log("Did not add lemon");
                    numFoods++;
                }*/
            }

            if (((cursy.position.x < banana.transform.position.x + 1) && (cursy.position.x > banana.transform.position.x - 1))
                   && ((cursy.position.y < banana.transform.position.y + 1) && (cursy.position.y > banana.transform.position.y - 1)))
            {
                if (bananaFound == false)
                {
                    Debug.Log("Touching banana");
                    //include.SetActive(true);
                    //if (transcriptRec.Contains("yes") && bananaFound == false)
                    //{
                    counter.IncreaseScore(1000);
                    bananaFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    banana.SetActive(false);
                    Debug.Log("Added banana");
                    numFoods++;
                }

                //}
                /*else if (transcriptRec.Contains("no") && bananaFound == false)
                {
                    counter.IncreaseScore(-500);
                    counter.UpdateScoreDisplay();
                    bananaFound = true;
                    Debug.Log(counter.score);
                    banana.SetActive(false);
                    Debug.Log("Did not add banana");
                    numFoods++;
                }*/
            }

            if (((cursy.position.x < tomato.transform.position.x + 1) && (cursy.position.x > tomato.transform.position.x - 1))
                   && ((cursy.position.y < tomato.transform.position.y + 1) && (cursy.position.y > tomato.transform.position.y - 1)))
            {
                if (tomatoFound == false)
                {
                    Debug.Log("Touching tomato");
                    //include.SetActive(true);
                    //if (transcriptRec.Contains("yes") && tomatoFound == false)
                    //{
                    counter.IncreaseScore(-500);
                    tomatoFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    tomato.SetActive(false);
                    Debug.Log("Added tomato");
                    numFoods++;
                }
                //}
                /*else if (transcriptRec.Contains("no") && tomatoFound == false)
                {
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    tomatoFound = true;
                    Debug.Log(counter.score);
                    tomato.SetActive(false);
                    Debug.Log("Did not add tomato");
                    numFoods++;
                }*/
            }

            if (((cursy.position.x < watermelon.transform.position.x + 1) && (cursy.position.x > watermelon.transform.position.x - 1))
                   && ((cursy.position.y < watermelon.transform.position.y + 1) && (cursy.position.y > watermelon.transform.position.y - 1)))
            {
                if (watermelonFound == false)
                {
                    Debug.Log("Touching watermelon");
                    //include.SetActive(true);
                    //if (transcriptRec.Contains("yes") && watermelonFound == false)
                    //{
                    counter.IncreaseScore(1000);
                    watermelonFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    watermelon.SetActive(false);
                    Debug.Log("Added watermelon");
                    numFoods++;
                }

                //}
                /*else if (transcriptRec.Contains("no") && watermelonFound == false)
                {
                    counter.IncreaseScore(-500);
                    counter.UpdateScoreDisplay();
                    watermelonFound = true;
                    Debug.Log(counter.score);
                    watermelon.SetActive(false);
                    Debug.Log("Did not add watermelon");
                    numFoods++;
                }*/
            }

            if (((cursy.position.x < grape.transform.position.x + 1) && (cursy.position.x > grape.transform.position.x - 1))
                   && ((cursy.position.y < grape.transform.position.y + 1) && (cursy.position.y > grape.transform.position.y - 1)))
            {
                if (grapeFound == false) {
                    Debug.Log("Touching grape");
                    //include.SetActive(true);
                    //if (transcriptRec.Contains("yes") && grapeFound == false)
                    //{
                    counter.IncreaseScore(-500);
                    grapeFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    grape.SetActive(false);
                    Debug.Log("Added grape");
                    numFoods++; }
                //}
                /*else if (transcriptRec.Contains("no") && grapeFound == false)
                {
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    grapeFound = true;
                    Debug.Log(counter.score);
                    grape.SetActive(false);
                    Debug.Log("Did not add grape");
                    numFoods++;
                }*/
            }

            if (((cursy.position.x < mushroom.transform.position.x + 1) && (cursy.position.x > mushroom.transform.position.x - 1))
                   && ((cursy.position.y < mushroom.transform.position.y + 1) && (cursy.position.y > mushroom.transform.position.y - 1)))
            {
                if (mushroomFound == false)
                {
                    Debug.Log("Touching mushroom");
                    //include.SetActive(true);
                    //if (transcriptRec.Contains("yes") && mushroomFound == false)
                    //{
                    counter.IncreaseScore(-500);
                    mushroomFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    mushroom.SetActive(false);
                    Debug.Log("Added mushroom");
                    numFoods++;
                }
                //}
                /*else if (transcriptRec.Contains("no") && mushroomFound == false)
                {
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    mushroomFound = true;
                    Debug.Log(counter.score);
                    mushroom.SetActive(false);
                    Debug.Log("Did not add mushroom");
                    numFoods++;
                }*/
            }
        }
    }
}