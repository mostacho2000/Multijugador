using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaPanel : MonoBehaviour
{
    public GameObject pausePanel; // Asigna el panel de pausa en el Inspector

    private bool isPaused = false;

    void Update()
    {
        // Detectar cuando se presiona ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true); // Activar el panel de pausa

        // Pausar el tiempo del juego (opcional)
       // Time.timeScale = 0f;

        // Liberar el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false); // Desactivar el panel de pausa

        // Reanudar el tiempo del juego
        Time.timeScale = 1f;

        // Bloquear el cursor (opcional, si tu juego lo usa)
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    // Métodos para los botones del menú
    public void QuitGame()
    {
        Application.Quit();
        // En el editor:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
