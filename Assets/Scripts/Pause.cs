using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    [Header("Pausing")]
    public GameObject pauseObject;
    public GameObject hud;
    public GameObject[] enemies; // Use an array to store multiple enemies

    // Start is called before the first frame update
    void Start()
    {
        //find all game objects with the 'Enemy' tag
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        //checks if escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc key pressed");

            // Disable HUD and show pause menu
            hud.SetActive(false);
            pauseObject.SetActive(true);

            // disables player movement
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            
            // Iterate through each enemy and disable their components
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<Enemy>().enabled = false;
                enemy.GetComponent<Shooting>().enabled = false;
            }
        }
    }

    // Method to resume the game
    public void Play()
    {
        // Enable HUD and hide pause menu
        hud.SetActive(true);
        pauseObject.SetActive(false);
        // Enable player movement
        gameObject.GetComponent<PlayerMovement>().enabled = true;

        // Iterate through each enemy and enable their components
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().enabled = true;
            enemy.GetComponent<Shooting>().enabled = true;
        }
    }
}
