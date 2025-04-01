using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinal : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;   // Velocidad de movimiento normal
    public float velocidadCorrer = 10.0f;      // Velocidad de movimiento al correr
    public float sensibilidadMouse = 200.0f;   // Sensibilidad para la rotación con el mouse
    public float fuerzaSalto = 5.0f;           // Fuerza del salto
    private Animator anim;
    private Rigidbody rb;                      // Referencia al Rigidbody del jugador
    public float x, y;
    private float rotacionX = 0f;              // Control de la rotación vertical de la cámara
    public bool estaEnSuelo;                   // Para verificar si el personaje está en el suelo
    private bool corriendo;                    // Para verificar si está corriendo
    private int saltosDisponibles = 1;         // Contador de saltos, 1 para permitir un salto

    public Transform camaraTransform;          // Referencia a la cámara

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();  // Obtener el Rigidbody
        Cursor.lockState = CursorLockMode.Locked;  // Bloquear el cursor en el centro de la pantalla
    }

    void Update()
    {
        // Capturar movimiento con el teclado
        x = Input.GetAxis("Horizontal");  // Movimiento lateral (A, D o flechas izquierda/derecha)
        y = Input.GetAxis("Vertical");    // Movimiento hacia adelante/atrás (W, S o flechas arriba/abajo)

        // Capturar si está corriendo (al mantener presionada la tecla Shift)
        corriendo = Input.GetKey(KeyCode.LeftShift);

        // Capturar movimiento del mouse para rotar el personaje
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        // Rotar el personaje en el eje Y (horizontal)
        transform.Rotate(0, mouseX, 0);

        // Rotar la cámara en el eje X (vertical) con límites de 45 grados hacia arriba y abajo
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -18f, 45f);  // Limitar el ángulo vertical a ±45 grados
        camaraTransform.localRotation = Quaternion.Euler(rotacionX, 0, 0);

        // Ajustar velocidad según si el jugador está corriendo o no
        float velocidadActual = corriendo ? velocidadCorrer : velocidadMovimiento;

        // Mover el personaje hacia adelante/atrás y lateralmente según las teclas
        Vector3 movimiento = transform.right * x + transform.forward * y;  // Movimiento lateral y hacia adelante
        transform.Translate(movimiento * Time.deltaTime * velocidadActual, Space.World);

        // Saltar si tiene saltos disponibles
        if (Input.GetButtonDown("Jump") && saltosDisponibles > 0)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);  // Aplicar fuerza hacia arriba para saltar
            saltosDisponibles = 0;  // Reducir los saltos disponibles a 0 al saltar
            estaEnSuelo = false;    // Cambiar el estado para que deje de estar en el suelo
            anim.SetBool("isJumping", true);  // Activar animación de salto
        }

        // Actualizar animaciones
        anim.SetFloat("VelX", x);  // Actualizar animación del movimiento lateral
        anim.SetFloat("VelY", y);  // Actualizar animación del movimiento hacia adelante/atrás
        anim.SetBool("isRunning", corriendo);  // Activar animación de correr si está corriendo
        //anim.SetBool("isJumping", !estaEnSuelo);  // Activar animación de salto si no está en el suelo
    }

    // Detectar cuando el personaje entra en contacto con el suelo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = true;       // Si colisiona con el suelo, está en el suelo
            saltosDisponibles = 1;    // Restablecer los saltos disponibles
            anim.SetBool("isJumping", false);  // Desactivar animación de salto
        }
    }

    // Detectar cuando el personaje deja de estar en contacto con el suelo
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = false;  // Si deja de colisionar con el suelo, ya no está en el suelo
        }
    }

    public void SetJumpForce(float newJumpForce)
    {
        fuerzaSalto = newJumpForce;
    }
}
