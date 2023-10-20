using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isGamePaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // If the player presses the escape key, pause or resume the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
        }

        // If the game is paused, enable the pause menu UI
        if (isGamePaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    // Public method to resume the game
    public void ResumeGame()
    {
        isGamePaused = false;
    }

    // Public method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}