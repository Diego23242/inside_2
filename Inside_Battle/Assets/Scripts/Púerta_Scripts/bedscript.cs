using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bedscript : MonoBehaviour
{
private bool isLyingDown = false;
    private Transform bedTransform ;

    // Offset para ajustar la posición del personaje al acostarse
    public Vector3 lieDownOffset;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bedTransform != null && !isLyingDown)
        {
            LieDown();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isLyingDown)
        {
            GetUp();
        }
    }

    private void LieDown()
    {
        isLyingDown = true;

        // Coloca al personaje en la posición de la cama con el offset especificado
        transform.position = bedTransform.position + bedTransform.TransformVector(lieDownOffset);
        transform.rotation = bedTransform.rotation;
    }

    private void GetUp()
    {
        isLyingDown = false;
        // Aquí puedes regresar al personaje a su posición original o activar una animación
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bed"))
        {
            bedTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bed"))
        {
            bedTransform = null;
        }
    }
}
