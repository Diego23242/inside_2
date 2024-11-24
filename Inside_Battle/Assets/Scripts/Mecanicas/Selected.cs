using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
    public float distancia = 1.5f;
    public LayerMask mask;
    public Texture2D Puntero;
    public GameObject TextDetected;
    GameObject UltimoReconocido = null;

    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
        TextDetected.SetActive(false);
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
        {
            Deselect();
            selectedobject(hit.transform);

            if (hit.collider.CompareTag("Objeto_interactivo"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ObjetoInteractivo interactivo = hit.collider.transform.GetComponent<ObjetoInteractivo>();
                    if (interactivo != null)
                    {
                        interactivo.ActivarObjeto();
                    }
                }
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);
        }
        else
        {
            Deselect();
        }
    }

    void selectedobject(Transform transform)
    {
        if (UltimoReconocido != null)
        {
            UltimoReconocido.GetComponent<Renderer>().material.color = Color.green;
        }
        UltimoReconocido = transform.gameObject;
    }

    void Deselect()
    {
        if (UltimoReconocido)
        {
            UltimoReconocido.GetComponent<Renderer>().material.color = Color.white;
            UltimoReconocido = null;
        }
    }

    void OnGUI()
    {
        if (Puntero != null)
        {
            Rect rect = new Rect(Screen.width / 2, Screen.height / 2, Puntero.width, Puntero.height);
            GUI.DrawTexture(rect, Puntero);
        }

        if (UltimoReconocido)
        {
            TextDetected.SetActive(true);
        }
        else
        {
            TextDetected.SetActive(false);
        }
    }
}