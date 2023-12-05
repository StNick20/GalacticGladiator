using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overdrive : MonoBehaviour
{

    public bool overdriveActive;
  
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            
            PickUp();
        }
    }

    void PickUp()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        StartCoroutine(playerMovement.Overdrive());
        Destroy(gameObject);
    }
}
