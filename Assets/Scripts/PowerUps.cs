using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public int powerUpId;
    public GameObject Player;

    void start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collider)
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
            }
            
        }
        Destroy(gameObject);
    }
}
