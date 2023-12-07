using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     public float speed = 50f;
     public float lifeTime = 3f;

    private Rigidbody2D rb;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, lifeTime);
    }

    private void FixedUpdate(){
        rb.velocity = transform.up * speed;
    }
}
