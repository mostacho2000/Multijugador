using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAiming : MonoBehaviourPun
{

    void Update()
    {
        if (photonView.IsMine)
        {
            //Obtener la posicion del cursor en todo momento
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {

                //calcular la direccion hacia el cursor
                Vector3 direction = hit.point - transform.position;
                direction.y = 0;//ignorar el componente Y


                //Rotar el jugadro hacia la direccion del jugador
                if (direction!= Vector3.zero) {

                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation= Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
                }
            }
        }
    }
}
