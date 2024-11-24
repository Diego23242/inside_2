using UnityEngine;
using UnityEngine.UI;

public class StressAndHealthManager : MonoBehaviour
{
    public Slider estres; // Barra de estrés
    public Slider vida;   // Barra de vida

    public float stressLevel = 50f; // Nivel inicial de estrés
    public float maxStress = 100f; // Nivel máximo de estrés
    public float health = 100f;    // Nivel inicial de vida
    public float maxHealth = 100f; // Nivel máximo de vida

    public float stressIncreaseRate = 5f; // Tasa de aumento de estrés al correr
    public float healthDecreaseRate = 2f; // Tasa de disminución de vida con estrés alto

    void Update()
    {
        // Incrementa el estrés al correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IncreaseStress(stressIncreaseRate * Time.deltaTime);
            Debug.Log("Incrementando estrés: " + stressLevel); // Confirmación del aumento de estrés
        }

        // Reduce la salud si el estrés es alto
        if (stressLevel >= maxStress * 0.8f)
        {
            DecreaseHealth(healthDecreaseRate * Time.deltaTime);
            Debug.Log("Reduciendo salud: " + health); // Confirmación de la reducción de salud
        }

        // Actualiza las barras
        estres.value = stressLevel;
        vida.value = health;

        // Mensaje de depuración
        Debug.Log($"Vida: {health}, Estrés: {stressLevel}");
    }


    public void IncreaseStress(float amount)
    {
        // Incrementa el estrés dentro del rango permitido
        stressLevel = Mathf.Clamp(stressLevel + amount, 0, maxStress);
    }

    public void DecreaseHealth(float amount)
    {
        // Disminuye la salud dentro del rango permitido
        health = Mathf.Clamp(health - amount, 0, maxHealth);
        if (health <= 0)
        {
            GameOver();
        }
    }

    public void CalmStress(float amount)
    {
        // Reduce el estrés dentro del rango permitido
        stressLevel = Mathf.Clamp(stressLevel - amount, 0, maxStress);
    }

    void GameOver()
    {
        Debug.Log("¡Game Over! Alex no puede continuar.");
        // Reinicia el nivel actual
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
