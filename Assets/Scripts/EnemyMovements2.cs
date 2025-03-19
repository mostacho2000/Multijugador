using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements2 : MonoBehaviour
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

    // M�todo que se llama cuando ocurre una colisi�n
    /* private void OnCollisionEnter(Collision collision)
     {
         // Verificar si el objeto con el que colision� tiene el tag "Bullet"
         if (collision.gameObject.CompareTag("Bullet"))
         {
             // Destruir el enemigo
             Destroy(gameObject);
         }
     }*/

    // Si est�s utilizando triggers en lugar de colisiones, usa este m�todo

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que colision� tiene el tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // Destruir el enemigo
            Destroy(gameObject);
        }
    }

}
