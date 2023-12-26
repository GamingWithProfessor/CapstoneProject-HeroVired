using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour
{
    public GameObject resultPanel;
    public TextMeshProUGUI scoreText;
    public GameObject starsPanel; // Reference to the panel containing star UI components

    public Image[] starImages; // UI star images

    private void Start()
    {
        // Ensure the result panel is initially hidden
        resultPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        resultPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        // Get the index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene based on the current scene index + 1
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void Upgrade()
    {
        // Implement logic for the upgrade button
    }

    public void CloseResult()
    {
        resultPanel.SetActive(false);
    }

    // Display the result with the provided score and star rating
    public void DisplayResult(int score, int starRating)
    {
        // Show the result panel
        resultPanel.SetActive(true);

        // Display the score
        scoreText.text = $"Score: {score}";

        // Enable or disable star images based on starRating
        SetStarRating(starRating);
    }

    private void SetStarRating(int starRating)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            // Enable or disable the images based on star rating
            starImages[i].enabled = i < starRating;
        }
    }

    // Update the star UI based on the received star rating
    private void UpdateStars(int starRating)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            // Enable or disable star images based on the star rating
            starImages[i].enabled = i < starRating;
        }
    }
}
