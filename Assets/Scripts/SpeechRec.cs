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
        public fs_read_hands_csv handReader;
        public GameObject apple;
        public GameObject includeText;

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
            if (((cursy.position.x < apple.transform.position.x + 0.3) && (cursy.position.x > apple.transform.position.x - 0.3))
                    && ((cursy.position.z < apple.transform.position.z + 0.3) && (cursy.position.z > apple.transform.position.z - 0.3)))
            {
                includeText.SetActive(true);
            }
            transcriptRec = streamRec.transcript;
            //Debug.Log("Tester: " + transcriptRec);

            string[] recipe = { "Watermelon", "Canteloupe", "Apple", "Pear", "Orange", "Grapefruit", "Banana", "Coconut" };
            string[] ingredients = new string[2];

            if (transcriptRec == "yes")
            {
                ingredients[0] = apple.name;
            }

            int points = 0;
            for (int i = 0; i < recipe.Length; i++)
            {
                if (ingredients[0] == recipe[i])
                {
                    points += 100;
                }
            }
            Debug.Log("Points: " + points.ToString());


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