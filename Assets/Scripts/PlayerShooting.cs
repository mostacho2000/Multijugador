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
            if (Input.GetButtonDown("Fire1"))
            {
                photonView.RPC("shoot", RpcTarget.All);
            }
        }
    }
    [PunRPC]
    void shoot()
    {
        //Instanciar el proyectil
        GameObject bullet = PhotonNetwork.Instantiate("Bullet", firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().photonView.TransferOwnership(photonView.Owner);
    }
}
