using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public int powerUpId;
    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            switch (powerUpId)
            {
                case 0:
                    Player.GetComponent<PlayerMovement>().AmmoPickUp();
                    break;
                case 1:
                    Player.GetComponent<PlayerHealth>().Heal();
                    break;
                case 2:
                    Player.GetComponent<EnergyShield>().ActivateShield();
                    break;
            }
            
        }
        Destroy(gameObject);
    }
}
