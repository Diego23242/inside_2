using UnityEngine;
using UnityEngine.UI;

public class BarraEfecto : MonoBehaviour
{
    public float Efecto = 100f; // L�mite de efecto del jugador
    public float LteMaxEfecto = 100f; // M�ximo valor de la barra de efecto
    public Image BarraDeEfecto; // Sprite que contiene la barra de efecto
    public float CantidadDeDa�oPorSegundo = 1f; // Da�o por segundo cuando la barra de efecto se vac�a
    private VidaPlayer vidaPlayer; // Referencia al script VidaPlayer
    private bool estaCorriendo = false; // Indicador de si el jugador est� corriendo

    void Start()
    {
        // Asigna autom�ticamente el script VidaPlayer en el jugador
        vidaPlayer = GetComponent<VidaPlayer>();
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Detectar si el jugador est� corriendo. Esto puede depender de tu propio script de movimiento.
        // Ejemplo con el componente de movimiento (puedes ajustar esto a tu c�digo).
        if (Input.GetKey(KeyCode.W)) // O la tecla para correr
        {
            estaCorriendo = true;
        }
        else
        {
            estaCorriendo = false;
        }

        ActualizarInterfaz();

        // Solo disminuye el efecto cuando el jugador est� corriendo
        if (estaCorriendo && Efecto > 0)
        {
            Efecto -= Time.deltaTime * 2f; // Reducir lentamente mientras se corre
            Efecto = Mathf.Clamp(Efecto, 0, LteMaxEfecto); // Asegura que no baje de 0
        }

        // Cuando el Efecto llega a cero, empieza a bajar la vida del jugador
        if (Efecto <= 0 && vidaPlayer != null)
        {
            // Aplica da�o a la vida del jugador, pero lentamente
            vidaPlayer.RecibirDa�o(CantidadDeDa�oPorSegundo * Time.deltaTime);
        }
    }

    void ActualizarInterfaz()
    {
        // Actualiza el fillAmount de la barra de efecto
        BarraDeEfecto.fillAmount = Efecto / LteMaxEfecto;
    }

    // M�todos para aumentar o reducir el efecto manualmente
    public void AumentarEfecto(float cantidad)
    {
        Efecto += cantidad;
        Efecto = Mathf.Clamp(Efecto, 0, LteMaxEfecto); // Asegura que no exceda el m�ximo
    }

    public void ReducirEfecto(float cantidad)
    {
        Efecto -= cantidad;
        Efecto = Mathf.Clamp(Efecto, 0, LteMaxEfecto); // Asegura que no baje de 0
    }
}
