using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float checkInterval = 1f;
    public string enemyTag = "Enemy";

    [Header("Referencias")]
    public RoundSpawner spawner; // Aseg�rate de asignar esto en el Inspector
    public Text roundText;
    public Text enemiesLeftText;

    private int currentRound = 0;
    private float checkTimer;

    void Start()
    {
        // Si no se asign� manualmente, intenta encontrar el spawner
        if (spawner == null)
        {
            spawner = FindObjectOfType<RoundSpawner>();
            if (spawner == null)
            {
                Debug.LogError("No se encontr� el RoundSpawner en la escena!");
                return;
            }
        }

        StartNextRound();
    }

    void Update()
    {
        if (spawner == null) return;

        checkTimer -= Time.deltaTime;
        if (checkTimer <= 0f)
        {
            CheckEnemies();
            checkTimer = checkInterval;
        }
    }

    void CheckEnemies()
    {
        if (spawner == null) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        if (enemiesLeftText != null)
            enemiesLeftText.text = $"Enemigos: {enemies.Length}";

        // Verificaci�n m�s segura
        bool isSpawning = spawner.GetComponent<RoundSpawner>().IsSpawning();
        if (enemies.Length == 0 && !isSpawning)
        {
            StartNextRound();
        }
    }

    void StartNextRound()
    {
        if (spawner == null) return;

        currentRound++;
        if (roundText != null)
            roundText.text = $"Ronda: {currentRound}";

        spawner.StartRound(currentRound);
    }
}
