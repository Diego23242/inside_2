using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotationController : MonoBehaviour
{
    public float rotationSpeed = 2f;    // Velocidad de rotaci�n
    public float openAngle = 90f;       // �ngulo de apertura (90 grados por defecto)
    private Quaternion closedRotation;  // Rotaci�n inicial (cerrada)
    private Quaternion openRotation;    // Rotaci�n abierta
    private bool isOpen = false;        // Estado de la puerta

    public AudioSource doorSound;       // AudioSource para el sonido de la puerta

    public Transform doorPivot;         // El objeto vac�o que ser� el pivote de la puerta

    void Start()
    {
        // Configuramos las rotaciones iniciales de la puerta
        closedRotation = doorPivot.localRotation;
        openRotation = Quaternion.Euler(0, openAngle, 0);  // Rota solo sobre el eje Y
    }

    void Update()
    {
        // Rotamos la puerta entre abierta y cerrada con una interpolaci�n suave
        if (isOpen)
        {
            doorPivot.localRotation = Quaternion.Lerp(doorPivot.localRotation, openRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            doorPivot.localRotation = Quaternion.Lerp(doorPivot.localRotation, closedRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el jugador entra al �rea y abre la puerta
        if (other.CompareTag("Player"))
        {
            isOpen = true;
            PlayDoorSound(); // Reproduce el sonido al abrir
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detecta si el jugador sale del �rea y cierra la puerta
        if (other.CompareTag("Player"))
        {
            isOpen = false;
            PlayDoorSound(); // Reproduce el sonido al cerrar
        }
    }

    private void PlayDoorSound()
    {
        if (doorSound != null)
        {
            doorSound.Play();
        }
    }
}
