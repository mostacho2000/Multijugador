using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameDisplay : MonoBehaviourPun
{
    public GameObject nameLabelPrefab;

    private GameObject nameLabel;
    void Start()
    {
        if (photonView.IsMine)
        {
            //solo el dueyo del photonview instancia el nickname
            nameLabel = Instantiate(nameLabelPrefab, Vector3.zero, Quaternion.identity);
            nameLabel.transform.SetParent(GameObject.Find("Canvas").transform, false);

            //asignar el nombre del usuario al texto
            TextMeshProUGUI nameText = nameLabel.GetComponent<TextMeshProUGUI>();
            nameText.text = photonView.Owner.NickName;

            //Asignar un color diferente para el jugador local
            nameText.color = Color.green;// se puede cambiar el color conforme las preferencias
        }
        else
        {
            //para otros jugadores, instanciar el nombre del usuario
            nameLabel = Instantiate(nameLabelPrefab, Vector3.zero, Quaternion.identity);
            nameLabel.transform.SetParent(GameObject.Find("Canvas").transform, false);

            //asignar el nombre del usuario al texto
            TextMeshProUGUI nameText = nameLabel.GetComponent<TextMeshProUGUI>();
            nameText.text = photonView.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //asignarr el nombre del usuario al texto
        if (nameLabel != null) {

            //actualizar la posicion del nombvre de usuario en pantalla
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f);//
            nameLabel.transform.position = screenPosition;
        }
    }

     void OnDestroy()
    {
        //Destruir el nombre del jugador cunqado el jugador es destruido
        if (nameLabel) {
            Destroy(nameLabel);
        }

    }
}

