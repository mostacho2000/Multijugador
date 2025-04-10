using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Configurar el cursor
        Cursor.visible = true; // Hacer visible el cursor
        Cursor.lockState = CursorLockMode.Confined; // Confinado a la ventana del juego
                                                    // Alternativa: Cursor.lockState = CursorLockMode.None; // Cursor completamente libre

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al servidor de Photon.");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No se encontró una sala, creando una nueva...");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Unido a una sala.");
        SpawnPlayer();
    }

    // Método público para respawnear
    private void SpawnPlayer()
    {
        Vector3 spawnPosition = SpawnPointManager.Instance.GetRandomSpawnPoint();
        GameObject newPlayer = PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);

        // Notificar a la cámara sobre el nuevo jugador
        CameraManager.Instance.SetNewPlayerTarget(newPlayer);
    }

    public void RespawnPlayer(GameObject playerToRespawn)
    {
        if (playerToRespawn.GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(playerToRespawn);
            SpawnPlayer();
        }
    }
}
