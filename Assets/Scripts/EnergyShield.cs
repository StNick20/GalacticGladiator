using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnergyShield : MonoBehaviour
{
    public GameObject shield;
    [SerializeField] public int shieldCharge = 0;

    public void ActivateShield()
    {
        shieldCharge += 1;
        UpdateShieldStatus();
    }

    private void UpdateShieldStatus()
    {
        if (shieldCharge <= 0)
        {
            Debug.Log("Shield deactivated");
            SetShieldActive(false);
        }
        else
        {
            Debug.Log("Shield activated");
            SetShieldActive(true);
        }

        EditorUtility.SetDirty(this);
    }

    private void SetShieldActive(bool isActive)
    {
        Debug.Log("Setting shield active status: " + isActive);
        shield.SetActive(isActive);
        Debug.Log("Is shield active: " + shield.activeSelf);
    }
}
