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


        public GameObject apple;
        public GameObject fish;
        int i = 0;
        int j = 0;
        int numOpts = 2;
        int points = 0;
        int missed = 0;
        int correct = 0;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Test");

            //bool pos = CursorObject.fs_read_hands_csv.positioner;


            //transcriptRec = streamRec.transcript;
        }


            /*GameObject speechObj = GameObject.Find("SpeechTest");
            StreamingRecognizer streamRec = speechObj.GetComponent<StreamingRecognizer>();
            Debug.Log("Hello");
            transcriptRec = streamRec.transcript;

            //if (transcriptRec == "yes")
            //{
                Debug.Log("Hello");
            }

            Debug.Log(transcriptRec);
            */

            // IMPORTANT SHIT

            //GameObject speechObj = GameObject.Find("SpeechTest");
            //StreamingRecognizer streamRec = speechObj.GetComponent<StreamingRecognizer>();
            //Console.WriteLine("Hello");
            //transcriptRec = streamRec.transcript;
            //Debug.Log(transcriptRec);


            /*if (transcriptRec == "yes")
            {
                Debug.Log("Hello");
            }*/

        // Update is called once per frame
        void Update()
        {
            Transform cursy = handReader.Cursor;
            //public Transform cursy = handReader.Cursor;
            string[] recipe = { "Watermelon", "Canteloupe", "Apple", "Pear", "Orange", "Grapefruit", "Banana", "Coconut" };
            string[] ingredients = new string[5];
            string[] notIncluded = new string[5];
            transcriptRec = streamRec.transcript;

            includeText.SetActive(false);

            if (((cursy.position.x < apple.transform.position.x + 0.5) && (cursy.position.x > apple.transform.position.x - 0.5))
                    && ((cursy.position.z < apple.transform.position.z + 0.5) && (cursy.position.z > apple.transform.position.z - 0.5)))
            {
                includeText.SetActive(true);
                if (transcriptRec == "yes")
                {
                    ingredients[0] = apple.name;
                    i++;
                    apple.SetActive(false);
                }
                else if (transcriptRec == "no")
                {
                    notIncluded[j] = apple.name;
                    j++;
                    apple.SetActive(false);
                }

                //includeText.SetActive(false);
            }

            //Debug.Log(cursy.position);

            if (((cursy.position.x < fish.transform.position.x + 0.3) && (cursy.position.x > fish.transform.position.x - 0.3))
                    && ((cursy.position.z < fish.transform.position.z + 0.7) && (cursy.position.z > fish.transform.position.z - 0.7)))
            {
                includeText.SetActive(true);
                if (transcriptRec == "yes")
                {
                    ingredients[0] = fish.name;
                    i++;
                    fish.SetActive(false);
                }
                else if (transcriptRec == "no")
                {
                    notIncluded[j] = fish.name;
                    j++;
                    fish.SetActive(false);
                }
                // includeText.SetActive(false);
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