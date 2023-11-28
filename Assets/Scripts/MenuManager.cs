using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel; // Reference to the settings panel GameObject

    void Start()
    {
        
    }

    // Method to be called when the Settings button is clicked
    public void OpenSettings()
    {
        
        {
            // Show the settings panel
            settingsPanel.SetActive(true);
        }
    }

    // Method to be called when the Close Settings button is clicked
    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            // Hide the settings panel
            settingsPanel.SetActive(false);
        }
    }

    // Method to be called when the Delivery Now button is clicked
    public void StartDeliveryNow()
    {
        // Load the game scene (assuming it's the first scene in the build settings)
        SceneManager.LoadScene(1);
    }
}
