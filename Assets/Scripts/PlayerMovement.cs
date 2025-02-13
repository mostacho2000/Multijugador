using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public float speed = 10f;

    //accedo al Rigidbody
    private Rigidbody rb;

    private void Start()
    {
        //obtiene el componente Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //verifica si el juagdor es el dueyo del Photonview
        if (photonView.IsMine) {
            //Movimiento
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            //Aplicar el movimiento al RB
            Vector3 movement = new Vector3(moveX, 0, moveZ) * speed;
            rb.velocity = movement;
           
        }
    }

    
}
