using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 120, 30), "Done with Speech"))
        {
            Debug.Log("Scene2 loading");
            SceneManager.LoadScene("SpeechTestPt2", LoadSceneMode.Single);
        }
    }
}
