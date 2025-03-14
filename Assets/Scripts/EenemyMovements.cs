using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EenemyMovements : MonoBehaviour
{
    public float speed = 3f; // Velocidad del enemigo
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Mover al enemigo hacia el jugador
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
