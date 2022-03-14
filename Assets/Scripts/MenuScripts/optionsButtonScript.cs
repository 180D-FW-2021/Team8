using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class optionsButtonScript : MonoBehaviour
{
	string optionName = "handReadDelay";
	// Drag the Input Field into this.
	public InputField input;
	public GameObject canvas1;
	
	// Use a button UI that calls this function that saves the input text into Player Prefs.
	public void OnButtonClick()
	{
		int playerInt = intSanitize(input.text);
		string savedText = playerInt.ToString();
		PlayerPrefs.SetString(optionName, savedText);
		Debug.Log("Saved Text:");
		Debug.Log(savedText);
		gameSelectionScene();
	}
	 
	// If the "optionName" exists when this script is called on start, it will put the text into the inputfield.
	private void Start(){
		if(PlayerPrefs.HasKey(optionName))
		{
			input.text = PlayerPrefs.GetString(optionName);
		}
	}
	
	// takes in the InputField string and returns equivalent sanitized integer.
	int intSanitize(string inputText)
	{
		int inputInt = 0;
		bool initSuccess = System.Int32.TryParse(inputText, out inputInt);
		if(initSuccess)
		{
			if(inputInt > 1000)
			{
				inputInt = 1000;
			}
			if(inputInt < 5)
			{
				inputInt = 5;
			}
		}
		else
		{
			inputInt = 13; // good typical value
		}
		return inputInt;
	}
	
	public void gameSelectionScene()
    {
		canvas1.SetActive(false);
        Debug.Log("Game Selection loading");
        SceneManager.LoadScene("ChopChopKitchen", LoadSceneMode.Single);
    }
}
