using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType
    {
        Timer,        // Timer power-up
        SpeedBoost    // Speed boost power-up
    }

    public PowerupType powerupType; // Type of the power-up
    public float timeToAdd = 10f;   // The amount of time to add to the timer (for Timer power-up)
    public float speedBoostDuration = 5f; // Duration of the speed boost (for Speed Boost power-up)
    public float speedBoostMultiplier = 2f; // Multiplier for speed boost (for Speed Boost power-up)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply the power-up effect based on its type
            switch (powerupType)
            {
                case PowerupType.Timer:
                    ApplyTimerPowerup(other);
                    break;
                case PowerupType.SpeedBoost:
                    ApplySpeedBoostPowerup(other);
                    break;
                // Add more cases for additional power-up types if needed
            }

            // Destroy the power-up object
            Destroy(gameObject);
        }
    }

    private void ApplyTimerPowerup(Collider playerCollider)
    {
        // Apply the timer power-up effect to the player's timer
        TimerController timerController = playerCollider.GetComponent<TimerController>();
        if (timerController != null)
        {
            timerController.AddTime(timeToAdd);
        }
    }

    private void ApplySpeedBoostPowerup(Collider playerCollider)
    {
        // Apply the speed boost power-up effect to the player
        PlayerController playerController = playerCollider.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ActivateSpeedBoost(speedBoostMultiplier, speedBoostDuration);
        }
    }
}
