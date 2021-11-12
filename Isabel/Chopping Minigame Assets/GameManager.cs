using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum StateType
{
    DEFAULT,    // Fall-back state, should never happen
    PLAYING,    // Player actively tracing shape
    PAUSING,    // Player pausing game
    WIN,        // Player has won the game
    LOSE        // Player has lost the game
}

public class GameManager : MonoBehaviour
{
    private StateType gameState = StateType.DEFAULT;
    private float remainingTime = 0;

    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject Objective;
    public Text timeText;

    public void Pause(bool paused)
    {
        if(paused) {
            gameState = StateType.PAUSING;
        } else {
            gameState = StateType.PLAYING;
        }
    }

    public StateType getState()
    {
        return gameState;
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = StateType.PLAYING;
        remainingTime = 30;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case StateType.PLAYING:
                Objective.SetActive(true);
                WinScreen.SetActive(false);
                LoseScreen.SetActive(false);
                timeText.enabled = true;
                break;
            case StateType.PAUSING:
                break;
            case StateType.WIN:
                Objective.SetActive(false);
                WinScreen.SetActive(true);
                LoseScreen.SetActive(false);
                timeText.enabled = false;
                break;
            case StateType.LOSE:
                Objective.SetActive(false);
                WinScreen.SetActive(false);
                LoseScreen.SetActive(true);
                timeText.enabled = false;
                break;
            default:
                Debug.Log("ERROR: Unknown game state");
                break;
        }

        if (remainingTime > 0 && getState() != StateType.PAUSING) {
            remainingTime -= Time.deltaTime;
            DisplayTime(remainingTime);
        }
        else if (remainingTime <= 0) {
            Debug.Log("Time has run out!");
            remainingTime = 0;
            gameState = StateType.LOSE;
        }
    }
}
