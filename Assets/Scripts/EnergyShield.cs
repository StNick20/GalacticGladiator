using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : MonoBehaviour
{
    // Current shield charge level
    public int shieldCharge = 0;

    // Maximum shield charge level
    public int MaxShield = 3;

    // Reference to the shield GameObject
    public GameObject shield;

    // Update is called once per frame
    void Update()
    {
        // Set the shield GameObject active based on the shield charge level
        shield.SetActive(shieldCharge > 0);
    }

    // Method to increase the shield charge
    public void ChargeUp()
    {
        // Increment the shield charge and ensure it does not exceed the maximum
        shieldCharge += 1;
        if(shieldCharge > MaxShield)
        {
            shieldCharge = MaxShield;
        }
    }

    // Called when another Collider2D enters the trigger zone
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider is an enemy bullet
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            // Decrease the shield charge when hit by an enemy bullet and stops it form entering negatives
            shieldCharge -= 1;
            if (shieldCharge < 0)
            {
                shieldCharge = 0;
            }
        }
    }

}
