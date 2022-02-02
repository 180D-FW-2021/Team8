using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
//using GoogleCloudStreamingSpeechToText;
using GoogleCloudStreamingSpeechToText;
using CursorObject;

namespace Speecher
{
    public class SpeechRec : MonoBehaviour
    {
		private int timeToComplete = 90;
		private float remainingTime = 0;
		
        string transcriptRec;
        public bool positioner = false;

        public GameObject cursor;
        public GameObject speechObj;
        public StreamingRecognizer streamRec;
        //public fs_read_hands_csv handReader;
        public fs_read_hands_csv handReader;
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
        public GameObject include;
        public GameObject noInclude;

        public HighScores scoreboard;

        public ScoreCounter counter;

        public GameObject leaderboard;       

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

        int numFoods = 0;
		
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
            //leaderboard.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
			timeText.enabled=true;
            Transform cursy = handReader.Cursor;

            transcriptRec = streamRec.transcript;
            Debug.Log(transcriptRec);
			
			if(remainingTime > 0) 
			{
				remainingTime -= Time.deltaTime;
				DisplayTime(remainingTime);
			}
			else if(remainingTime <=0)
			{
				remainingTime = 0;
			}

            if(transcriptRec == "yes")
            {
                include.SetActive(true);
                noInclude.SetActive(false);
            }
            if(transcriptRec == "no")
            {
                include.SetActive(false);
                noInclude.SetActive(true);
            }

            if (numFoods == 9)
            {
                // Send to high score
                // Make final score big on the screen or something
				int timeBonus = (int)Math.Round(remainingTime);
				timeBonus = timeBonus*100;
				counter.IncreaseScore(timeBonus);

                scoreboard.AddNewScore("Player1", counter.score);

                Debug.Log("Added all food");
                //entry1.SetActive(true);
                leaderboard.SetActive(true);
				timeText.enabled=false;

            }

            if (((cursy.position.x < bread.transform.position.x + 1) && (cursy.position.x > bread.transform.position.x - 1))
                   && ((cursy.position.y < bread.transform.position.y + 1) && (cursy.position.y > bread.transform.position.y - 1)))
            {
                Debug.Log("Touching bread");

                if (transcriptRec == "yes" && breadFound == false)
                {
                    include.SetActive(true);
                    noInclude.SetActive(false);
                    counter.IncreaseScore(-500);
                    breadFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    bread.SetActive(false);
                    Debug.Log("Added bread");
                    numFoods++;
                }
                else if (transcriptRec == "no" && breadFound == false)
                {
                    noInclude.SetActive(true);
                    include.SetActive(false);
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    breadFound = true;
                    Debug.Log(counter.score);
                    bread.SetActive(false);
                    Debug.Log("Did not add bread");
                    numFoods++;
                }
            }

            if (((cursy.position.x < carrot.transform.position.x + 1) && (cursy.position.x > carrot.transform.position.x - 1))
                   && ((cursy.position.y < carrot.transform.position.y + 1) && (cursy.position.y > carrot.transform.position.y - 1)))
            {
                Debug.Log("Touching carrot");

                if (transcriptRec == "yes" && carrotFound == false)
                {
                    include.SetActive(true);
                    noInclude.SetActive(false);
                    counter.IncreaseScore(-500);
                    carrotFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    carrot.SetActive(false);
                    Debug.Log("Added carrot");
                    numFoods++;
                }
                else if (transcriptRec == "no" && carrotFound == false)
                {
                    include.SetActive(false);
                    noInclude.SetActive(true);
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    carrotFound = true;
                    Debug.Log(counter.score);
                    carrot.SetActive(false);
                    Debug.Log("Did not add carrot");
                    numFoods++;
                }
            }

            if (((cursy.position.x < apple.transform.position.x + 1) && (cursy.position.x > apple.transform.position.x - 1))
                   && ((cursy.position.y < apple.transform.position.y + 1) && (cursy.position.y > apple.transform.position.y - 1)))
            {
                Debug.Log("Touching apple");

                if (transcriptRec == "yes" && appleFound == false)
                {
                    include.SetActive(true);
                    noInclude.SetActive(false);
                    counter.IncreaseScore(1000);
                    appleFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    apple.SetActive(false);
                    Debug.Log("Added apple");
                    numFoods++;
                }
                else if (transcriptRec == "no" && appleFound == false)
                {
                    include.SetActive(false);
                    noInclude.SetActive(true);
                    counter.IncreaseScore(-500);
                    counter.UpdateScoreDisplay();
                    appleFound = true;
                    Debug.Log(counter.score);
                    apple.SetActive(false);
                    Debug.Log("Did not add apple");
                    numFoods++;
                }
            }

            if (((cursy.position.x < lemon.transform.position.x + 1) && (cursy.position.x > lemon.transform.position.x - 1))
                   && ((cursy.position.y < lemon.transform.position.y + 1) && (cursy.position.y > lemon.transform.position.y - 1)))
            {
                Debug.Log("Touching lemon");

                if (transcriptRec == "yes" && lemonFound == false)
                {
                    include.SetActive(true);
                    noInclude.SetActive(false);
                    counter.IncreaseScore(-500);
                    lemonFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    lemon.SetActive(false);
                    Debug.Log("Added lemon");
                    numFoods++;
                }
                else if (transcriptRec == "no" && lemonFound == false)
                {
                    include.SetActive(false);
                    noInclude.SetActive(true);
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    lemonFound = true;
                    Debug.Log(counter.score);
                    lemon.SetActive(false);
                    Debug.Log("Did not add lemon");
                    numFoods++;
                }
            }

            if (((cursy.position.x < banana.transform.position.x + 1) && (cursy.position.x > banana.transform.position.x - 1))
                   && ((cursy.position.y < banana.transform.position.y + 1) && (cursy.position.y > banana.transform.position.y - 1)))
            {
                Debug.Log("Touching banana");

                if (transcriptRec == "yes" && bananaFound == false)
                {
                    include.SetActive(true);
                    noInclude.SetActive(false);
                    counter.IncreaseScore(1000);
                    bananaFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    banana.SetActive(false);
                    Debug.Log("Added banana");
                    numFoods++;
                }
                else if (transcriptRec == "no" && bananaFound == false)
                {
                    include.SetActive(false);
                    noInclude.SetActive(true);
                    counter.IncreaseScore(-500);
                    counter.UpdateScoreDisplay();
                    bananaFound = true;
                    Debug.Log(counter.score);
                    banana.SetActive(false);
                    Debug.Log("Did not add banana");
                    numFoods++;
                }
            }

            if (((cursy.position.x < tomato.transform.position.x + 1) && (cursy.position.x > tomato.transform.position.x - 1))
                   && ((cursy.position.y < tomato.transform.position.y + 1) && (cursy.position.y > tomato.transform.position.y - 1)))
            {
                Debug.Log("Touching tomato");

                if (transcriptRec == "yes" && tomatoFound == false)
                {
                    include.SetActive(true);
                    noInclude.SetActive(false);
                    counter.IncreaseScore(-500);
                    tomatoFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    tomato.SetActive(false);
                    Debug.Log("Added tomato");
                    numFoods++;
                }
                else if (transcriptRec == "no" && tomatoFound == false)
                {
                    include.SetActive(false);
                    noInclude.SetActive(true);
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    tomatoFound = true;
                    Debug.Log(counter.score);
                    tomato.SetActive(false);
                    Debug.Log("Did not add tomato");
                    numFoods++;
                }
            }

            if (((cursy.position.x < watermelon.transform.position.x + 1) && (cursy.position.x > watermelon.transform.position.x - 1))
                   && ((cursy.position.y < watermelon.transform.position.y + 1) && (cursy.position.y > watermelon.transform.position.y - 1)))
            {
                Debug.Log("Touching watermelon");

                if (transcriptRec == "yes" && watermelonFound == false)
                {
                    include.SetActive(true);
                    noInclude.SetActive(false);
                    counter.IncreaseScore(1000);
                    watermelonFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    watermelon.SetActive(false);
                    Debug.Log("Added watermelon");
                    numFoods++;
                }
                else if (transcriptRec == "no" && watermelonFound == false)
                {
                    include.SetActive(false);
                    noInclude.SetActive(true);
                    counter.IncreaseScore(-500);
                    counter.UpdateScoreDisplay();
                    watermelonFound = true;
                    Debug.Log(counter.score);
                    watermelon.SetActive(false);
                    Debug.Log("Did not add watermelon");
                    numFoods++;
                }
            }

            if (((cursy.position.x < grape.transform.position.x + 1) && (cursy.position.x > grape.transform.position.x - 1))
                   && ((cursy.position.y < grape.transform.position.y + 1) && (cursy.position.y > grape.transform.position.y - 1)))
            {
                Debug.Log("Touching grape");

                if (transcriptRec == "yes" && grapeFound == false)
                {
                    include.SetActive(true);
                    noInclude.SetActive(false);
                    counter.IncreaseScore(-500);
                    grapeFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    grape.SetActive(false);
                    Debug.Log("Added grape");
                    numFoods++;
                }
                else if (transcriptRec == "no" && grapeFound == false)
                {
                    include.SetActive(false);
                    noInclude.SetActive(true);
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    grapeFound = true;
                    Debug.Log(counter.score);
                    grape.SetActive(false);
                    Debug.Log("Did not add grape");
                    numFoods++;
                }
            }

            if (((cursy.position.x < mushroom.transform.position.x + 1) && (cursy.position.x > mushroom.transform.position.x - 1))
                   && ((cursy.position.y < mushroom.transform.position.y + 1) && (cursy.position.y > mushroom.transform.position.y - 1)))
            {
                Debug.Log("Touching mushroom");

                if (transcriptRec == "yes" && mushroomFound == false)
                {
                    include.SetActive(true);
                    noInclude.SetActive(false);
                    counter.IncreaseScore(-500);
                    mushroomFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    mushroom.SetActive(false);
                    Debug.Log("Added mushroom");
                    numFoods++;
                }
                else if (transcriptRec == "no" && mushroomFound == false)
                {
                    include.SetActive(false);
                    noInclude.SetActive(true);
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    mushroomFound = true;
                    Debug.Log(counter.score);
                    mushroom.SetActive(false);
                    Debug.Log("Did not add mushroom");
                    numFoods++;
                }
            }
        }
    }
}