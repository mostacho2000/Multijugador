using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathNotifier : MonoBehaviour
{
    [HideInInspector] public SpawnManager spawnManager;

    void OnDestroy()
    {
        if (spawnManager != null && gameObject.scene.isLoaded)
        {
            spawnManager.OnEnemyDeath();
        }
    }
}
