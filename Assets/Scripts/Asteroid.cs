using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;

    public float moveSpeed = 50;
    public float size = 1.0f;
    public float minSize = 1f, maxSize = 3.5f;

    public float lifeTime = 30.0f;

    private SpriteRenderer sR;
    private Rigidbody2D rgBody;

    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
        rgBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        sR.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * size;

        rgBody.mass = size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        rgBody.AddForce(direction * moveSpeed);

        Destroy(this.gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
