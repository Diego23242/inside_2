using UnityEngine;
using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour
{
    public float vida = 100f; // Límite de vida del jugador
    public float SaludMaxima = 100f;
    public Image barraDeVida; // Sprite que contiene la barra del jugador

    // Update se llama una vez por frame
    void Update()
    {
        vida = Mathf.Clamp(vida, 0, SaludMaxima); // Asegura que la vida esté entre 0 y SaludMaxima
        barraDeVida.fillAmount = vida / SaludMaxima;

        if (vida == 0)
        {
            Debug.Log("¡Te has muerto!"); // Si la vida llega a cero, el jugador muere
        }
    }

    public void RecibirDaño(float Daño)
    {
        vida -= Daño;
        Debug.Log("Vida después de daño: " + vida);
    }

    public void Curar(float cura)
    {
        vida += cura;
        vida = Mathf.Clamp(vida, 0, SaludMaxima); // Asegura que la vida no supere el máximo
    }
}
