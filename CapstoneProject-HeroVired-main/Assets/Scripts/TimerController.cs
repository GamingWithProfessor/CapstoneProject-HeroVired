using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class TimerController : MonoBehaviour
{
    [System.Serializable]
    public class OrderTimer
    {
        public Transform hotel;
        public List<Transform> goals;
        public int scoreValue = 10;
        public float initialCountdown = 30f;

        [HideInInspector]
        public float countdownTimer;
        [HideInInspector]
        public bool isTimerRunning = false;
        [HideInInspector]
        public bool isCompleted = false;
    }

    [Header("Star Timers")]
    public float fiveStarTimer = 20f;
    public float fourStarTimer = 30f;
    public float threeStarTimer = 40f;
    public float twoStarTimer = 50f;

    [Header("Player and Timer Text")]
    public Transform player;
    public TextMeshProUGUI timerText;

    [Header("Order Timers")]
    public List<OrderTimer> orderTimers;

    [Header("Game Progress")]
    public int deliveriesCompleted = 0;
    public int deliveriesRequiredToFinishLevel = 3;

    [Header("Result Panel")]
    public ResultPanel resultPanel;

    [Header("Waypoint Marker")]
    public WaypointMarker waypointMarker;

    private OrderTimer currentOrder;
    private int totalScore = 0;
    private bool isOnDelivery = false;

    private void Start()
    {
        timerText.gameObject.SetActive(false);

        foreach (var orderTimer in orderTimers)
        {
            orderTimer.countdownTimer = orderTimer.initialCountdown;
            foreach (var goal in orderTimer.goals)
            {
                goal.gameObject.SetActive(false);
            }
        }

        if (waypointMarker == null)
        {
            Debug.LogError("WaypointMarker reference is not set in TimerController. Please set it in the Unity Editor.");
        }
        else
        {
            waypointMarker.SetTargets(new List<Transform>(), player);
            waypointMarker.EnableMarker(false);
        }
    }

    private void Update()
    {
        foreach (var orderTimer in orderTimers)
        {
            if (orderTimer.isTimerRunning)
            {
                orderTimer.countdownTimer -= Time.deltaTime;
                UpdateTimerText(orderTimer);

                if (orderTimer.countdownTimer <= 0f)
                {
                    StopTimer(orderTimer);
                    ReloadLevel();
                }

                bool anyGoalReached = false;
                foreach (var goal in orderTimer.goals)
                {
                    float distanceToGoal = Vector3.Distance(player.position, goal.position);

                    if (distanceToGoal < 1f)
                    {
                        goal.gameObject.SetActive(false);
                        anyGoalReached = true;
                    }
                }

                if (anyGoalReached)
                {
                    if (orderTimer.goals.All(g => !g.gameObject.activeSelf))
                    {
                        StopTimer(orderTimer);
                        DeliverySuccessful(orderTimer);
                    }
                }

                UpdateWaypointMarker(orderTimer);
            }
            else
            {
                if (!isOnDelivery && Vector3.Distance(player.position, orderTimer.hotel.position) < 1f)
                {
                    isOnDelivery = true;
                    currentOrder = orderTimer;
                    orderTimer.hotel.gameObject.SetActive(false);

                    foreach (var goal in orderTimer.goals)
                    {
                        goal.gameObject.SetActive(true);
                    }

                    StartTimer(orderTimer);
                    waypointMarker.EnableMarker(true);
                    waypointMarker.SetTargets(orderTimer.goals, player);
                }
            }
        }
    }

    private void UpdateTimerText(OrderTimer orderTimer)
    {
        string seconds = Mathf.Ceil(orderTimer.countdownTimer).ToString("00");
        timerText.text = $"Timer: {seconds}s";
    }

    public void StartTimer(OrderTimer orderTimer)
    {
        timerText.gameObject.SetActive(true);
        orderTimer.isTimerRunning = true;
    }

    public void StopTimer(OrderTimer orderTimer)
    {
        timerText.gameObject.SetActive(false);
        orderTimer.isTimerRunning = false;
        orderTimer.countdownTimer = Mathf.Max(0f, orderTimer.countdownTimer);
        totalScore += orderTimer.scoreValue;

        // Check if the order has not been marked as completed yet
        if (!orderTimer.isCompleted)
        {
            deliveriesCompleted++; // Increment deliveriesCompleted only once per order
            orderTimer.isCompleted = true; // Mark the order as completed
        }

        currentOrder = null;
        isOnDelivery = false;

        if (CheckAllOrdersCompleted())
        {
            int starRating = CalculateStarRating();
            resultPanel.DisplayResult(totalScore, starRating);
        }

        waypointMarker.EnableMarker(false);
    }

    private void UpdateWaypointMarker(OrderTimer orderTimer)
    {
        Transform nearestGoal = FindNearestActiveGoal(orderTimer.goals);

        if (nearestGoal != null)
        {
            waypointMarker.EnableMarker(true);
            waypointMarker.SetTargets(new List<Transform> { nearestGoal }, player);
        }
        else
        {
            waypointMarker.EnableMarker(false);
        }
    }

    private Transform FindNearestActiveGoal(List<Transform> goals)
    {
        Transform nearestGoal = null;
        float minDistance = float.MaxValue;

        foreach (var goal in goals)
        {
            if (goal.gameObject.activeSelf)
            {
                float distance = Vector3.Distance(player.position, goal.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestGoal = goal;
                }
            }
        }

        return nearestGoal;
    }

    private bool CheckAllOrdersCompleted()
{
    if (deliveriesCompleted >= deliveriesRequiredToFinishLevel)
    {
        // Load the next level or perform any other action
        LoadNextLevel();
        return true;
    }
    return false;
}

private void LoadNextLevel()
{
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;

    // Check if there is a next level
    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
    else
    {
        Debug.LogWarning("No next level available. Consider adding more levels to your build.");
    }
}


    private int CalculateStarRating()
    {
        float totalRemainingTime = 0f;
        int completedOrders = 0;

        foreach (var orderTimer in orderTimers)
        {
            if (!orderTimer.isTimerRunning)
            {
                totalRemainingTime += orderTimer.countdownTimer;
                completedOrders++;
            }
        }

        if (completedOrders > 0)
        {
            float averageRemainingTime = totalRemainingTime / completedOrders;

            if (averageRemainingTime >= fiveStarTimer)
            {
                return 5;
            }
            else if (averageRemainingTime >= fourStarTimer)
            {
                return 4;
            }
            else if (averageRemainingTime >= threeStarTimer)
            {
                return 3;
            }
            else if (averageRemainingTime >= twoStarTimer)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return 0;
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddTime(float timeToAdd)
    {
        // Add time to the timer
        if (currentOrder != null && currentOrder.isTimerRunning)
        {
            currentOrder.countdownTimer += timeToAdd;

            // Update the timer text to reflect the added time
            UpdateTimerText(currentOrder);
        }
    }

    private void DeliverySuccessful(OrderTimer orderTimer)
    {
        totalScore += orderTimer.scoreValue;
        currentOrder = null;
        isOnDelivery = false;

        if (CheckAllOrdersCompleted())
        {
            int starRating = CalculateStarRating();
            resultPanel.DisplayResult(totalScore, starRating);
        }
        waypointMarker.EnableMarker(false);
    }
}
