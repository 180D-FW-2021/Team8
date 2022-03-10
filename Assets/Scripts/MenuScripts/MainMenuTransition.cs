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

    public void loadFryFry()
    {
        Debug.Log("Fry Fry loading");
        SceneManager.LoadScene("Fry Fry", LoadSceneMode.Single);
    }

    public void loadThinky1()
    {
        Debug.Log("Thinky 1 loading");
        SceneManager.LoadScene("Thinky1", LoadSceneMode.Single);
    }

    public void loadThink2()
    {
        Debug.Log("Thinky 2 loading");
        SceneManager.LoadScene("Thinky2", LoadSceneMode.Single);
    }

    public void loadThinky22()
    {
        Debug.Log("Thinky 2.2 loading");
        SceneManager.LoadScene("Thinky2.2", LoadSceneMode.Single);
    }

    public void loadPickMe()
    {
        Debug.Log("PickyPick loading");
        SceneManager.LoadScene("PickyPick", LoadSceneMode.Single);
    }
}
