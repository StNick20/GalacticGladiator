using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collider)
    {


        GameObject player = GameObject.FindWithTag("Player");

        PlayerHealth Health = player.GetComponent<PlayerHealth>();

        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            PickUp(Health);
        }
    }

    public void PickUp(PlayerHealth playerHealth)
    {
        Debug.Log("health kit picked up");

        playerHealth.heal();
    }    
}
