using UnityEngine;
using Photon.Pun;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -3);
    private GameObject player;
    private bool isFollowing = true;

    private void Awake()
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
        FindPlayer();
    }

    public void FindPlayer()
    {
        // Buscar al jugador local
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            PhotonView pv = p.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                SetNewPlayerTarget(p);
                break;
            }
        }
    }

    public void SetNewPlayerTarget(GameObject newPlayer)
    {
        player = newPlayer;
        isFollowing = true;

        // Posicionamiento inmediato al cambiar de jugador
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
    }

    void LateUpdate()
    {
        if (player != null && isFollowing)
        {
            Vector3 desiredPosition = player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }
}
