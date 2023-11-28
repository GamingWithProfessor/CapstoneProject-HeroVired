using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [System.Serializable]
    public class Delivery
    {
        public GameObject hotelObject;
        public GameObject goalObject;
    }

    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI deliveryFailedText;
    public TextMeshProUGUI deliverySuccessfulText;
    public TextMeshProUGUI fiveStarText;
    public TextMeshProUGUI fourStarText;
    public TextMeshProUGUI threeStarText;
    public TextMeshProUGUI twoStarText;
    public TextMeshProUGUI oneStarText;

    public Delivery[] deliveries; // Array to hold multiple deliveries
    private int currentDeliveryIndex = 0; // Index to track the current delivery
    private bool timerStarted = false;
    private GameManager gameManager; // Reference to the GameManager script

    void Start()
    {
        // Find the GameManager script in the scene
        gameManager = GameObject.FindObjectOfType<GameManager>();

        // Initialize UI elements
        timerText.gameObject.SetActive(false);
        deliveryFailedText.gameObject.SetActive(false);
        deliverySuccessfulText.gameObject.SetActive(false);
        fiveStarText.gameObject.SetActive(false);
        fourStarText.gameObject.SetActive(false);
        threeStarText.gameObject.SetActive(false);
        twoStarText.gameObject.SetActive(false);
        oneStarText.gameObject.SetActive(false);

        // Start the first delivery
        StartDelivery();
    }

    void Update()
    {
        if (timerStarted)
        {
            // Update the timer
            timeRemaining -= Time.deltaTime;

            // Check if the player reached the goal
            if (PlayerIsTouchingObject(deliveries[currentDeliveryIndex].goalObject) && timeRemaining > 0)
            {
                StopTimer();
                ShowDeliveryResult();

                // Notify the GameManager of a successful delivery
                if (gameManager != null)
                {
                    gameManager.DeliverySuccessful();
                }

                // Hide the goal object when the player reaches it
                deliveries[currentDeliveryIndex].goalObject.SetActive(false);

                // Move to the next delivery
                currentDeliveryIndex++;
                if (currentDeliveryIndex < deliveries.Length)
                {
                    // Start the next delivery
                    StartDelivery();
                }
                else
                {
                    // All deliveries completed, you can implement level completion logic here
                }
            }

            // Check if the timer has reached zero
            if (timeRemaining <= 0)
            {
                StopTimer();
                ShowDeliveryFailed();

                // Restart the current level
                ReloadCurrentLevel();
            }
        }

        // Update the timer text UI element
        UpdateTimerText();
    }

    private void StartDelivery()
    {
        // Find the hotel and goal objects for the current delivery
        GameObject hotelObject = deliveries[currentDeliveryIndex].hotelObject;
        GameObject goalObject = deliveries[currentDeliveryIndex].goalObject;

        // Find the hotel and goal objects in the scene
        hotelObject.SetActive(true);

        // Start the timer, show the path to the goal, and hide the hotel object
        StartTimer();
        ShowPathToGoal(hotelObject, goalObject);
        hotelObject.SetActive(false);
    }

    private void ShowPathToGoal(GameObject hotelObject, GameObject goalObject)
    {
        // You can add code here to show the path if needed
    }

    private void ReloadCurrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void StartTimer()
    {
        timerStarted = true;
        timerText.gameObject.SetActive(true);
    }

    private void StopTimer()
    {
        timerStarted = false;
        timerText.gameObject.SetActive(false);
    }

    private void ShowDeliveryResult()
    {
        // Add code here to show the delivery result
    }

    private void ShowDeliveryFailed()
    {
        // Add code here to show the delivery failed message
    }

    private void UpdateTimerText()
    {
        timerText.text = Mathf.Max(0, Mathf.RoundToInt(timeRemaining)).ToString(); // Ensure the timer doesn't show negative values
    }

    private bool PlayerIsTouchingObject(GameObject objectToTest)
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        Vector3 objectPosition = objectToTest.transform.position;
        float distance = Vector3.Distance(playerPosition, objectPosition);
        return distance < 1f;
    }

    // Remove the duplicated method from the integrated code
     public void AddExtraTime(float extraTime)
     {
         timeRemaining += extraTime;
    //     Debug.Log($"Added {extraTime} seconds. New time remaining: {timeRemaining}");
         UpdateTimerText();
     }
}
