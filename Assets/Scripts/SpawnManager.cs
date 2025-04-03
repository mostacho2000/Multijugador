using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject enemyPrefab;
        public string enemyName;
    }

    [Header("Configuración de Enemigos")]
    public EnemyType[] enemyTypes;
    public Transform[] spawnPoints;

    [Header("Configuración de Rondas")]
    public int baseEnemiesPerType = 5;
    public float spawnInterval = 2f;
    public float spawnPositionOffset = 0.5f;
    public float initialSpawnDelay = 5f; // Tiempo de espera inicial antes de empezar a spawnear

    private int currentRound = 0;
    private bool isSpawning = false;
    private bool roundInProgress = false;
    private int enemiesAlive = 0;
    private bool gameStarted = false;

    void Start()
    {
        StartCoroutine(StartGameWithDelay());
    }

    IEnumerator StartGameWithDelay()
    {
        Debug.Log($"Esperando {initialSpawnDelay} segundos antes de comenzar...");
        yield return new WaitForSeconds(initialSpawnDelay);
        gameStarted = true;
        StartNextRound();
    }

    void Update()
    {
        if (!gameStarted) return;

        if (roundInProgress && !isSpawning && enemiesAlive <= 0)
        {
            roundInProgress = false;
            StartNextRound();
        }
    }

    void StartNextRound()
    {
        currentRound++;
        Debug.Log($"Iniciando Ronda: {currentRound}");

        // Actualizar la UI de la ronda
        if (RoundDisplay.Instance != null)
        {
            RoundDisplay.Instance.UpdateRoundDisplay(currentRound);
        }

        int enemiesToSpawn = baseEnemiesPerType * currentRound;
        StartCoroutine(SpawnEnemies(enemiesToSpawn));
    }

    IEnumerator SpawnEnemies(int countPerType)
    {
        isSpawning = true;
        roundInProgress = true;

        for (int i = 0; i < countPerType; i++)
        {
            foreach (var enemyType in enemyTypes)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Vector3 spawnPosition = spawnPoint.position +
                    new Vector3(
                        Random.Range(-spawnPositionOffset, spawnPositionOffset),
                        0,
                        Random.Range(-spawnPositionOffset, spawnPositionOffset)
                    );

                GameObject enemy = Instantiate(enemyType.enemyPrefab, spawnPosition, spawnPoint.rotation);
                enemy.tag = "Enemy";

                EnemyDeathNotifier deathNotifier = enemy.GetComponent<EnemyDeathNotifier>() ?? enemy.AddComponent<EnemyDeathNotifier>();
                deathNotifier.spawnManager = this;

                SetupEnemyNavigation(enemy);

                enemiesAlive++;
                Debug.Log($"Spawned {enemyType.enemyName} at {spawnPoint.name}. Enemigos vivos: {enemiesAlive}");

                yield return new WaitForSeconds(spawnInterval);
            }
        }

        isSpawning = false;
        Debug.Log($"Spawning completado. Enemigos vivos: {enemiesAlive}");
    }

    void SetupEnemyNavigation(GameObject enemy)
    {
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            StartCoroutine(DelayedAgentSetup(agent));
        }
    }

    IEnumerator DelayedAgentSetup(NavMeshAgent agent)
    {
        agent.enabled = false;
        yield return new WaitForEndOfFrame();
        agent.enabled = true;

        if (agent.isOnNavMesh)
        {
            // Esperar un frame adicional para asegurar que el jugador existe
            yield return new WaitForEndOfFrame();
            agent.SetDestination(GetPlayerPositionOrDefault(agent.transform.position));
        }
    }

    Vector3 GetPlayerPositionOrDefault(Vector3 defaultPosition)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player != null ? player.transform.position : defaultPosition + new Vector3(0, 0, 2f);
    }

    public void OnEnemyDeath()
    {
        enemiesAlive--;
        Debug.Log($"Enemigo eliminado. Quedan: {enemiesAlive}");
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }
}