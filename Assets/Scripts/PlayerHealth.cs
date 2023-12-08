using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health settings")]
    public int maxHealth = 200; //maximum health of player
    public int playerHealth; //current health of player
    public HealthBar healthBar; //reference to the health bar UI
    public int healAmount = 20; //amount of health gained when healing

    [Header("Death Screen")]
    public GameObject deathScreen; //reference to the death screen game object
    public GameObject hud; //reference to the hud game object
    public PlayerMovement scriptToDisable; //referecnce to the player movement script to disable on death
    
    void Start()
    {
        // Initialize player health and set the max health for the health bar
        playerHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Function to handle when the player takes damage
    public void PlayerTakeDamage(int damage)
    {
        // Decrease player health and update the health bar
        playerHealth = playerHealth - damage;
        healthBar.SetHealth(playerHealth);
    } 

    void Update()
    {
        // Check if player health is less than 1 and trigger death
        if (playerHealth < 1)
        { 
            Death();
        }
    }

    // Function to handle player death
    public void Death()
    {
        //disable hud and show death screen
        hud.SetActive(false);
        deathScreen.SetActive(true);

        //disable the player movement script
        scriptToDisable.enabled = false;
    }

    //function to heal the player
    public void Heal()
    {
        //increase player health by the specified amount and cap it at the max
        playerHealth += healAmount;
        if(playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }

        //update the health bar
        healthBar.SetHealth(playerHealth);
    }
}
