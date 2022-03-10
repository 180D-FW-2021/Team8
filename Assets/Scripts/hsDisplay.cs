using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hsDisplay : MonoBehaviour
{
    public Text nameText;
    public Text scoreText;

    public void DisplayHS(string name, int score)
    {
        nameText.text = name;
        scoreText.text = string.Format("{0:000000}", score);
    }

    public void HideDisplay()
    {
        nameText.text = "";
        scoreText.text = "";
    }
}
