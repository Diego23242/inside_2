using UnityEngine;
using UnityEngine.UI;
public class VidaPlayer : MonoBehaviour
{
    public float vida = 100f; // Vida actual del jugador
    public float SaludMaxima = 100f; // Vida m�xima del jugador
    public Image barraDeVida; // Imagen de la barra de vida
    public string nombreEscenaActual; // Nombre de la escena actual (opcional si no quieres usar SceneManager)
    public ControlYouLose controlYouLose;

    void Update()
    {
        // Asegura que la vida est� siempre entre 0 y el m�ximo
        vida = Mathf.Clamp(vida, 0, SaludMaxima);

        // Actualiza el valor visual de la barra de vida
        if (barraDeVida != null)
        {
            barraDeVida.fillAmount = (float)vida / (float)SaludMaxima;  // Aseg�rate de convertir a float
        }

        // Si la vida llega a 0, muestra la pantalla de "You Lose" solo si a�n no est� activa
        if (vida <= 0 && !controlYouLose.canvasYouLose.activeSelf)
        {
            controlYouLose.MostrarPantallaYouLose(); // Llama al m�todo de instancia
        }
    }

    // M�todo para reducir la vida del jugador
    public void RecibirDa�o(float Da�o)
    {
        vida -= Da�o;
        Debug.Log("Vida despu�s de recibir da�o: " + vida);
    }

    // M�todo para curar al jugador
    public void Curar(float cura)
    {
        vida += cura;
        vida = Mathf.Clamp(vida, 0, SaludMaxima); // Asegura que la vida no exceda el m�ximo
        Debug.Log("Vida despu�s de curarse: " + vida);
    }
}
