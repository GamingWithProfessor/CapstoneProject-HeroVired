using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeLimit; // The maximum time allowed to reach the goal.
    public TextMeshProUGUI timerText; // The UI text to display the timer.
    private float startTime; // The time when the timer started.
    private bool isRunning; // Whether the timer is running.
    void Start()
    {
        // Initialize the timer.
        startTime = Time.time;
        isRunning = false;
    }

    void Update()
    {
        // If the timer is running, update the timer text.
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime;
            timerText.text = $"{timeLimit - elapsedTime:0.0}";
        }

        // Check if the player has reached the goal or run out of time.
        if (GameObject.FindWithTag("Goal") != null)
        {
            // The player has reached the goal. Stop the timer and display a success message.
            isRunning = false;
            timerText.text = "Deliver Successful!";
        }
        else if (Time.time - startTime > timeLimit)
        {
            // The player has run out of time. Stop the timer and display a failure message.
            isRunning = false;
            timerText.text = "Deliver Failed!";
        }
    }
    public void StartTimer()
    {
        // Start the timer.
        startTime = Time.time;
        isRunning = true;
    }
    public void StopTimer()
    {
        // Stop the timer.
        isRunning = false;
    }
}