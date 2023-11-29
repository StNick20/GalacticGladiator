using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
Rigidbody2D body;
public GameObject Bullet;


float horizontal;
float vertical;
float moveLimiter = 0.7f;

public float runSpeed = 20.0f;
public Vector2 mousePos;

void Start ()
{
   body = GetComponent<Rigidbody2D>();
}

void Update()
{
   horizontal = Input.GetAxisRaw("Horizontal");
   vertical = Input.GetAxisRaw("Vertical");
   mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

   float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
   transform.localRotation = Quaternion.Euler(0, 0, angle);

   if (Input.GetMouseButtonDown(0)){
      Shoot();
   }
}

void FixedUpdate()
{
   if (horizontal != 0 && vertical != 0)
   {
      horizontal *= moveLimiter;
      vertical *= moveLimiter;
   } 

   body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
}

private void Shoot() {
      Instantiate(Bullet, transform.position, transform.rotation);
}
}
