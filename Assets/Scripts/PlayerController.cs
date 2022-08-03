using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rgBody;

    public Bullet bulletPrefab;

    public float moveSpeed, turnSpeed;

    private bool moving;
    private float turning;

    private void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turning = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turning = -1.0f;
        }
        else
        {
            turning = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rgBody.AddForce(transform.up * moveSpeed);
            //fire animation
            //particles
            //fire sound
        }

        if (turning != 0)
        {
            rgBody.AddTorque(turning * turnSpeed);
        }
    }

    public void Shoot()
    {
        //shoot sound
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }
}
