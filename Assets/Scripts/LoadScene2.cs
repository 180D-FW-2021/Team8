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
        if (GUI.Button(new Rect(50, 50, 160, 30), "Return to Main Menu"))
        {
            Debug.Log("Scene2 loading");
            SceneManager.LoadScene("MAINTEST", LoadSceneMode.Single);
        }
    }
}
