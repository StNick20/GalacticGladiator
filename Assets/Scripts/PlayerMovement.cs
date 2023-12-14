using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //reference for the rigidbody component
    Rigidbody2D body;


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
        //get input for movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //get the mouse position in the world space
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //calculate the angle between the player and the mouse position
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        
        //rotate the player towards the mouse position
        transform.localRotation = Quaternion.Euler(0, 0, angle);

        
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
}