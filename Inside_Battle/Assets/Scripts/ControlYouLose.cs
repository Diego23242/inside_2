using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlYouLose : MonoBehaviour
{
    public GameObject canvasYouLose;
    // Llama a esta función cuando el jugador pierda.
    public void MostrarPantallaYouLose()
    {
        canvasYouLose.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor.
        Cursor.visible = true; // Hace visible el cursor.
        Time.timeScale = 0; // Pausa el juego.
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1; // Restablece la escala de tiempo.
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor nuevamente.
        Cursor.visible = false; // Oculta el cursor.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual.
    }

    public void IrAInicio()
    {
        Time.timeScale = 1; // Restablece la escala de tiempo.
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor.
        Cursor.visible = true; // Hace visible el cursor.
        SceneManager.LoadScene(0); // Carga la escena con índice 0 (la de inicio).
    }
}