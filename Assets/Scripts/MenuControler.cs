using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MenuControler : MonoBehaviour
{
    bool cambio;

    public GameObject Baltazar;

    public void ChangeScene(string name)
    {
        StartCoroutine(Time(name));

    }

    public void Salir()
    {
    
        Debug.Log("Salir...");
        Application.Quit();
    }
    IEnumerator Time(string nameScene)
    {
     
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(nameScene);
    }
    public void OnPlayButtonClicked()
    {
        // Opción A: Reconectar (si usas salas)
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom(); // Abandonar la sala actual
        }

        // Opción B: Cargar la escena y dejar que Photon maneje el spawn
        SceneManager.LoadScene("GameScene2");
        Cursor.visible = cambio;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void menu2()
    {
        // Destruir al jugador actual en red (si existe)
        PlayerHealth player = FindObjectOfType<PlayerHealth>();
        if (player != null && player.photonView.IsMine)
        {
            PhotonNetwork.Destroy(player.gameObject); // Destrucción explícita en red
        }
        
        // Cargar el menú
        SceneManager.LoadScene("Menu");

        // Configurar cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Final()
    {
        SceneManager.LoadScene("winner");
        Cursor.visible = cambio;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
