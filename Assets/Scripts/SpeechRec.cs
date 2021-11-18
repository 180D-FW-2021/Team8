using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleCloudStreamingSpeechToText;

public class SpeechRec : MonoBehaviour
{
    string transcriptRec;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Test");
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
    }

    // Update is called once per frame
    void Update()
    {
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
