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

        public GameObject includeText;
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

        int i = 0;
        int j = 0;
        //int k = 0;
        int numOpts = 3;
        int points = 0;
        int missed = 0;
        int correct = 0;

        //bool appleCompl = false;
        //bool fishCompl = false;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Test");
        }

        // Update is called once per frame
        void Update()
        {
            Transform cursy = handReader.Cursor;
            //public Transform cursy = handReader.Cursor;
            string[] recipe = { "Watermelon", "Canteloupe", "Apple", "Pear", "Orange", "Grapefruit", "Banana", "Coconut" };
            string[] ingredients = new string[5];
            string[] notIncluded = new string[5];


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
                if (transcriptRec == "include")
                {
                    includeText.SetActive(true);
                    ingredients[0] = apple.name;
                    i++;
                    addedText.SetActive(true);
                    apple.SetActive(false);
                    Debug.Log("Added apple");
                    //appleCompl = true;
                }
                else if (transcriptRec == "exclude")
                {
                    excludeText.SetActive(true);
                    notIncluded[0] = apple.name;
                    j++;
                    notAddedText.SetActive(true);
                    apple.SetActive(false);
                    //appleCompl = true;
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
                if (transcriptRec == "include")
                {
                    includeText.SetActive(true);
                    ingredients[0] = fish.name;
                    i++;
                    addedText.SetActive(true);
                    fish.SetActive(false);
                    Debug.Log("Added fish");
                    //fishCompl = true;
                }
                else if (transcriptRec == "exclude")
                {
                    excludeText.SetActive(true);
                    notIncluded[0] = fish.name;
                    j++;
                    notAddedText.SetActive(true);
                    fish.SetActive(false);
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
                //includeText.SetActive(true);
                if (transcriptRec == "include")
                {
                    includeText.SetActive(true);
                    ingredients[0] = grapefruit.name;
                    i++;
                    addedText.SetActive(true);
                    grapefruit.SetActive(false);
                    Debug.Log("Added grapefruit");
                    //fishCompl = true;
                }
                else if (transcriptRec == "exclude")
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

            if (((cursy.position.x < cabbage.transform.position.x + 0.5) && (cursy.position.x > cabbage.transform.position.x - 0.5))
                    && ((cursy.position.z < cabbage.transform.position.z + 0.5) && (cursy.position.z > cabbage.transform.position.z - 0.5)))
            {
                which.SetActive(true);
                //streamRec.StartListening();
                transcriptRec = streamRec.transcript;
                if (transcriptRec == "include")
                {
                    includeText.SetActive(true);
                    ingredients[0] = cabbage.name;
                    i++;
                    addedText.SetActive(true);
                    cabbage.SetActive(false);
                    Debug.Log("Added cabbage");
                    //appleCompl = true;
                }
                else if (transcriptRec == "exclude")
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
            }


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