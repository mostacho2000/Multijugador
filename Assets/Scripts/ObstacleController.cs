using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class ObstacleController : MonoBehaviour
{
    [Header("Configuración de Lluvia de Lava")]
    public GameObject[] lavaObstacles;
    public ParticleSystem lavaRainParticles;
    public float lavaDamagePerSecond = 10f;

    [Header("Configuración de Tormenta de Nieve")]
    public ParticleSystem snowStormParticles;
    public float movementSpeedReduction = 0.5f; // Reduce velocidad a la mitad (0.5 = 50%)
    public float snowStormDuration = 60f;

    [Header("Configuración General")]
    public float activationTime = 20f;
    public float cooldownBetweenClimates = 180f;

    private NavMeshSurface navMeshSurface;
    private bool climateActive = false;
    private float originalPlayerSpeed;
    private float[] originalEnemySpeeds;
    private GameObject player;
    private GameObject[] enemies;

    void Start()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Almacenar velocidades originales
        if (player != null)
        {
            originalPlayerSpeed = player.GetComponent<PlayerMovement>().speed; // Ajusta según tu componente
        }

        originalEnemySpeeds = new float[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                var enemyNavMesh = enemies[i].GetComponent<NavMeshAgent>();
                if (enemyNavMesh != null)
                {
                    originalEnemySpeeds[i] = enemyNavMesh.speed;
                }
            }
        }

        // Desactivar todo al inicio
        DeactivateAllClimates();

        // Iniciar ciclo de clima
        StartCoroutine(ClimateCycle());
    }

    IEnumerator ClimateCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(activationTime);

            // Elegir clima aleatorio
            bool isSnowStorm = Random.Range(0, 2) == 0;

            if (isSnowStorm)
            {
                yield return StartCoroutine(ActivateSnowStorm());
            }
            else
            {
                yield return StartCoroutine(ActivateLavaRain());
            }

            // Cooldown entre climas
            yield return new WaitForSeconds(cooldownBetweenClimates);
        }
    }

    IEnumerator ActivateLavaRain()
    {
        DeactivateAllClimates();
        climateActive = true;

        // Activar obstáculos de lava
        foreach (GameObject obstacle in lavaObstacles)
        {
            if (obstacle != null) obstacle.SetActive(true);
        }

        // Activar partículas
        if (lavaRainParticles != null) lavaRainParticles.Play();

        // Actualizar NavMesh
        if (navMeshSurface != null) navMeshSurface.BuildNavMesh();

        Debug.Log("Lluvia de lava activada");
        yield return new WaitForSeconds(snowStormDuration); // Usamos la misma duración

        DeactivateAllClimates();
    }

    IEnumerator ActivateSnowStorm()
    {
        DeactivateAllClimates();
        climateActive = true;

        // Activar partículas de nieve
        if (snowStormParticles != null) snowStormParticles.Play();

        // Reducir velocidad de jugador y enemigos
        ReduceMovementSpeeds();

        Debug.Log("Tormenta de nieve activada");
        yield return new WaitForSeconds(snowStormDuration);

        // Restaurar velocidades
        RestoreMovementSpeeds();
        DeactivateAllClimates();
    }

    void DeactivateAllClimates()
    {
        // Desactivar lava
        foreach (GameObject obstacle in lavaObstacles)
        {
            if (obstacle != null) obstacle.SetActive(false);
        }

        // Detener partículas
        if (lavaRainParticles != null) lavaRainParticles.Stop();
        if (snowStormParticles != null) snowStormParticles.Stop();

        // Restaurar velocidades por si acaso
        RestoreMovementSpeeds();

        // Actualizar NavMesh
        if (navMeshSurface != null) navMeshSurface.BuildNavMesh();

        climateActive = false;
    }

    void ReduceMovementSpeeds()
    {
        // Reducir velocidad del jugador
        if (player != null)
        {
            var playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.speed *= movementSpeedReduction;
            }
        }

        // Reducir velocidad de enemigos
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                var enemyNavMesh = enemies[i].GetComponent<NavMeshAgent>();
                if (enemyNavMesh != null)
                {
                    enemyNavMesh.speed = originalEnemySpeeds[i] * movementSpeedReduction;
                }
            }
        }
    }

    void RestoreMovementSpeeds()
    {
        // Restaurar jugador
        if (player != null)
        {
            var playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.speed = originalPlayerSpeed;
            }
        }

        // Restaurar enemigos
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                var enemyNavMesh = enemies[i].GetComponent<NavMeshAgent>();
                if (enemyNavMesh != null)
                {
                    enemyNavMesh.speed = originalEnemySpeeds[i];
                }
            }
        }
    }

    // Método para daño por lava (llamar desde Update en objetos afectados)
    public bool IsLavaActive()
    {
        return climateActive && lavaRainParticles != null && lavaRainParticles.isPlaying;
    }
}
