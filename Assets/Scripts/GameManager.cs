using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ParticleSystem playerExplosion;

    public PlayerController player;
    public int lives = 3;
    public float respawnTime = 3.0f;

    public int score = 0;

    private void Awake()
    {
        instance = this;
    }

    public void PlayerDied()
    {
        playerExplosion.transform.position = player.transform.position;
        playerExplosion.Play();

        lives--;

        if(lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);
        }
    }

    private void Respawn()
    {
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), 3.0f);
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }

    private void GameOver()
    {
        //...
    }
}
