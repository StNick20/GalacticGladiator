using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            PickUp();
        }
    }

    public void PickUp()
    {
        Debug.Log("health kit picked up");

        GameObject player = GameObject.FindWithTag("Player");
        
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.heal();
    }    
}
