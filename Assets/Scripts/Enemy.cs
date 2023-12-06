using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject player;
    public int minSpeed = 1;  // Set your minimum speed
    public int maxSpeed = 7;  // Set your maximum speed
    public float distanceBetween;

    private float distance;
    public int randomSpeed;
    
    void Start()
    {
        randomSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween && distance > 1f)  // Adjust the threshold as needed
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, randomSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is another enemy on the same layer
        if (other.CompareTag("Enemy") && other.gameObject.layer == gameObject.layer)
        {
            // Adjust the position to prevent overlap
            Vector2 avoidPos = transform.position + (transform.position - other.transform.position).normalized * 30f;
            transform.position = Vector2.MoveTowards(transform.position, avoidPos, randomSpeed * Time.deltaTime);
        }

        if (other.CompareTag("Player") && other.gameObject.layer == gameObject.layer)
        {
            // Adjust the position to prevent overlap
            Vector2 avoidPos = transform.position + (transform.position - other.transform.position).normalized * 30f;
            transform.position = Vector2.MoveTowards(transform.position, avoidPos, randomSpeed * Time.deltaTime);
        }
    }
}
