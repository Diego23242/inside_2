using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationcontroller : MonoBehaviour
{
    public Transform[] waypoints; // Puntos de patrullaje
    public float speed = 2f; // Velocidad de movimiento
    public float detectionRadius = 5f; // Radio de detección del jugador u objetivo
    public Transform target; // Objetivo a seguir (por ejemplo, el jugador)

    private Animator animator;
    private int currentWaypointIndex = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("No se encontró un componente Animator en el NPC.");
        }

        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No hay waypoints asignados al NPC. Se moverá de forma aleatoria.");
        }
    }

    void Update()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) <= detectionRadius)
        {
            FollowTarget(); // Seguir al objetivo si está cerca
        }
        else if (waypoints.Length > 0)
        {
            Patrol(); // Patrullar si no hay objetivo cerca
        }
        else
        {
            Wander(); // Movimiento aleatorio si no hay waypoints ni objetivo
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        // Mover hacia el waypoint actual
        Transform waypoint = waypoints[currentWaypointIndex];
        MoveTowards(waypoint.position);

        // Cambiar al siguiente waypoint si llega al actual
        if (Vector3.Distance(transform.position, waypoint.position) < 0.2f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void FollowTarget()
    {
        // Mover hacia el objetivo (por ejemplo, jugador)
        MoveTowards(target.position);
    }

    void Wander()
    {
        // Generar una dirección aleatoria
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f)
        ).normalized;

        transform.Translate(randomDirection * speed * Time.deltaTime, Space.World);

        // Actualizar el Animator
        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        }
    }

    void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;

        // Mover al NPC hacia el destino
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Rotar hacia el destino
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Actualizar el Animator
        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        }
    }
}
