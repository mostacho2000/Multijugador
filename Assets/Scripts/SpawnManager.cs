using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Transform[] spawnPoints; // Asigna tus 10 objetos vacíos aquí

    [Header("Configuración de Rondas")]
    public int baseEnemiesPerType = 5; // Enemigos base por tipo (5 en primera ronda)
    public float spawnInterval = 2f; // Tiempo entre spawns

    private int currentRound = 0;
    private bool isSpawning = false;
    private bool roundInProgress = false;
    private int enemiesAlive = 0;

    void Start()
    {
        StartNextRound();
    }

    void Update()
    {
        // Verificar si la ronda está en progreso y no hay enemigos vivos
        if (roundInProgress && !isSpawning && enemiesAlive <= 0)
        {
            roundInProgress = false;
            StartNextRound();
        }
    }

    void StartNextRound()
    {
        currentRound++;
        Debug.Log("Iniciando Ronda: " + currentRound);

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
                // Seleccionar un punto de spawn aleatorio
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Instanciar el enemigo
                GameObject enemy = Instantiate(enemyType.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                enemy.tag = "Enemy"; // Asegurar que tiene el tag correcto

                // Añadir componente para detectar muerte si no lo tiene
                EnemyDeathNotifier deathNotifier = enemy.GetComponent<EnemyDeathNotifier>();
                if (deathNotifier == null)
                {
                    deathNotifier = enemy.AddComponent<EnemyDeathNotifier>();
                }
                deathNotifier.spawnManager = this;

                enemiesAlive++;
                Debug.Log("Spawned " + enemyType.enemyName + " at " + spawnPoint.name + ". Enemigos vivos: " + enemiesAlive);

                // Esperar antes del próximo spawn
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        isSpawning = false;
        Debug.Log("Spawning completado. Enemigos vivos: " + enemiesAlive);
    }

    public void OnEnemyDeath()
    {
        enemiesAlive--;
        Debug.Log("Enemigo eliminado. Quedan: " + enemiesAlive);
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }
}
