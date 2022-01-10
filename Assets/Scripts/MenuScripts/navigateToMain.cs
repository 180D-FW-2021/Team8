using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class navigateToMain : MonoBehaviour
{
    public GameObject returnButton;
    // Start is called before the first frame update
    void Start()
    {
        returnButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pressButton(){
        SceneManager.LoadScene("MAINTEST", LoadSceneMode.Single);
    }
}
