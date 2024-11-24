using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaRecorida : MonoBehaviour
{
    private Vector3 closedPosition;    // Posición cerrada (inicial)
    private Vector3 openPosition;      // Posición abierta (después de presionar la tecla)
    public float slideSpeed = 2f;      // Velocidad de movimiento
    private bool isOpen = false;       // Estado de la puerta

    // Start is called before the first frame update
    void Start()
    {
        // Define la posición inicial como la posición cerrada
        closedPosition = transform.position;

        // Define la posición abierta desplazando el valor en el eje X
        openPosition = new Vector3(closedPosition.x + 3f, closedPosition.y, closedPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve la puerta hacia su posición abierta o cerrada
        if (isOpen)
        {
            transform.position = Vector3.Lerp(transform.position, openPosition, slideSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, closedPosition, slideSpeed * Time.deltaTime);
        }
    }

    public void ToggleDoor()
    {
        // Cambia el estado de la puerta
        isOpen = !isOpen;
    }
}
    