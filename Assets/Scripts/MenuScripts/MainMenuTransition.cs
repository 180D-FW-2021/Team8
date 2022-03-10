using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuTransition : MonoBehaviour
{

    public GameObject Panel;
	public csvReaderStart pyKiller; // here to properly close python

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
		// note: pyKiller's OnApplicationQuit() callback closes Python.
		// But it only autocloses if user closes/quits from main menu.
        Application.Quit();
    }

    public void testScoreScreen()
    {
        if(Panel != null){
            Panel.SetActive(true);
        }
        
    }
}
