using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 200;
    public int playerHealth;
    
    void Start()
    {
        playerHealth = maxHealth;
    }

    public void PlayerTakeDamage(int damage)
    {
    playerHealth = playerHealth - damage;
    } 
    void Update()
    {
        if(playerHealth < 1){ 
        Destroy(this.gameObject);
        }
    }
}
