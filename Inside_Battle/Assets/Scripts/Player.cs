using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;            // Velocidad de movimiento
    public float runSpeed = 10f;        // Velocidad al correr
    public float jumpForce = 5f;        // Fuerza de salto
    public float crouchHeight = 0.5f;   // Altura cuando el jugador está agachado
    public float normalHeight = 2f;     // Altura normal del jugador
    public float mouseSensitivity = 2f; // Sensibilidad del ratón
    public Transform groundCheck;       // Punto para detectar si el jugador está en el suelo
    public LayerMask groundMask;        // Capa para el suelo
    public Transform playerCamera;      // La cámara del jugador

    private float rotationX = 0f;       // Rotación actual en el eje X (vertical)
    private float groundDistance = 0.4f; // Distancia para detectar el suelo
    private bool isGrounded;            // Para verificar si el jugador está en el suelo
    private bool isCrouching = false;   // Estado de agachado
    private Rigidbody rb;               // Referencia al Rigidbody del jugador
    private CapsuleCollider playerCollider; // Referencia al Collider del jugador

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        LockCursor(); // Bloquear el cursor al inicio del juego
    }

    void Update()
    {
        // Comprobar si el jugador está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Movimiento del jugador
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * moveHorizontal + transform.forward * moveVertical;

        // Cambiar entre caminar y correr
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;

        // Aplicar movimiento
        transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);

        // Manejar la rotación del mouse
        HandleMouseLook();

        // Saltar si el jugador está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Agacharse o levantarse
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }

        // Bloquear el cursor si está desbloqueado accidentalmente
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            LockCursor();
        }
    }

    void HandleMouseLook()
    {
        // Movimiento horizontal del ratón (girar el jugador)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // Movimiento vertical del ratón (mirar arriba/abajo)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Aplicar la rotación horizontal al jugador
        transform.Rotate(Vector3.up * mouseX);

        // Limitar la rotación vertical de la cámara
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Aplicar la rotación a la cámara
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    void Jump()
    {
        // Añadir fuerza para saltar
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Crouch()
    {
        if (isCrouching)
        {
            // Si está agachado, volver a la altura normal
            playerCollider.height = normalHeight;
            isCrouching = false;
        }
        else
        {
            // Si no está agachado, reducir la altura del jugador
            playerCollider.height = crouchHeight;
            isCrouching = true;
        }
    }

    // Método para bloquear y ocultar el cursor
    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro de la pantalla
        Cursor.visible = false; // Ocultar el cursor
    }
}
