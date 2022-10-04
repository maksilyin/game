using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    private void Start()
    {
        
    }
    public void RecordScene()
    {
        SceneManager.LoadScene("records");
    }

    public void Play()
    {
        SceneManager.LoadScene("game");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
