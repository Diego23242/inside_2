using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemDoor : MonoBehaviour
{
    public bool doorOpen = false;           // Controla si la puerta está abierta o cerrada
    public float doorOpenAngle = -90f;       // Ángulo al que la puerta se abrirá (en Y)
    public float doorCloseAngle = 90f;     // Ángulo al que la puerta se cerrará (en Y)
    public float smooth = 3.0f;             // Velocidad de la rotación suave

    public AudioClip OpenDoor;
    public AudioClip CloseDoor;

    public void changeDoorState()
    {
        doorOpen = !doorOpen;
    }
    void Update()
    {

        // Lógica de apertura/cierre de la puerta
        if (doorOpen)
        {
            // Definir la rotación de apertura basándonos en la rotación inicial
            Quaternion targetRotation = Quaternion.Euler(90, doorOpenAngle, 0); // Abrir la puerta hacia la derecha
            // Interpolar suavemente hacia la rotación de apertura
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else
        {
            // Definir la rotación de cierre basándonos en la rotación inicial
            Quaternion targetRotation2 = Quaternion.Euler(90, doorCloseAngle, 0); // Cerrar la puerta hacia la posición inicial
            // Interpolar suavemente hacia la rotación de cierre
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TriggerDoor")
        {
            AudioSource.PlayClipAtPoint(CloseDoor, transform.position, 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TriggerDoor")
        {
            AudioSource.PlayClipAtPoint(CloseDoor, transform.position, 1);
        }
    }
}
