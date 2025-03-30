using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoundSpawner : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject[] enemyPrefabs; // Tus 3 tipos de enemigos
    public Transform[] spawnPoints;   // 10 puntos de spawn
    public float timeBetweenSpawns = 2f;

    [Header("Enemigos por Ronda")]
    public int baseEnemies = 5;      // Enemigos en ronda 1
    public int extraEnemiesPerRound = 2; // Incremento por ronda

    private int currentRound = 0;
    private int enemiesToSpawn;
    private float spawnTimer;
    [SerializeField] private bool isSpawning;
    public void StartRound(int roundNumber)
    {
        currentRound = roundNumber;
        enemiesToSpawn = baseEnemies + (currentRound - 1) * extraEnemiesPerRound;
        isSpawning = true;
        Debug.Log($"Generando {enemiesToSpawn} enemigos para ronda {currentRound}");
    }

    void Update()
    {
        if (isSpawning && enemiesToSpawn > 0)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnEnemy();
                spawnTimer = timeBetweenSpawns;
                enemiesToSpawn--;
            }
        }
    }
    public bool IsSpawning()
    {
        return isSpawning;
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
