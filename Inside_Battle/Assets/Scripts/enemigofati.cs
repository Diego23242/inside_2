using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoFati : MonoBehaviour
{
    public float dañoVida = 10f; // Daño que reduce la vida del jugador
    public float dañoEfecto = 20f; // Daño que reduce la barra de efecto
    public float duracionEfecto = 15f; // Duración del efecto en segundos
    public float tiempoEntreAtaques = 1f; // Tiempo entre ataques
    public float rangoDeAtaque = 2f; // Distancia mínima para atacar al jugador
    public Transform objetivo; // Transform del jugador
    public float velocidadEnemigo = 3.5f; // Velocidad de movimiento del enemigo

    private NavMeshAgent navMeshAgent; // Componente de navegación
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
            Debug.LogError("El objetivo no está asignado. Por favor, asigna el jugador en el inspector.");
        }
    }

    void Update()
    {
        tiempoDesdeUltimoAtaque += Time.deltaTime;

        if (objetivo != null)
        {
            navMeshAgent.SetDestination(objetivo.position); // Seguir al jugador

            // Verificar si está dentro del rango para atacar
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
        // Detectar al jugador y aplicar daño/efecto si los scripts están presentes
        VidaPlayer vidaPlayer = objetivo.GetComponent<VidaPlayer>();
        BarraEfecto barraEfecto = objetivo.GetComponent<BarraEfecto>();

        if (vidaPlayer != null && barraEfecto != null)
        {
            vidaPlayer.RecibirDaño(dañoVida); // Reducir la vida
            barraEfecto.ReducirEfecto(dañoEfecto); // Reducir la barra de efecto

            StartCoroutine(AplicarEfecto(barraEfecto)); // Iniciar el efecto temporal
        }
    }

    private IEnumerator AplicarEfecto(BarraEfecto barraEfecto)
    {
        barraEfecto.ReducirEfecto(dañoEfecto); // Reducir el efecto

        yield return new WaitForSeconds(duracionEfecto); // Esperar el tiempo del efecto

        // Restaurar el efecto solo si el jugador sigue vivo
        barraEfecto.AumentarEfecto(dañoEfecto);
    }
}
