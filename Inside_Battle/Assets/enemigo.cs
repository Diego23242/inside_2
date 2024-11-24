using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemigo : MonoBehaviour
{
    public float Velocidad; // Velocidad del enemigo
    public NavMeshAgent IA; // Componente NavMeshAgent
    public float rangoMovimiento = 10f; // Radio de movimiento aleatorio
    public float tiempoCambioDestino = 3f; // Tiempo para cambiar de destino

    private float temporizador; // Temporizador para cambiar de destino
    private Animator animator; // Referencia al Animator

    void Start()
    {
        IA.speed = Velocidad;
        CambiarDestinoAleatorio(); // Establecer un destino inicial aleatorio
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    void Update()
    {
        temporizador += Time.deltaTime;

        // Cambiar destino después de un tiempo
        if (temporizador >= tiempoCambioDestino)
        {
            CambiarDestinoAleatorio();
            temporizador = 0f;
        }

        // Actualizar el parámetro de velocidad en el Animator
        if (animator != null)
        {
            animator.SetFloat("Speed", IA.velocity.magnitude); // Velocidad actual del NavMeshAgent
        }
    }

    // Cambiar a un destino aleatorio dentro del rango especificado
    void CambiarDestinoAleatorio()
    {
        Vector3 destinoAleatorio = ObtenerPosicionAleatoria();
        IA.SetDestination(destinoAleatorio);
    }

    // Obtener una posición aleatoria dentro del rango
    Vector3 ObtenerPosicionAleatoria()
    {
        Vector3 posicionAleatoria = Random.insideUnitSphere * rangoMovimiento; // Generar posición aleatoria
        posicionAleatoria += transform.position; // Ajustar posición con respecto al enemigo
        NavMeshHit hit; // Verificar si está en el NavMesh
        if (NavMesh.SamplePosition(posicionAleatoria, out hit, rangoMovimiento, NavMesh.AllAreas))
        {
            return hit.position; // Devolver posición válida
        }
        return transform.position; // Si no es válida, quedarse en la misma posición
    }
}
