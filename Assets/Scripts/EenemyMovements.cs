using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EenemyMovements : MonoBehaviour
{
    public float speed = 3f;
    public float playerSearchInterval = 0.5f; // Cada cuánto buscar al jugador

    private Transform player;
    private NavMeshAgent navAgent;
    private float lastPlayerSearchTime;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed;
        FindPlayer();
    }

    void Update()
    {
        // Buscar al jugador periódicamente si no lo tenemos o fue destruido
        if (Time.time - lastPlayerSearchTime > playerSearchInterval &&
           (player == null || player.gameObject == null))
        {
            FindPlayer();
            lastPlayerSearchTime = Time.time;
        }

        // Mover solo si tenemos un jugador válido y el NavMeshAgent está listo
        if (player != null && navAgent != null && navAgent.isOnNavMesh)
        {
            navAgent.SetDestination(player.position);
        }
    }

    private void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("Bullet2") || other.CompareTag("MuerteLava"))
        {
            Destroy(gameObject);
        }
    }
}
