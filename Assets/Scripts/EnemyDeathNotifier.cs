using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathNotifier : MonoBehaviour
{
    public SpawnManager spawnManager;

    void OnDestroy()
    {
        // Notificar al SpawnManager cuando el enemigo es destruido
        if (spawnManager != null)
        {
            spawnManager.OnEnemyDeath();
        }
    }
}
