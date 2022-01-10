using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuTransition : MonoBehaviour
{

    public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        if(Panel != null){
            Panel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameSelectionScene()
    {
        Debug.Log("Game Selection loading");
        SceneManager.LoadScene("GameSelection", LoadSceneMode.Single);
    }

    public void exitGame()
    {
        Application.Quit();
    }

     public void testScoreScreen()
    {
        if(Panel != null){
            Panel.SetActive(true);
        }
        
    }
}
