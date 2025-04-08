using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class ObstacleController : MonoBehaviour
{
    [Tooltip("Array de obstáculos que serán controlados")]
    public GameObject[] obstacles;

    [Tooltip("Tiempo en segundos antes de activar los obstáculos")]
    public float activationTime = 20f;

    private NavMeshSurface navMeshSurface;
    private bool obstaclesActivated = false;

    void Start()
    {
        // Obtener la referencia al NavMeshSurface en la escena
        navMeshSurface = FindObjectOfType<NavMeshSurface>();

        if (navMeshSurface == null)
        {
            Debug.LogError("No se encontró un NavMeshSurface en la escena!");
        }

        // Asegurarse de que todos los obstáculos están desactivados al inicio
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle != null)
            {
                obstacle.SetActive(false);
            }
        }

        // Iniciar la corutina para activar los obstáculos
        StartCoroutine(ActivateObstaclesAfterDelay());
    }

    IEnumerator ActivateObstaclesAfterDelay()
    {
        yield return new WaitForSeconds(activationTime);

        // Activar todos los obstáculos
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle != null)
            {
                obstacle.SetActive(true);
            }
        }

        obstaclesActivated = true;

        // Actualizar el NavMesh
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
            Debug.Log("Obstáculos activados y NavMesh actualizado");
        }
    }

    // Método público para activar manualmente los obstáculos si es necesario
    public void ActivateObstaclesManually()
    {
        if (!obstaclesActivated)
        {
            StopAllCoroutines();
            StartCoroutine(ActivateObstaclesAfterDelay(0f));
        }
    }

    // Versión sobrecargada para activar con tiempo personalizado
    IEnumerator ActivateObstaclesAfterDelay(float customDelay)
    {
        yield return new WaitForSeconds(customDelay);

        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle != null)
            {
                obstacle.SetActive(true);
            }
        }

        obstaclesActivated = true;

        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
    }
}
