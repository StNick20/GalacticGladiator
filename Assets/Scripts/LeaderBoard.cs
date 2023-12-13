using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class LeaderBoard : MonoBehaviour
{
    // Path to the file where the leaderboard data will be stored
    private string leaderboardFilePath = "Assets/Leaderboard.txt";

    // Declare the leaderboard array
    private string[] leaderboard;

    void Start()
    {
        if (!File.Exists(leaderboardFilePath))
        {
            string[] lines = new string[]
            {
            "Blank:0",
            "Blank:0",
            "Blank:0",
            "Blank:0",
            "Blank:0"
            };

            File.WriteAllLines(leaderboardFilePath, lines);
        }
    }

    void Update()
    {
        // Read all lines from the leaderboard file
        leaderboard = File.ReadAllLines(leaderboardFilePath);

        GameObject.Find("First").GetComponent<TMP_Text>().text = leaderboard[0];
        GameObject.Find("Second").GetComponent<TMP_Text>().text = leaderboard[1];
        GameObject.Find("Third").GetComponent<TMP_Text>().text = leaderboard[2];
        GameObject.Find("Fourth").GetComponent<TMP_Text>().text = leaderboard[3];
        GameObject.Find("Fifth").GetComponent<TMP_Text>().text = leaderboard[4];
    }
}
