using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public GameObject hotelObject;
    public GameObject[] goalObjects;
    public int goalsRequired = 3; // Adjust the number of goals required for success
    public float timerDuration = 60.0f; // Adjust the timer duration in seconds

    private bool isTimerRunning = false;
    private float timer;

    private int goalsReached = 0;

    void Start()
    {
        // Ensure the hotel object is initially visible
        if (hotelObject != null)
            hotelObject.SetActive(true);

        // Ensure all goal objects are initially visible
        foreach (GameObject goal in goalObjects)
        {
            if (goal != null)
                goal.SetActive(true);
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                // Timer has run out, handle the event (e.g., fail the delivery)
                ResetDelivery();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == hotelObject)
        {
            // Player reached the hotel
            StartTimer();
            HideObject(hotelObject);
        }

        foreach (GameObject goal in goalObjects)
        {
            if (other.gameObject == goal)
            {
                // Player reached a goal
                goalsReached++;

                HideObject(goal);

                if (goalsReached >= goalsRequired)
                {
                    // Player has reached enough goals for a successful delivery
                    SuccessfulDelivery();
                }
            }
        }
    }

    void StartTimer()
    {
        isTimerRunning = true;
        timer = timerDuration;
    }

    void ResetDelivery()
    {
        isTimerRunning = false;
        goalsReached = 0;

        // Show the hotel object again
        if (hotelObject != null)
            hotelObject.SetActive(true);

        // Show all goal objects again
        foreach (GameObject goal in goalObjects)
        {
            if (goal != null)
                goal.SetActive(true);
        }
    }

    void SuccessfulDelivery()
    {
        isTimerRunning = false;

        // Handle successful delivery logic here

        // For example, you can print a message to the console
        Debug.Log("Delivery successful!");

        // Reset the delivery for the next round
        ResetDelivery();
    }

    void HideObject(GameObject obj)
    {
        if (obj != null)
            obj.SetActive(false);
    }
}