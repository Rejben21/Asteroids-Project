using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public static AsteroidSpawner instance;

    public Asteroid asteroidPrefab;

    public float trajection = 15;
    public float spawnRate = 1;
    public float spawnDistance = 15;

    public int spawnAmount = 1;

    public float timer = 3;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(GameManager.instance.hasStarted)
        {
            timer -= Time.deltaTime * .03f;
        }
    }

    public void StartSpawning()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            float variance = Random.Range(-trajection, trajection);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);

            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
