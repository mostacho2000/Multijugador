using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    private float speed;

    private Photon.Realtime.Player owner;

    public void Initialize(float bulletSpeed, Photon.Realtime.Player bulletOwner)
    {
        speed = bulletSpeed;
        owner = bulletOwner;
    }

    void Start()
    {
        if (photonView.IsMine) {
            Invoke("DestroyBullet",1f);
        }
    }


    void Update()
    {
        if (photonView.IsMine)
        {
            transform.Translate(Vector3.forward * speed*Time.deltaTime);
        }
    }

   void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine) {

            if (!other.CompareTag("Player"))
            {
                DestroyBullet();
            }
        }
    }
    void DestroyBullet()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
