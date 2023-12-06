using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Shield picked up");
            PickUp();
        }
    }

    private void PickUp()
    {
        player.GetComponent<EnergyShield>()?.ActivateShield();
        Destroy(gameObject);
    }
}
