using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Mainmenu : MonoBehaviour
{
    public string SceneName;
    public void ChangeScene()
    {
        
        SceneManager.LoadScene(SceneName);
    }
    public void salir()
    {
        Application.Quit();
    }
}
