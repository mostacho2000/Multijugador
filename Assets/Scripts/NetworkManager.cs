using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //Conecta al servidor de Photon
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al servidor de photon");
        //Unirse a una sala aleatoria
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log("No se creo una sala, creando una nueva...");
        //crear una nueva sala si no hay ninguna disponible
        PhotonNetwork.CreateRoom(null,new Photon.Realtime.RoomOptions{MaxPlayers = 4 });
    }

    public override void OnJoinedRoom() {
        Debug.Log("Unido a una sala.");
        //Iniciar al jugador en la sala
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
}
