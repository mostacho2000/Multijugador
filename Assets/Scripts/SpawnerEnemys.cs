using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemys : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo
    public float spawnInterval = 10f; // Tiempo entre cada spawn

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}