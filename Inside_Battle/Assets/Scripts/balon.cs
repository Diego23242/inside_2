using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balon : MonoBehaviour
{

    public float pushForce = 5f; // Fuerza con la que empujar el balón

    private void OnCollisionEnter(Collision collision)
    {
        // Detectar si chocamos con el balón
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // Calculamos la dirección del empuje
                Vector3 pushDirection = collision.contacts[0].point - transform.position;
                pushDirection.y = 0; // Opcional: No empujar hacia arriba
                pushDirection.Normalize();

                // Aplicamos la fuerza al balón
                ballRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
}
