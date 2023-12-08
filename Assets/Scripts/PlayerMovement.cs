using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //reference for the rigidbody component
    Rigidbody2D body;

    [Header("Firing")]
    public GameObject Bullet; //prefab for the bullet
    public Transform firingPoint; //transform representing the firing point
    public bool ableToFire = true; //boolean to check if the player is able to fire
    public int maxBullet = 200; //maximus bullet count
    public int bulletCount = 100; //current bullet count
    public int reloadAmount = 30; //amount of bullets to reload
    public TMP_Text bulletCounter; //text component displaying the bullet count


    //variables for player movement
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    [Header("Movement")]
    public float runSpeed = 20.0f; //speed of the player
    public Vector2 mousePos;//vector representing mouse position

    //start is called before the first frame update
    void Start()
    {
        //get the rigidbody component
        body = GetComponent<Rigidbody2D>();
    }

    //update is called once per frame
    void Update()
    {
        //update the bullet count text
        bulletCounter.text = bulletCount + "/" + maxBullet;

        //get input for movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //get the mouse position in the world space
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //calculate the angle between the player and the mouse position
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        
        //rotate the player towards the mouse position
        transform.localRotation = Quaternion.Euler(0, 0, angle);

        //check if the fire button  is pressed and the player is able to fire
        if (Input.GetMouseButtonDown(0) && ableToFire)
        {
            Shoot();
        }
        else if (Input.GetMouseButton(0) && ableToFire)
        {
            Shoot();
        }

    }

    // FixedUpdate is called at a fixed time interval and is used for physics calculations
    void FixedUpdate()
    {
        //limit diagonal movement speed
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        //set the velocity of the player based on input and speed
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    //method for shooting
    private void Shoot()
    {

        //instantiate a bullet at the firing point
        Instantiate(Bullet, firingPoint.position, firingPoint.rotation);

        //decrease the bullet count
        bulletCount -= 1;

        //start the cooldown for shooting
        StartCoroutine(shootCooldown());
    }

    //method for reloading
    public void Reload()
    {
        //increase the bullet count by the reload amount and cap it at the maximum
        bulletCount += reloadAmount;
        if (bulletCount > maxBullet)
        {
            bulletCount = maxBullet;
        }
    }

    //coroutine for the shooting cooldown
    private IEnumerator shootCooldown()
    {
        //set ableToFire to false and wait for a specified time
        ableToFire = false;
        yield return new WaitForSeconds(0.3f);

        //set ableToFire back to true
        ableToFire = true;
    }
}
