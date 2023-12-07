using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    
    Rigidbody2D body;
    [Header("Firing")]
    public GameObject Bullet;
    public Transform firingPoint;
    public bool ableToFire = true;
    public int MAXBULLET = 200;
    public int bulletCount = 0;
    public int reloadAmount = 20;
    public TMP_Text hudBulletCount;


    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    [Header("Movement")]
    public float runSpeed = 20.0f;
    public Vector2 mousePos;
    public bool boost = false;

    [Header("Pausing")]
    public GameObject pauseObject;
    public GameObject hud;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hudBulletCount.text = bulletCount + "/200";
    }

    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        transform.localRotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0) && ableToFire && bulletCount > 0)
        {
            Shoot();
        }
        else if (Input.GetMouseButton(0) && ableToFire && bulletCount > 0)
        {
            Shoot();
        }

        //checks if escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc key pressed");

            hud.SetActive(false);
            pauseObject.SetActive(true);
            enabled = false;
        }


    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        if (boost == true)
        {
            body.velocity = new Vector2(horizontal * (runSpeed * 2), vertical * (runSpeed * 2));
        }
        else if (boost == false)
        {
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }
        
    }

    private void Shoot() 
    {
        Instantiate(Bullet, firingPoint.position, firingPoint.rotation);
        StartCoroutine(Cooldown(0));
        bulletCount -= 1;
        hudBulletCount.text = bulletCount + "/200";
    }

    public void AmmoPickUp()
    {
        bulletCount += reloadAmount;
        if (bulletCount > 200) { bulletCount = 200; }
        hudBulletCount.text = bulletCount + "/200";
    }

    public IEnumerator Cooldown(int cooldownID)
    {
        switch (cooldownID)
        {
            case 0:
                ableToFire = false;
                yield return new WaitForSeconds(0.3f);
                ableToFire = true;
                break;
            case 1:
                boost = true;
                yield return new WaitForSeconds(3.0f);
                boost = false;
                break;
        }
        
    }
}
