using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CamaraScript : MonoBehaviour
{
    public float Speed = 100f;
    float rotacionX;
    public Transform jugador;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float MauseX = Input.GetAxis("Mouse X") * Speed * Time.deltaTime;
        float MauseY = Input.GetAxis("Mouse Y") * Speed  * Time.deltaTime;

        rotacionX -= MauseY;
        rotacionX = Mathf.Clamp (rotacionX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotacionX, 0f,0f);
        jugador.Rotate(Vector3.up * MauseX);
    }
}
