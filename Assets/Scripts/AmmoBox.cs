using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    public int pickUpAmount = 0;

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            PickUp();
        }
    }

    void PickUp()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        playerMovement.AmmoPickUp(pickUpAmount);

        
    }
}
