using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject wolfPrefab, wolfEaterPrefab;

    [SerializeField]
    Transform[] spawnPoints;

    [SerializeField]
    int eaterChance=3;

    [SerializeField]
    float spawnTime = 12f;

    [SerializeField]
    float spawnReductionPerWolf = 1f; // next spawn delay

    [SerializeField]
    float minSpawnDelay = 3.5f;

    float currentSpawnTime;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnTime = spawnTime;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            Spawn();
            currentSpawnTime -= spawnReductionPerWolf;
            if (currentSpawnTime<=minSpawnDelay)
            {
                currentSpawnTime = minSpawnDelay;
            }
            timer = Time.time + currentSpawnTime;
        }
    }
    void Spawn()
    {
        if (Random.Range(0,11) > eaterChance)
        {
            Instantiate(wolfPrefab, spawnPoints[Random.Range(0,spawnPoints.Length)].position,Quaternion.identity);
        }
        else
        {
            Instantiate(wolfEaterPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }

    }

}
