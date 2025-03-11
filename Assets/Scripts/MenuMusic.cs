using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    public AudioClip musicForScenes0to3; // M�sica para las escenas 1 a 4
    public AudioClip musicForScene4; // M�sica para la escena 5
    public float halfVolume = 0.5f; // Volumen a la mitad

    private AudioSource audioSource;

    void Awake()
    {
        // Este bloque de c�digo asegura que solo exista una instancia del MenuMusicManager
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex >= 0 && scene.buildIndex <= 3)
        {
            // Si la escena est� entre la 1 y la 4, reproducir la m�sica para esas escenas
            if (audioSource.clip != musicForScenes0to3 || !audioSource.isPlaying)
            {
                audioSource.clip = musicForScenes0to3;
                audioSource.volume = halfVolume; // Establecer el volumen a la mitad
                audioSource.loop = true; // Activar repetici�n
                audioSource.Play();
            }
        }
        else if (scene.buildIndex >= 4)
        {
            // Si la escena es la 5, reproducir la m�sica diferente para esa escena
            if (audioSource.clip != musicForScene4 || !audioSource.isPlaying)
            {
                audioSource.clip = musicForScene4;
                audioSource.volume = 1f; // Restaurar volumen completo
                audioSource.loop = true; // Activar repetici�n
                audioSource.Play();
            }
        }
    }
}
