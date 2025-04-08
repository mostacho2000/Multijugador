using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimateController : MonoBehaviour
{
    public GameObject lavaRainSystem; // Asigna el sistema de lluvia de lava en el Inspector
    public GameObject snowStormSystem; // Asigna el sistema de tormenta de nieve en el Inspector

    private bool isClimateActive = false;
    private float climateDuration = 60f; // 60 segundos de clima activo
    private float cooldownDuration = 180f; // 3 minutos de espera

    private void Start()
    {
        // Aseguramos que ambos sistemas estén desactivados al inicio
        if (lavaRainSystem != null) lavaRainSystem.SetActive(false);
        if (snowStormSystem != null) snowStormSystem.SetActive(false);

        // Iniciamos la corrutina del ciclo de clima
        StartCoroutine(ClimateCycle());
    }

    private IEnumerator ClimateCycle()
    {
        while (true)
        {
            // Espera el tiempo de cooldown si no es el primer ciclo
            if (isClimateActive)
            {
                yield return new WaitForSeconds(cooldownDuration);
            }

            // Elige un clima aleatorio
            int randomClimate = Random.Range(0, 2); // 0 o 1

            // Activa el clima correspondiente
            if (randomClimate == 0)
            {
                ActivateLavaRain();
            }
            else
            {
                ActivateSnowStorm();
            }

            isClimateActive = true;

            // Espera la duración del clima activo
            yield return new WaitForSeconds(climateDuration);

            // Desactiva todos los climas
            DeactivateAllClimates();
        }
    }

    private void ActivateLavaRain()
    {
        DeactivateAllClimates();
        if (lavaRainSystem != null)
        {
            lavaRainSystem.SetActive(true);
            Debug.Log("Lluvia de lava activada");
        }
    }

    private void ActivateSnowStorm()
    {
        DeactivateAllClimates();
        if (snowStormSystem != null)
        {
            snowStormSystem.SetActive(true);
            Debug.Log("Tormenta de nieve activada");
        }
    }

    private void DeactivateAllClimates()
    {
        if (lavaRainSystem != null) lavaRainSystem.SetActive(false);
        if (snowStormSystem != null) snowStormSystem.SetActive(false);
        isClimateActive = false;
    }

    // Métodos públicos para modificar tiempos si es necesario
    public void SetClimateDuration(float newDuration)
    {
        climateDuration = newDuration;
    }

    public void SetCooldownDuration(float newDuration)
    {
        cooldownDuration = newDuration;
    }
}