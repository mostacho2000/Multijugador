using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerShooting : MonoBehaviourPun
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
 
    void Update()
    {  
        if (photonView.IsMine)
        {
            //Disparar
            if (photonView.IsMine && Input.GetButtonDown("Fire1"))
            {
                shoot();
            }
        }
    }
    [PunRPC]
    void shoot()
    {
        //Instanciar el proyectil
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name,firePoint.position,firePoint.rotation);
        bullet.GetComponent<Bullet>().Initialize(bulletSpeed,photonView.Owner);
        //photonView.RPC("shoot", RpcTarget.All);
    }
}
