using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    bool cambio;

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
    public void inicio()
    {
        SceneManager.LoadScene("GameScene2");
        Cursor.visible = cambio;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Final()
    {
        SceneManager.LoadScene("winner");
        Cursor.visible = cambio;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
