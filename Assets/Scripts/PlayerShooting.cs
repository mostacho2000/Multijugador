using UnityEngine;
using Photon.Pun;

public class PlayerShooting : MonoBehaviourPun
{
    // Punto desde donde se disparan las balas
    public Transform firePoint;

    // Velocidad de la bala
    public float bulletSpeed = 10f;

    // Prefabs de las balas (deben estar en la carpeta Resources)
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;

    // Prefab actual que se va a disparar
    private GameObject currentBulletPrefab;

    void Start()
    {
        // Inicializar el prefab actual con el primer prefab de bala
        currentBulletPrefab = bulletPrefab;
    }

    void Update()
    {
        // Verifica si el jugador local es el dueño del PhotonView
        if (photonView.IsMine)
        {
            // Si presiona el botón izquierdo del mouse, dispara
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot(); // Llama al método para disparar
            }

            // Si presiona el botón derecho del mouse, alterna entre los prefabs de balas
            if (Input.GetButtonDown("Fire2"))
            {
                ToggleBulletPrefab(); // Llama al método para alternar entre los prefabs
            }
        }
    }

    void Shoot()
    {
        // Instancia la bala en la red usando PhotonNetwork.Instantiate
        // Solo el jugador local (dueño del PhotonView) ejecuta esta lógica
        GameObject bullet = PhotonNetwork.Instantiate(currentBulletPrefab.name, firePoint.position, firePoint.rotation);

        // Inicializa la bala con la velocidad y el dueño (jugador que disparó)
        bullet.GetComponent<Bullet>().Initialize(bulletSpeed, photonView.Owner);
    }

    void ToggleBulletPrefab()
    {
        // Alterna entre los dos prefabs de balas
        if (currentBulletPrefab == bulletPrefab)
        {
            currentBulletPrefab = bulletPrefab2;
        }
        else
        {
            currentBulletPrefab = bulletPrefab;
        }

        // Opcional: Puedes añadir un mensaje de depuración para verificar el cambio
        Debug.Log("Prefab de bala cambiado a: " + currentBulletPrefab.name);
    }
}