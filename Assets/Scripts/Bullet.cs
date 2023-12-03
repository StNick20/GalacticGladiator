using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     private float speed = 30f;
     private float lifeTime = 3f;

    private Rigidbody2D rb;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate(){
        rb.velocity = transform.up * speed;
    }
}
