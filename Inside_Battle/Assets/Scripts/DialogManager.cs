using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    public Text dialogText; // Referencia al cuadro de texto
    public GameObject dialogPanel; // Panel para activar o desactivar los diálogos

    private Queue<string> sentences; // Cola para los diálogos

    void Start()
    {
        sentences = new Queue<string>();
        dialogPanel.SetActive(false); // Ocultar el panel al inicio
    }

    public void StartDialog(string[] dialogLines)
    {
        dialogPanel.SetActive(true); // Mostrar el panel
        sentences.Clear(); // Limpiar la cola

        foreach (string sentence in dialogLines)
        {
            sentences.Enqueue(sentence); // Añadir frases a la cola
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue(); // Obtener la siguiente frase
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence)); // Mostrar con efecto de escritura
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = ""; // Limpiar el texto
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter; // Agregar letra por letra
            yield return new WaitForSeconds(0.05f); // Controla la velocidad
        }
    }

    void EndDialog()
    {
        dialogPanel.SetActive(false); // Ocultar el panel
    }
}
