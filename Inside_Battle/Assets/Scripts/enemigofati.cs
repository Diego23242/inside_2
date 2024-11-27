using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoFati : MonoBehaviour
{
    public float da�oVida = 10f; // Da�o que reduce la vida del jugador
    public float da�oEfecto = 20f; // Da�o que reduce la barra de efecto
    public float duracionEfecto = 15f; // Duraci�n del efecto en segundos
    public float tiempoEntreAtaques = 1f; // Tiempo entre ataques
    public float rangoDeAtaque = 2f; // Distancia m�nima para atacar al jugador
    public Transform objetivo; // Transform del jugador
    public float velocidadEnemigo = 3.5f; // Velocidad de movimiento del enemigo

    private NavMeshAgent navMeshAgent; // Componente de navegaci�n
    private float tiempoDesdeUltimoAtaque; // Temporizador para ataques

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = velocidadEnemigo; // Configurar la velocidad del enemigo
        }

        if (objetivo == null)
        {
            Debug.LogError("El objetivo no est� asignado. Por favor, asigna el jugador en el inspector.");
        }
    }

    void Update()
    {
        tiempoDesdeUltimoAtaque += Time.deltaTime;

        if (objetivo != null)
        {
            navMeshAgent.SetDestination(objetivo.position); // Seguir al jugador

            // Verificar si est� dentro del rango para atacar
            float distanciaAlJugador = Vector3.Distance(transform.position, objetivo.position);
            if (distanciaAlJugador <= rangoDeAtaque && tiempoDesdeUltimoAtaque >= tiempoEntreAtaques)
            {
                AtacarJugador();
                tiempoDesdeUltimoAtaque = 0f; // Reiniciar el temporizador
            }
        }
    }

    private void AtacarJugador()
    {
        // Detectar al jugador y aplicar da�o/efecto si los scripts est�n presentes
        VidaPlayer vidaPlayer = objetivo.GetComponent<VidaPlayer>();
        BarraEfecto barraEfecto = objetivo.GetComponent<BarraEfecto>();

        if (vidaPlayer != null && barraEfecto != null)
        {
            vidaPlayer.RecibirDa�o(da�oVida); // Reducir la vida
            barraEfecto.ReducirEfecto(da�oEfecto); // Reducir la barra de efecto

            StartCoroutine(AplicarEfecto(barraEfecto)); // Iniciar el efecto temporal
        }
    }

    private IEnumerator AplicarEfecto(BarraEfecto barraEfecto)
    {
        barraEfecto.ReducirEfecto(da�oEfecto); // Reducir el efecto

        yield return new WaitForSeconds(duracionEfecto); // Esperar el tiempo del efecto

        // Restaurar el efecto solo si el jugador sigue vivo
        barraEfecto.AumentarEfecto(da�oEfecto);
    }
}
