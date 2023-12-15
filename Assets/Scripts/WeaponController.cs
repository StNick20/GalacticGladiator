using UnityEngine;
using TMPro;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;

    [Header("Firing")]
    public bool ableToFire = true;
    public int maxBullet = 0;
    public int bulletCount = 0;
    public int pistolAmmo = 30;
    public int maxPistol = 60;
    public int shotgunAmmo = 10;
    public int maxShotgun = 20;
    public int reloadAmount = 0;
    public TMP_Text bulletCounter;

    private Weapon currentWeapon;  // Track the current weapon instance
    string selectedWeapon = "Shotgun";
    // Set the default weapon to Shotgun in Start or Awake
    void Start()
    {

        SetWeapon<Shotgun>();
        UpdateBulletCounter();
        maxBullet = maxShotgun;
        bulletCount = shotgunAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon<Pistol>();
            selectedWeapon = "Pistol";
            bulletCount = pistolAmmo;
            maxBullet = maxPistol;
            UpdateBulletCounter();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon<Shotgun>();
            selectedWeapon = "Shotgun";
            bulletCount = shotgunAmmo;
            maxBullet = maxShotgun;
            UpdateBulletCounter();
        }
        /*else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R");
           if(selectedWeapon == "Shotgun"){
            bulletCount = 10;
            UpdateBulletCounter();
           }
           if(selectedWeapon == "Pistol"){
            bulletCount = 30;
            UpdateBulletCounter();
           }
        }*/

        if(selectedWeapon == "Shotgun")
        {
            shotgunAmmo = bulletCount;
            UpdateBulletCounter();
        }
        else if(selectedWeapon == "Pistol")
        {
            pistolAmmo = bulletCount;
            UpdateBulletCounter();
        }


        // Fire when left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && ableToFire && bulletCount > 0)
        {
            // Shoot
            currentWeapon.Shoot();
            bulletCount--;
            UpdateBulletCounter();
            StartCoroutine(shootCooldown());
        }
    }
    void ReloadCurrentWeapon(){
        if (currentWeapon != null)
        {
            currentWeapon.Reload();
            UpdateBulletCounter();
        }
    }


    private IEnumerator shootCooldown()
    {
        ableToFire = false;
        yield return new WaitForSeconds(0.3f);
        ableToFire = true;
    }

    void UpdateBulletCounter()
    {
        if (bulletCounter != null)
        {
            bulletCounter.text = bulletCount.ToString() + "/" + maxBullet.ToString();
        }
    }

    public void SetWeapon<T>() where T : Weapon
    {
        // Destroy the previous weapon script component
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        // Add the new weapon component to the same GameObject
        currentWeapon = gameObject.AddComponent<T>();
        currentWeapon.firePoint = firePoint;
        currentWeapon.bulletPrefab = bulletPrefab;
        currentWeapon.bulletForce = bulletForce;

        // Update bullet counter when switching weapons
        /*bulletCount = Mathf.Min(bulletCount, currentWeapon.GetMaxAmmo());
        UpdateBulletCounter();
        */

        // Initialize the new weapon
        currentWeapon.Initialize();
    }

    public void Reload()
    {
        shotgunAmmo += 5;
        if(shotgunAmmo > maxShotgun) { shotgunAmmo = maxShotgun; }
        pistolAmmo += 10;
        if(pistolAmmo > maxPistol) { pistolAmmo = maxPistol; }

        if(selectedWeapon == "Shotgun")
        {
            bulletCount = shotgunAmmo;
        }
        else if(selectedWeapon == "Pistol")
        {
            bulletCount = pistolAmmo;
        }
        Debug.Log("Reload");
        UpdateBulletCounter();
    }
}

// ------------------------------------------------------------------------------------------------------------------- //

public abstract class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;

    // Separate ammo count for each weapon
    protected int ammoCount = 0;


    public int reloadAmount = 0;

    // Method to get the maximum ammo count for the weapon
    public virtual int GetMaxAmmo()
    {
        return 0; // Default value, override in subclasses
    }

    public virtual void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Bullet instantiated");
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = bullet.AddComponent<Rigidbody2D>();
        }
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public int GetCurrentAmmo()
    {
        return ammoCount;
    }

    public void SetAmmoCount(int count)
    {
        ammoCount = count;
    }

    public void Initialize()
    {
        ammoCount = GetMaxAmmo();
    }

    public virtual void Reload()
    {

    
        // Calculate the amount of ammo needed to reach the maximum capacity
        int remainingAmmoSpace = GetMaxAmmo() - GetCurrentAmmo();
    
        // Calculate the amount of ammo to reload (e.g., reloadAmount is the number of bullets reloaded per reload)
        int ammoToReload = Mathf.Min(remainingAmmoSpace, reloadAmount);

        // Perform the actual reload
        ammoCount += ammoToReload;

        // Log the reloading action (you can replace this with your own feedback mechanism)
        Debug.Log($"Reloading... Current Ammo: {GetCurrentAmmo()}");

    }

}

// -----------------------------------  PISTOL CONFIG ------------------------------------------------------------------------- //

public class Pistol : Weapon
{
    public int pelletCount = 1;
    public int pistolBulletCount = 15;
    public float spreadAngle = 0f;

    // Override the GetMaxAmmo method
    public override int GetMaxAmmo()
    {
        return pistolBulletCount;
    }

    // Override the Reload method
    public override void Reload()
    {
        int remainingAmmoSpace = pistolBulletCount - GetCurrentAmmo();
        int ammoToReload = Mathf.Min(remainingAmmoSpace, reloadAmount);
        SetAmmoCount(GetCurrentAmmo() + ammoToReload);

        Debug.Log($"Reloading... Current Ammo: {GetCurrentAmmo()}");
    }


    // Override the Shoot method
    public override void Shoot()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // Calculate the spread angle for each pellet
            float angle = Random.Range(-spreadAngle, spreadAngle);

            // Apply the spread to the rotation of the bullet
            Quaternion spreadRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Quaternion finalRotation = firePoint.rotation * spreadRotation;

            // Instantiate the bullet with the adjusted rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, finalRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        }
    }
}



// -----------------------------------  SHOTGUN CONFIG ------------------------------------------------------------------------- //


public class Shotgun : Weapon
{
    private weaponAmmoManager AmmoManager;

    public int pelletCount = 5;
    public int shotgunBulletCount = 10;
    public float spreadAngle = 15f;

    // Override the GetMaxAmmo method
    public override int GetMaxAmmo()
    {
        return shotgunBulletCount;
    }

    // Override the Reload method
    public Shotgun()
    {
        // Assuming you want to initialize the AmmoManager with some initial ammo count
        AmmoManager = new weaponAmmoManager(initialAmmo: 50);
    }

    public override void Reload()
    {
        // Call the Reload method from the base class (Weapon)
        base.Reload();

        // Assuming each shotgun shot consumes 1 ammo
        int ammoConsumed = 1;

        // Call the ManageAmmo method from the AmmoManager class
        AmmoManager.ManageAmmo(ammoConsumed);
    }

    // Override the Shoot method
    public override void Shoot()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // Calculate the spread angle for each pellet
            float angle = Random.Range(-spreadAngle, spreadAngle);

            // Apply the spread to the rotation of the bullet
            Quaternion spreadRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Quaternion finalRotation = firePoint.rotation * spreadRotation;

            // Instantiate the bullet with the adjusted rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, finalRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        }
    }
}



// -----------------------------------  LAUNCHER CONFIG ------------------------------------------------------------------------- //







/*public class Launcher : Weapon
{
    public float explosionRadius = 5f;
    public LayerMask damageableLayers;

    public override void Shoot()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the rigidbody of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = projectile.AddComponent<Rigidbody2D>();
        }

        // Apply force to the projectile
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        // Apply area-of-effect damage after a delay
        Invoke("Explode", 2f);
    }

private void Explode()
{
    // Perform area-of-effect damage
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayers);

    foreach (Collider2D collider in colliders)
    {
        // Deal damage to the objects within the explosion radius
        // You can implement your own logic for dealing damage here
        Debug.Log("Dealing damage to: " + collider.gameObject.name);
    }

    // Optional: Instantiate explosion visual/audio effects
    // Instantiate(explosionPrefab, transform.position, Quaternion.identity);

    // Destroy the launcher projectile
    Destroy(gameObject);  // Destroy the projectile, not the entire launcher
}}
*/
