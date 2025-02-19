using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameDisplay : MonoBehaviourPun
{
    public GameObject nameLabelPrefab; // Prefab que contiene el TextMeshProUGUI para mostrar el nombre del jugador

    private GameObject nameLabel; // Instancia del prefab que mostrará el nombre del jugador en la pantalla

    void Start()
    {
        if (photonView.IsMine)
        {
            // Si el jugador no tiene un nombre asignado, generar un nombre por defecto
            if (string.IsNullOrEmpty(PhotonNetwork.NickName))
            {
                PhotonNetwork.NickName = "Jugador_" + Random.Range(1000, 9999); // Nombre por defecto si no tiene
            }

            // Instanciar el nombre del jugador con un color diferente para el jugador local
            InstanciarNombre(Color.green); // Se puede cambiar el color conforme a las preferencias
        }
        else
        {
            // Para otros jugadores, instanciar el nombre del usuario con color blanco
            InstanciarNombre(Color.white);
        }
    }

    void InstanciarNombre(Color colorTexto)
    {
        // Instanciar el nombre en la UI
        nameLabel = Instantiate(nameLabelPrefab, Vector3.zero, Quaternion.identity);
        nameLabel.transform.SetParent(GameObject.Find("Canvas").transform, false); // Asegurar que el texto esté en el Canvas

        // Obtener el componente TextMeshProUGUI del prefab
        TextMeshProUGUI nameText = nameLabel.GetComponentInChildren<TextMeshProUGUI>();
        if (nameText != null)
        {
            // Asignar el nombre del usuario al texto
            nameText.text = photonView.Owner.NickName;

            // Asignar un color diferente según si es el jugador local o un enemigo
            nameText.color = colorTexto;
        }
        else
        {
            Debug.LogError("No se encontró un componente");
        }
    }

    void Update()
    {
        if (nameLabel != null)
        {
            // Actualizar la posición del nombre de usuario en pantalla
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f);
            nameLabel.transform.position = screenPosition;
        }
    }

    void OnDestroy()
    {
        // Destruir el nombre del jugador cuando el jugador es destruido
        if (nameLabel)
        {
            Destroy(nameLabel);
        }
    }
}

