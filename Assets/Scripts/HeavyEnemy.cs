using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : MonoBehaviour
{
    public GameObject player;
    public int minSpeed = 1;  // Set your minimum speed
    public int maxSpeed = 3;  // Set your maximum speed
    public float distanceBetween;

    private float distance;
    public int randomSpeed;

    public int meleeDamage;

    // Start is called before the first frame update
    void Start()
    {
        randomSpeed = Random.Range(minSpeed, maxSpeed);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, randomSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the collided object is another enemy on the same layer
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.layer == gameObject.layer)
        {
            // Adjust the position to prevent overlap
            Vector2 avoidPos = transform.position + (transform.position - other.transform.position).normalized * 60f;
            transform.position = Vector2.MoveTowards(transform.position, avoidPos, 10 * Time.deltaTime);
        }

        if (other.gameObject.CompareTag("Player")/* && other.gameObject.layer == gameObject.layer*/)
        {
            // Adjust the position to prevent overlap
            Vector2 avoidPos = transform.position + (transform.position - other.transform.position).normalized * 60f;
            transform.position = Vector2.MoveTowards(transform.position, avoidPos, 10 * Time.deltaTime);

            player.GetComponent<PlayerHealth>().PlayerTakeDamage(meleeDamage);
        }
    }
}
