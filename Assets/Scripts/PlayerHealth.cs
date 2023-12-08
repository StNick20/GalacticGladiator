using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health settings")]
    public int maxHealth = 200;
    public int playerHealth;
    public HealthBar healthBar;

    [Header("Death Screen")]
    public GameObject deathScreen;
    public GameObject hud;
    public PlayerMovement scriptToDisable;
    
    void Start()
    {
        playerHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void PlayerTakeDamage(int damage)
    {
        playerHealth = playerHealth - damage;
        healthBar.SetHealth(playerHealth);
    } 

    void Update()
    {
        if(playerHealth < 1)
        { 
            Death();
        }
    }

    //this will run when the player reaches 0 health
    public void Death()
    {
        hud.SetActive(false);
        deathScreen.SetActive(true);
        scriptToDisable.enabled = false;
    }
}
