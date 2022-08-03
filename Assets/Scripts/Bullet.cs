using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    private Rigidbody2D rgBody;

    // Start is called before the first frame update
    private void Awake()
    {
        rgBody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        rgBody.AddForce(direction * moveSpeed);
        Destroy(this.gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
