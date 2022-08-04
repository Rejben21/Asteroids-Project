using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rgBody;
    private Animator anim;

    public Bullet bulletPrefab;

    public float moveSpeed, turnSpeed;
    private bool isMoving;

    private bool moving;
    private float turning;

    public GameObject laser;
    public GameObject shield;

    public float shieldTime;
    private float shieldDuration;
    private bool isShield = false;

    private void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(GameManager.instance.hasStarted)
        {
            moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                isMoving = true;
                laser.SetActive(false);
            }
            else
            {
                isMoving = false;
                laser.SetActive(true);
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && shieldDuration >= shieldTime)
            {
                isShield = true;
                shield.SetActive(true);
                AudioManager.instance.PlaySFX(2);
            }

            if (shieldDuration <= 0)
            {
                isShield = false;
                shield.SetActive(false);
                AudioManager.instance.PlaySFX(1);
            }

            if (!isShield && shieldDuration < shieldTime)
            {
                shieldDuration += Time.deltaTime;
            }
            else
            {
                shieldDuration -= Time.deltaTime;
            }

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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }

            GameManager.instance.shieldbar.value = shieldDuration;
            shield.transform.position = transform.position;
            shield.transform.rotation = transform.rotation;
            anim.SetBool("isMoving", isMoving);
        }
    }

    private void FixedUpdate()
    {
        if(GameManager.instance.hasStarted)
        {
            if (moving)
            {
                rgBody.AddForce(transform.up * moveSpeed);
            }

            if (turning != 0)
            {
                rgBody.AddTorque(turning * turnSpeed);
            }
        }
    }

    public void Shoot()
    {
        AudioManager.instance.PlaySFX(3);
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Asteroid"))
        {

            rgBody.velocity = Vector2.zero;
            rgBody.angularVelocity = 0.0f;

            gameObject.SetActive(false);

            GameManager.instance.PlayerDied();
            AudioManager.instance.PlaySFX(5);
        }
    }
}
