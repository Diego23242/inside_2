using UnityEngine;
using UnityEngine.UI;
public class VidaPlayer : MonoBehaviour
{
    public float vida = 100f; // Vida actual del jugador
    public float SaludMaxima = 100f; // Vida máxima del jugador
    public Image barraDeVida; // Imagen de la barra de vida
    public string nombreEscenaActual; // Nombre de la escena actual (opcional si no quieres usar SceneManager)
    public ControlYouLose controlYouLose;

    void Update()
    {
        // Asegura que la vida esté siempre entre 0 y el máximo
        vida = Mathf.Clamp(vida, 0, SaludMaxima);

        // Actualiza el valor visual de la barra de vida
        if (barraDeVida != null)
        {
            barraDeVida.fillAmount = (float)vida / (float)SaludMaxima;  // Asegúrate de convertir a float
        }

        // Si la vida llega a 0, muestra la pantalla de "You Lose" solo si aún no está activa
        if (vida <= 0 && !controlYouLose.canvasYouLose.activeSelf)
        {
            controlYouLose.MostrarPantallaYouLose(); // Llama al método de instancia
        }
    }

    // Método para reducir la vida del jugador
    public void RecibirDaño(float Daño)
    {
        vida -= Daño;
        Debug.Log("Vida después de recibir daño: " + vida);
    }

    // Método para curar al jugador
    public void Curar(float cura)
    {
        vida += cura;
        vida = Mathf.Clamp(vida, 0, SaludMaxima); // Asegura que la vida no exceda el máximo
        Debug.Log("Vida después de curarse: " + vida);
    }
}
