using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Scoring : MonoBehaviour
{
    [Header("ScoreBoard")]
    public int score;
    public TMP_Text TextBox;
    private int finalScore = 0;

    [Header("Timer")]
    public float timer;
    public TMP_Text timerText;

    // Path to the file where the leaderboard data will be stored
    private string leaderboardFilePath = "Assets/Leaderboard.txt";

    // Update is called once per frame
    void Update()
    {
        TextBox.text = "Score: " + score;

        timer += Time.deltaTime;
        timerText.text = "Timer: " + timer.ToString("F0");

        
    }

    public void FinalScore()
    {
        finalScore = score * ((int)timer) / 2;
        Debug.Log("FinalScore: " + finalScore);

        // Save the final score to the leaderboard file
        SaveScoreToLeaderboard(finalScore);
    }

    private void SaveScoreToLeaderboard(int scoreToSave)
    {
        // Check if the file exists, create it if not
        if (!File.Exists(leaderboardFilePath))
        {
            File.WriteAllText(leaderboardFilePath, "Leaderboard\n");
        }

        // Append the new score to the leaderboard file
        File.AppendAllText(leaderboardFilePath, scoreToSave + "\n");
    }
}
