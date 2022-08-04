using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ParticleSystem playerExplosion;

    public PlayerController player;
    public int lives = 3;
    public Text livesText;
    public float respawnTime = 3.0f;

    public int score = 0;
    public Text scoreText;

    public GameObject menu, gameOver;
    public bool hasStarted = false;

    public Slider shieldbar;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();
    }

    public void PlayerDied()
    {
        playerExplosion.transform.position = player.transform.position;
        playerExplosion.Play();

        lives--;

        if(lives <= 0)
        {
            gameOver.SetActive(true);
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
        AudioManager.instance.PlaySFX(4);
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void StartGame()
    {
        hasStarted = true;
        menu.SetActive(false);
        AsteroidSpawner.instance.StartSpawning();
        AudioManager.instance.PlaySFX(4);
    }
}
