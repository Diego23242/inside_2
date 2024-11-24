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

        // Cambiar destino despu�s de un tiempo
        if (temporizador >= tiempoCambioDestino)
        {
            CambiarDestinoAleatorio();
            temporizador = 0f;
        }

        // Actualizar el par�metro de velocidad en el Animator
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

    // Obtener una posici�n aleatoria dentro del rango
    Vector3 ObtenerPosicionAleatoria()
    {
        Vector3 posicionAleatoria = Random.insideUnitSphere * rangoMovimiento; // Generar posici�n aleatoria
        posicionAleatoria += transform.position; // Ajustar posici�n con respecto al enemigo
        NavMeshHit hit; // Verificar si est� en el NavMesh
        if (NavMesh.SamplePosition(posicionAleatoria, out hit, rangoMovimiento, NavMesh.AllAreas))
        {
            return hit.position; // Devolver posici�n v�lida
        }
        return transform.position; // Si no es v�lida, quedarse en la misma posici�n
    }
}
