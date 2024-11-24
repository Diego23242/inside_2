using UnityEngine;
using UnityEngine.UI;

public class StressAndHealthManager : MonoBehaviour
{
    public Slider estres; // Barra de estr�s
    public Slider vida;   // Barra de vida

    public float stressLevel = 50f; // Nivel inicial de estr�s
    public float maxStress = 100f; // Nivel m�ximo de estr�s
    public float health = 100f;    // Nivel inicial de vida
    public float maxHealth = 100f; // Nivel m�ximo de vida

    public float stressIncreaseRate = 5f; // Tasa de aumento de estr�s al correr
    public float healthDecreaseRate = 2f; // Tasa de disminuci�n de vida con estr�s alto

    void Update()
    {
        // Incrementa el estr�s al correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IncreaseStress(stressIncreaseRate * Time.deltaTime);
            Debug.Log("Incrementando estr�s: " + stressLevel); // Confirmaci�n del aumento de estr�s
        }

        // Reduce la salud si el estr�s es alto
        if (stressLevel >= maxStress * 0.8f)
        {
            DecreaseHealth(healthDecreaseRate * Time.deltaTime);
            Debug.Log("Reduciendo salud: " + health); // Confirmaci�n de la reducci�n de salud
        }

        // Actualiza las barras
        estres.value = stressLevel;
        vida.value = health;

        // Mensaje de depuraci�n
        Debug.Log($"Vida: {health}, Estr�s: {stressLevel}");
    }


    public void IncreaseStress(float amount)
    {
        // Incrementa el estr�s dentro del rango permitido
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
        // Reduce el estr�s dentro del rango permitido
        stressLevel = Mathf.Clamp(stressLevel - amount, 0, maxStress);
    }

    void GameOver()
    {
        Debug.Log("�Game Over! Alex no puede continuar.");
        // Reinicia el nivel actual
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
