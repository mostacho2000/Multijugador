using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint; // Objeto Respawn al que se transportará el jugador

    private void Start()
    {
        if (respawnPoint == null)
        {
            Debug.LogError("El punto de respawn no está asignado. Por favor, arrástralo al campo Respawn Point en el inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Muerte")) // Si colisiona con un objeto con el tag "Muerte"
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position; // Transporta al jugador al punto de respawn
            transform.rotation = respawnPoint.rotation; // Ajusta también la rotación al punto de respawn
        }
        else
        {
            Debug.LogWarning("No se pudo teletransportar al jugador porque el punto de respawn no está asignado.");
        }
    }
}