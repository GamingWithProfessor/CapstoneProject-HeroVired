using UnityEngine;

public enum PowerUpType
{
    SpeedBoost,
    ExtraTimer
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUpEffect(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void ApplyPowerUpEffect(GameObject player)
    {
        MyPlayerController playerController = player.GetComponent<MyPlayerController>();
        CountdownTimer countdownTimer = FindObjectOfType<CountdownTimer>();

        if (playerController != null)
        {
            switch (type)
            {
                case PowerUpType.SpeedBoost:
                    playerController.ApplySpeedBoost();
                    break;
                case PowerUpType.ExtraTimer:
                    if (countdownTimer != null)
                    {
                        countdownTimer.AddExtraTime(10f); // You can adjust the added time as needed
                    }
                    break;
            }
        }

        Invoke(nameof(RevertPowerUpEffect), duration);
    }

    private void RevertPowerUpEffect()
    {
        // Implement code to revert the power-up effect if needed
        Destroy(gameObject);
    }
}