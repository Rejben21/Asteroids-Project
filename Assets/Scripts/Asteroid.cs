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

    public GameObject distanceSplit;

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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (size <= 1.5f)
            {

            }
            else if(size <= 2f && size > 1.5f)
            {
                Split();
                Split();
            }
            else if(size <= 3.5f && size > 2f)
            {
                Split();
                Split();
                Split();
            }

            GameManager.instance.score++;
            AudioManager.instance.PlaySFX(6);
            Instantiate(distanceSplit, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.CompareTag("Shield"))
        {
            AudioManager.instance.PlaySFX(0);
        }
    }

    private void Split()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * moveSpeed);
    }
}
