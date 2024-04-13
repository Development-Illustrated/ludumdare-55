using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnRate = 1.0f;

    [SerializeField] int waveSize= 10;

    [SerializeField] float waveSpawnDelay = 5.0f;

    int waveCount = 0;
    int waveEnemyCount = 0;
    float nextSpawnTime = 0.0f;
    Transform nextSpawnPoint;

    void Update()
    {
        if (waveEnemyCount >= waveSize)
        {
            StartNewWave();
        }   

        if (ShouldSpawn())
        {
            nextSpawnPoint = GetSpawnPoint();
            SpawnEnemy(nextSpawnPoint, waveCount);
            waveEnemyCount++;
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    Transform GetSpawnPoint()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }

    void StartNewWave()
    {
        waveCount++;
        waveEnemyCount = 0;
        nextSpawnTime = Time.time + waveSpawnDelay;
    }

    bool ShouldSpawn()
    {
        return Time.time > nextSpawnTime;
    }

    void SpawnEnemy(Transform spawnPoint, int seed)
    {
        BodyAssembly body = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<BodyAssembly>();
        body.CreateBody(body.transform, seed);
    }
}
