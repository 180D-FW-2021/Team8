using System.Collections.Generic;
using UnityEngine;

public class HighScoreEntry
{
    public string name;
    public int score;
}

public class HighScores : MonoBehaviour
{
    public HighScoreDisplay[] highScoreDisplayArray;
    List<HighScoreEntry> scores = new List<HighScoreEntry>();

    void Start()
    {
        // Adds some test data
        AddNewScore("Megalodon", 10900);
        AddNewScore("Doug", 4900);
        AddNewScore("Dave", 380);
        AddNewScore("Steve", 6654);
        AddNewScore("Mike", 9021);
        AddNewScore("Teddy", 3252);
        
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.score.CompareTo(x.score));

        for (int i = 0; i < highScoreDisplayArray.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreDisplayArray[i].DisplayHighScore(scores[i].name, scores[i].score);
            }
            else
            {
                highScoreDisplayArray[i].HideEntryDisplay();
            }
        }
    }

    public void AddNewScore(string entryName, int entryScore)
    {
        scores.Add(new HighScoreEntry { name = entryName, score = entryScore });
    }
}