// AmmoManager.cs

using UnityEngine;

public class weaponAmmoManager
{
    private int totalAmmo;

    public weaponAmmoManager(int initialAmmo)
    {
        totalAmmo = initialAmmo;
    }

    public void ManageAmmo(int ammoConsumed)
    {
        // Subtract the consumed ammo from the total ammo count
        totalAmmo -= ammoConsumed;

        // Ensure the total ammo doesn't go below zero
        totalAmmo = Mathf.Max(totalAmmo, 0);

        Debug.Log($"Ammo managed. Remaining total ammo: {totalAmmo}");
    }

    public int GetTotalAmmo()
    {
        return totalAmmo;
    }

    public void AddAmmo(int ammoToAdd)
    {
        // Add ammo to the total ammo count
        totalAmmo += ammoToAdd;

        Debug.Log($"Ammo added. New total ammo: {totalAmmo}");
    }
}
