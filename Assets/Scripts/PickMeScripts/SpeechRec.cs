using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleCloudStreamingSpeechToText;
using GoogleCloudStreamingSpeechToText;
using CursorObject;

namespace Speecher
{
    public class SpeechRec : MonoBehaviour
    {
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

        public HighScores scoreboard;

            // THIS IS ACTUALLY SUPER IMPORTANT

        /*public GameObject includeText;
        public GameObject excludeText;
        public GameObject which;

        public GameObject addedText;
        public GameObject notAddedText;

        public GameObject done;
        public GameObject pointCtr;
        public GameObject gameOver;
        public GameObject returnToMenu;

        public GameObject apple;
        public GameObject fish;
        public GameObject grapefruit;
        public GameObject cabbage;
        */       

        //public HighScoreEntry curScore;
        //public HighScores leaderboard;
        //public HighScoreDisplay curPlayer;
        public ScoreCounter counter;

        /*
        int i = 0;
        int j = 0;
        //int k = 0;
        int numOpts = 3;
        int points = 0;
        int missed = 0;
        int correct = 0;
        */

        public GameObject leaderboard;       

        bool breadFound = false;
        bool carrotFound = false;
        bool appleFound = false;
        int numFoods = 0;

        //bool appleCompl = false;
        //bool fishCompl = false;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Test");
            //counter.IncreaseScore(5000);
            //counter.UpdateScoreDisplay();
            //counter.IncreaseScore(5000);
            //entry1.SetActive(true);
            //leaderboard.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            Transform cursy = handReader.Cursor;
            //public Transform cursy = handReader.Cursor;
           // string[] recipe = { "Watermelon", "Canteloupe", "Apple", "Pear", "Orange", "Grapefruit", "Banana", "Coconut" };
           // string[] ingredients = new string[5];
           // string[] notIncluded = new string[5];


            //if (((cursy.position.x < bread.transform.position.x + 36) && (cursy.position.x > bread.transform.position.x + 32))
            // && ((cursy.position.y < bread.transform.position.y + 16) && (cursy.position.y > bread.transform.position.y + 12)))

            // this works
            //if(((cursy.position.x < 36) && (cursy.position.x > 30)) && ((cursy.position.y < 23) && (cursy.position.y > 16)))

            // x around 36
            //Debug.Log(bread.transform.position.y);

            transcriptRec = streamRec.transcript;
            Debug.Log(transcriptRec);

            if(numFoods == 3)
            {
                // Send to high score
                // Make final score big on the screen or something

                scoreboard.AddNewScore("Player1", counter.score);

                Debug.Log("Added all food");
                //entry1.SetActive(true);
                leaderboard.SetActive(true);

            }

            if (((cursy.position.x < bread.transform.position.x + 1) && (cursy.position.x > bread.transform.position.x - 1))
                   && ((cursy.position.y < bread.transform.position.y + 1) && (cursy.position.y > bread.transform.position.y - 1)))
            {
                Debug.Log("Touching bread");

                if (transcriptRec == "yes" && breadFound == false)
                {
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
                    counter.IncreaseScore(-500);
                    appleFound = true;
                    counter.UpdateScoreDisplay();
                    Debug.Log(counter.score);
                    apple.SetActive(false);
                    Debug.Log("Added apple");
                    numFoods++;
                }
                else if (transcriptRec == "no" && appleFound == false)
                {
                    counter.IncreaseScore(1000);
                    counter.UpdateScoreDisplay();
                    appleFound = true;
                    Debug.Log(counter.score);
                    apple.SetActive(false);
                    Debug.Log("Did not add apple");
                    numFoods++;
                }
            }

            // ALSO SUPER IMPORTANT!!!!
            /*
            //transcriptRec = streamRec.transcript;

            includeText.SetActive(false);
            addedText.SetActive(false);
            notAddedText.SetActive(false);



            if (((cursy.position.x < apple.transform.position.x + 0.5) && (cursy.position.x > apple.transform.position.x - 0.5))
                    && ((cursy.position.z < apple.transform.position.z + 0.5) && (cursy.position.z > apple.transform.position.z - 0.5)))
            {
                //streamRec.StartListening();
                which.SetActive(true);
                transcriptRec = streamRec.transcript;
                if (transcriptRec == "yes")
                {
                    includeText.SetActive(true);
                    ingredients[0] = apple.name;
                    i++;
                    addedText.SetActive(true);
                    apple.SetActive(false);
                    Debug.Log("Added apple");
                    //appleCompl = true;
                    // add points if correct
                    bool found = false;
                    for(int i = 0; i < recipe.Length; i++)
                    {
                        if(apple.name == recipe[i])
                        {
                            counter.IncreaseScore(100);
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        counter.IncreaseScore(-50);
                    }
                }
                else if (transcriptRec == "no")
                {
                    excludeText.SetActive(true);
                    notIncluded[0] = apple.name;
                    j++;
                    notAddedText.SetActive(true);
                    apple.SetActive(false);
                    Debug.Log("Did not add apple");
                    //appleCompl = true;
                    bool found = false;
                    for (int i = 0; i < recipe.Length; i++)
                    {
                        if (apple.name != recipe[i])
                        {
                            counter.IncreaseScore(100);
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        counter.IncreaseScore(-50);
                    }
                }

                //includeText.SetActive(false);
            }

            //Debug.Log(cursy.position);

            if (((cursy.position.x < fish.transform.position.x + 0.3) && (cursy.position.x > fish.transform.position.x - 0.3))
                    && ((cursy.position.z < fish.transform.position.z + 0.7) && (cursy.position.z > fish.transform.position.z - 0.7)))
            {
                which.SetActive(true);
                //streamRec.StartListening();
                transcriptRec = streamRec.transcript;
                //includeText.SetActive(true);
                if (transcriptRec == "yes")
                {
                    includeText.SetActive(true);
                    ingredients[0] = fish.name;
                    i++;
                    addedText.SetActive(true);
                    fish.SetActive(false);
                    Debug.Log("Added fish");
                    //fishCompl = true;
                }
                else if (transcriptRec == "no")
                {
                    excludeText.SetActive(true);
                    notIncluded[0] = fish.name;
                    j++;
                    notAddedText.SetActive(true);
                    fish.SetActive(false);
                    Debug.Log("Fish not included");
                    //fishCompl = true;
                }
                // includeText.SetActive(false);
            }

            if (((cursy.position.x < grapefruit.transform.position.x + 0.3) && (cursy.position.x > grapefruit.transform.position.x - 0.3))
                    && ((cursy.position.z < grapefruit.transform.position.z + 0.7) && (cursy.position.z > grapefruit.transform.position.z - 0.7)))
            {
                which.SetActive(true);
                //streamRec.StartListening();
                //Debug.Log("Grapefruit");
                transcriptRec = streamRec.transcript;
                Debug.Log("On grapefruit");
                //includeText.SetActive(true);
                if (transcriptRec == "yes")
                {
                    includeText.SetActive(true);
                    ingredients[0] = grapefruit.name;
                    i++;
                    addedText.SetActive(true);
                    grapefruit.SetActive(false);
                    Debug.Log("Added grapefruit");
                    //fishCompl = true;
                }
                else if (transcriptRec == "no")
                {
                    excludeText.SetActive(true);
                    notIncluded[0] = grapefruit.name;
                    j++;
                    notAddedText.SetActive(true);
                    grapefruit.SetActive(false);
                    //fishCompl = true;
                }
                // includeText.SetActive(false);
            }
            // 347 and 204
            if (((cursy.position.x < cabbage.transform.position.x + 400) && (cursy.position.x > cabbage.transform.position.x + 300))
                    && ((cursy.position.y < cabbage.transform.position.y + 200) && (cursy.position.y > cabbage.transform.position.y + 208)))
            {
                which.SetActive(true);
                //streamRec.StartListening();
                transcriptRec = streamRec.transcript;
                Debug.Log("On Cabbage");
                if (transcriptRec == "yes")
                {
                    includeText.SetActive(true);
                    ingredients[0] = cabbage.name;
                    i++;
                    addedText.SetActive(true);
                    cabbage.SetActive(false);
                    Debug.Log("Added cabbage");
                    //appleCompl = true;
                }
                else if (transcriptRec == "no")
                {
                    excludeText.SetActive(true);
                    notIncluded[0] = cabbage.name;
                    j++;
                    notAddedText.SetActive(true);
                    cabbage.SetActive(false);
                    //appleCompl = true;
                }

                //includeText.SetActive(false);
            }

            if (((cursy.position.x < done.transform.position.x + 2) && (cursy.position.x > done.transform.position.x - 2))
                    && ((cursy.position.z < done.transform.position.z + 1) && (cursy.position.z > done.transform.position.z - 1)))
            {
                pointCtr.SetActive(true);
                gameOver.SetActive(true);
                returnToMenu.SetActive(true);
                cursor.SetActive(false);
            }

            //Debug.Log("Tester: " + transcriptRec);


            if (i + j == numOpts)
            {
                for (int k = 0; k < ingredients.Length; k++)
                {
                    //bool madeTheCut = false;
                    for (int l = 0; l < recipe.Length; l++)
                    {
                        if (ingredients[k] == recipe[l])
                        {
                            points += 100;
                            correct += 1;
                            Debug.Log("Included: " + ingredients[k]);
                        }
                    }
                }
                for (int k = 0; k < notIncluded.Length; k++)
                {
                    //bool madeTheCut = true;
                    for (int l = 0; l < recipe.Length; l++)
                    {
                        if (notIncluded[k] == recipe[l])
                        {
                            //madeTheCut = false;
                            points -= 50;
                            missed += 1;
                            Debug.Log("Missed: " + notIncluded[k]);
                        }
                    }
                }
                Debug.Log("Points: " + points.ToString());
                //curPlayer.DisplayHighScore("Ralph", points);
                //addScore.IncreaseScore(points);
            }
            */


            /*GameObject thePlayer = GameObject.Find("SpeechTest");
            PlayerScript playerScript = thePlayer.GetComponent<PlayerScript>();
            playerScript.Health -= 10.0f;*/

            //transcriptRec = GameObject.Find("SpeechTest").GetComponent<StreamingRecognizer>().transcript; ;
            /*
            GameObject speechObj = GameObject.Find("SpeechTest");
            StreamingRecognizer streamRec = speechObj.GetComponent<StreamingRecognizer>();
            //Console.WriteLine("Hello");
            transcriptRec = streamRec.transcript;

            if(transcriptRec == "yes")
            {
                Debug.Log("Hello");
            }

            Debug.Log(transcriptRec);
            */
        }
    }
}