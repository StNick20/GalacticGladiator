using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Method to load the "Level1" scene
    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    // Method to quit the application
    public void Quit()
    {
        Application.Quit(); // Close the application
        Debug.Log("Game Closed"); // Log a message to the console indicating that the game has been closed
    }

    //method to load the "menu" scene
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
