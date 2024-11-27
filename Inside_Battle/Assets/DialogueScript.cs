using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] Lines;
    int index;

    public float textSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.E))
       {
        if(dialogueText.text == Lines[index]){
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = Lines[index];
        }
       }
    }

    public void StartDialogue()
    {
        index = 0;
        dialogueText.text = ""; // Limpiar el texto antes de iniciar
        StartCoroutine(Writeline());
    }

    IEnumerator Writeline()
    {
        foreach (char letter in Lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void NextLine () {
        if(index <Lines.Length - 1){
            index++;
            StartCoroutine(Writeline());
        }
        else{
            gameObject.SetActive(false);

        }
    }
}
