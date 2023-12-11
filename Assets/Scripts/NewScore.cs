using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System;

public class NewScore : MonoBehaviour
{
    // Path to the file where the leaderboard data will be stored
    private string leaderboardFilePath = "Assets/Leaderboard.txt";

    public TMP_Text InputField;

    // Declare the leaderboard array
    private string[] leaderboard;

    void Start()
    {
        

        // Read all lines from the leaderboard file
        leaderboard = File.ReadAllLines(leaderboardFilePath);

        if(leaderboard.Length == 5)
        {
            GameObject.Find("InputCanvas").SetActive(false);
        }
        else
        {
            GameObject.Find("LeaderBoardCanvas").SetActive(false);
        }

        Debug.Log(leaderboard.Length);

        GameObject.Find("InputTitle").GetComponent<TMP_Text>().text = "Your Score: " + leaderboard[5];
    }

    public void ButtonPress()
    {

        leaderboard[5] = InputField.text.ToString() + ":" + leaderboard[5];


        leaderboard = leaderboard.Select(line => line.Split(':'))
            .OrderByDescending(parts => int.Parse(parts[1].Trim())) // Trim to remove extra whitespaces
            .Select(parts => string.Join(":", parts))
            .ToArray();

        Array.Resize(ref leaderboard, 5);
        foreach (var part in leaderboard)
        {
            Debug.Log(part);
        }

        File.WriteAllLines(leaderboardFilePath, leaderboard);
    }
}
