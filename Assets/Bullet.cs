using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 40)]
    [SerializeField] private float speed = 30f;

    [Range(1,1000)]
    [SerializeField] private float lifeTime = 1000f;

    private Rigidbody2D rb;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate(){
        rb.velocity = transform.up * speed;
    }
}
