using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EenemyMovements : MonoBehaviour
{
    public float speed = 3f; // Velocidad del enemigo
    private Transform player;
    private NavMeshAgent navAgent; // Reference to the NavMeshAgent component

    void Start()
    {
        // Get the NavMeshAgent component
        navAgent = GetComponent<NavMeshAgent>();

        // Set the agent's speed
        navAgent.speed = speed;

        // Find the player
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player != null && navAgent != null && navAgent.isOnNavMesh)
        {
            // Set the destination to the player's position
            navAgent.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que colisionó tiene el tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // Destruir el enemigo
            Destroy(gameObject);
        }
        if (other.CompareTag("Bullet2"))
        {
            // Destruir el enemigo
            Destroy(gameObject);
        }
    }
}
