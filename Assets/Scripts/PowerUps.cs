using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    // Reference to the player GameObject
    public GameObject player;

    // Identifier for different power-ups
    public int powerUpID;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player GameObject with the "Player" tag
        player = GameObject.FindWithTag("Player");    
    }
    // Called when another Collider2D enters the trigger zone
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider is the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Switch statement to determine the effect of the power-up based on its ID
            switch (powerUpID)
            {
                // Power-up ID 0: Heal the player
                case 0:
                    player.GetComponent<PlayerHealth>().Heal();
                    break;
                // Power-up ID 1: Reload the player's weapon
                case 1:
                    player.GetComponent<PlayerMovement>().Reload();
                    break;
                // Power-up ID 2: Charge up the energy shield
                case 2:
                    player.GetComponent<EnergyShield>().ChargeUp();
                    break;
            }

            // Destroy the power-up GameObject after applying its effect
            Destroy(gameObject);
        }
    }
}
