using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CountdownTimer countdownTimer;

    // Singleton pattern
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public int deliveriesCompleted = 0;
    public int deliveriesRequiredForNextLevel = 1;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // Initialize references
        countdownTimer = GetComponentInChildren<CountdownTimer>();
    }

    void Update()
    {
        if (deliveriesCompleted >= deliveriesRequiredForNextLevel)
        {
            LoadNextLevel();
        }
    }

    public void DeliveryCompleted()
    {
        deliveriesCompleted++;
    }

    public void DeliveryFailed()
    {
        // Implement any actions for a failed delivery
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more levels available.");
            // Optionally, implement game completion logic here
        }
    }

    public void DeliverySuccessful()
    {
        DeliveryCompleted();
        // Implement any actions for a successful delivery
    }

    public void AddExtraTime(float extraTime)
    {
        countdownTimer.AddExtraTime(extraTime);
    }
}