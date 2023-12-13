using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageHUD : MonoBehaviour
{
    public Image hudImage;
    public float displayDuration = 0.4f; // Adjust the display duration as needed

    void Start()
    {
        // Initially hide the HUD image
        hudImage.enabled = false;
    }

    public void ShowDamageHUD()
    {
        // Show the HUD image
        hudImage.enabled = true;

        // Start a coroutine to hide the HUD image after a delay
        StartCoroutine(HideDamageHUD());
    }

    private IEnumerator HideDamageHUD()
    {
        // Wait for the specified display duration
        yield return new WaitForSeconds(displayDuration);

        // Hide the HUD image
        hudImage.enabled = false;
    }
}