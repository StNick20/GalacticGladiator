using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 200;
    public int playerHealth;
    public HealthBar healthBar;
    
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
        if(Input.GetKeyDown(KeyCode.Space)){
            PlayerTakeDamage(20);
        }
        if(playerHealth < 1){ 
        Destroy(this.gameObject);
        }
    }
}
