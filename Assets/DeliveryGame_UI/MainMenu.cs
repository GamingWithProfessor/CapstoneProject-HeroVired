using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
   
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        SceneManager.LoadSceneAsync(2);
        SceneManager.LoadSceneAsync(3);
        SceneManager.LoadSceneAsync(4);
    } 
    
    public void QuitGame()
    {
        Application.Quit();
    }
   
}
