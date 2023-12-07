using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnergyShield : MonoBehaviour
{
    public GameObject shield;
    public int shieldCharge = 0;
    public bool shieldStatus = false;

    void Start()
    {
        shield = GameObject.FindWithTag("Shield");
    }

    public void ActivateShield()
    {
        if(shieldCharge < 3)
        {
            shieldCharge += 1;
        }
        Debug.Log("shieldCharge value: " + shieldCharge);
    }

    void Update()
    {
        if(shieldCharge > 0)
        {
            shieldStatus = true;
        }
        if(shieldCharge == 0)
        {
            shieldStatus = false;
        }

        shield.SetActive(shieldStatus);
    }
}
