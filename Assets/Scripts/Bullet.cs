using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    public float speed = 10f;

    private Photon.Realtime.Player owner;

    public void OnServerInitialized(float bulletSpeed, Photon.Realtime.Player bulletOwner)
    {
        speed = bulletSpeed;
        owner = bulletOwner;
    }

    void Start()
    {
        if (photonView.IsMine) {
            Destroy(gameObject, 2f);//destruye bala despues de 2 segundos
        }
    }


    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

   void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine && other.CompareTag("Player")) {
            //aqui ayades logica de dayo al jugador
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
