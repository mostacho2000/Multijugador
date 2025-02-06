using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    public float speed = 10f;
    public float lifetime = 2f;
    void Start()
    {
        if (photonView.IsMine) {
            Destroy(gameObject, lifetime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

   void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine && other.CompareTag("Player")) {
            //aqui ayades logica de dayo al jugador
            Debug.Log("Jugador Golpeado!");
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
