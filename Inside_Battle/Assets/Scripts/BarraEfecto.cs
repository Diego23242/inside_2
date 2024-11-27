using UnityEngine;
using UnityEngine.UI;

public class BarraEfecto : MonoBehaviour
{
    public float Efecto = 100f; // Límite de efecto del jugador
    public float LteMaxEfecto = 100f; // Máximo valor de la barra de efecto
    public Image BarraDeEfecto; // Sprite que contiene la barra de efecto
    public float CantidadDeDañoPorSegundo = 1f; // Daño por segundo cuando la barra de efecto se vacía
    private VidaPlayer vidaPlayer; // Referencia al script VidaPlayer
    private bool estaCorriendo = false; // Indicador de si el jugador está corriendo
    public KeyCode teclaCorrer = KeyCode.LeftShift; // Tecla para correr

    void Start()
    {
        // Asigna automáticamente el script VidaPlayer en el jugador
        vidaPlayer = GetComponent<VidaPlayer>();
    }

    // Update se llama una vez por frame
    void Update()
    {
        DetectarEstadoMovimiento();
        ActualizarInterfaz();

        // Solo disminuye el efecto cuando el jugador está corriendo
        if (estaCorriendo && Efecto > 0)
        {
            Efecto -= Time.deltaTime * 2f; // Reducir lentamente mientras se corre
            Efecto = Mathf.Clamp(Efecto, 0, LteMaxEfecto); // Asegura que no baje de 0
        }

        // Cuando el efecto llega a cero, empieza a bajar la vida del jugador
        if (Efecto <= 0 && vidaPlayer != null)
        {
            vidaPlayer.RecibirDaño(CantidadDeDañoPorSegundo * Time.deltaTime);
        }
    }

    void DetectarEstadoMovimiento()
    {
        // Detectar si el jugador está corriendo (puedes ajustar esto a tu propio sistema de movimiento)
        if (Input.GetKey(teclaCorrer) && Input.GetKey(KeyCode.W)) // Está corriendo si presiona Shift y avanza
        {
            estaCorriendo = true;
        }
        else
        {
            estaCorriendo = false; // No está corriendo
        }
    }

    void ActualizarInterfaz()
    {
        // Actualiza el fillAmount de la barra de efecto
        BarraDeEfecto.fillAmount = Efecto / LteMaxEfecto;
    }

    // Métodos para aumentar o reducir el efecto manualmente
    public void AumentarEfecto(float cantidad)
    {
        Efecto += cantidad;
        Efecto = Mathf.Clamp(Efecto, 0, LteMaxEfecto); // Asegura que no exceda el máximo
    }

    public void ReducirEfecto(float cantidad)
    {
        Efecto -= cantidad;
        Efecto = Mathf.Clamp(Efecto, 0, LteMaxEfecto); // Asegura que no baje de 0
    }
}
