using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    public Text nameText;
    public Text scoreText;

    public void DisplayHighScore(string name, int score)
    {
        nameText.text = name;
        scoreText.text = string.Format("{0:000000}", score);
    }

    public void HideEntryDisplay()
    {
        nameText.text = "";
        scoreText.text = "";
    }
}
