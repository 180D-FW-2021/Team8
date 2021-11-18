using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
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
        if (GUI.Button(new Rect(50, 50, 100, 30), "START"))
        {
            Debug.Log("Scene2 loading");
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
