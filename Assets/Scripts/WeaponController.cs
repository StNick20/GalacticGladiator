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
    public int reloadAmount = 0;
    public TMP_Text bulletCounter;

    private Weapon currentWeapon;  // Track the current weapon instance

    // Set the default weapon to Shotgun in Start or Awake
    void Start()
    {
        SetWeapon<Shotgun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon<Pistol>();
        }
        // Switch weapons when the key is pressed
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon<Shotgun>();
        }
        
        /*
        else if (Input.GetKeyDown(KeyCode.Alpha3)){
            SetWeapon<Launcher>();
        }*/


        // Fire when left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && ableToFire)
        {
            Debug.Log("Left Click");
            // Shoot
            currentWeapon.Shoot();
            StartCoroutine(shootCooldown());
        }
    }

    public void Reload()
    {
        bulletCount += reloadAmount;
        if (bulletCount > maxBullet)
        {
            bulletCount = maxBullet;
        }
    }

    private IEnumerator shootCooldown()
    {
        ableToFire = false;
        yield return new WaitForSeconds(0.3f);
        ableToFire = true;
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
    }
}

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;

   public virtual void Shoot()
{
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Debug.Log("Bullet instantiated");
    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    if (rb == null)
    {
        rb = bullet.AddComponent<Rigidbody2D>();
    }

    rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
}
}

public class Pistol : Weapon
{
    public int pelletCount = 1;
    public float spreadAngle = 0f; // Adjust the spread angle as needed
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

public class Shotgun : Weapon
{
    public int pelletCount = 5;
    public float spreadAngle = 15f; // Adjust the spread angle as needed

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
