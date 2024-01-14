using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame ()
    {
        SceneManager.LoadScene(1);
    }
    public void Menu ()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame () 
    {
        Debug.Log("why u kill me");
        Application.Quit();
    }
}
