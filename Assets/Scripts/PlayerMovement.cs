using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public float speed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;


    void Update()
    {
        if (photonView.IsMine) {
            //Movimiento
            float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(new Vector3(moveX, 0, moveZ));

            //Disparar
            if (Input.GetButton("Fire1")) {
                photonView.RPC("shoot", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void Shoot()
    {
        //Instanciar el proyectil
        GameObject bullet = PhotonNetwork.Instantiate("Bullet", firePoint.position,firePoint.rotation);
        bullet.GetComponent<Bullet>().photonView.TransferOwnership(photonView.Owner);
    }
}
