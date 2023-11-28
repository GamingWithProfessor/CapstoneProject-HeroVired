using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    public float normalSpeed = 10f;
    public float turnSpeed = 100f;
    public float extraTimeAmount = 10f; // Amount of extra time to add

    private float currentSpeed;
    private bool speedBoosted = false;
    private float extraTime;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = normalSpeed;
        extraTime = 0f;
    }

    void Update()
    {
        // Check if the player is providing input
        float turnInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        if (turnInput != 0 || forwardInput != 0)
        {
            // Move the player forward
            float forwardMove = currentSpeed * forwardInput * Time.deltaTime;
            rb.MovePosition(transform.position + transform.forward * forwardMove);

            // Rotate the player based on the player's input
            transform.Rotate(Vector3.up * turnSpeed * turnInput * Time.deltaTime);
        }

        // Update the timer
        extraTime -= Time.deltaTime;

        // Check if the extra time has reached zero
        if (extraTime <= 0)
        {
            ResetTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            //Debug.Log("Player collided with power-up!");

            PowerUp powerUp = other.GetComponent<PowerUp>();
            if (powerUp != null)
            {
                //Debug.Log($"Power-up type: {powerUp.type}");
               // Debug.Log($"Player speed before power-up: {currentSpeed}");

                ApplyPowerUpEffect(powerUp);

                //Debug.Log($"Player speed after power-up: {currentSpeed}");
            }
        }
    }

    public void ApplySpeedBoost()
    {
        if (!speedBoosted)
        {
            // Increase speed
            currentSpeed *= 2f;
            speedBoosted = true;
        }
    }

    public void ApplyExtraTimer()
    {
        // Add extra time to the timer through GameManager
        GameManager.Instance.AddExtraTime(extraTimeAmount);
        //Debug.Log($"Extra time applied: {extraTimeAmount}, Total extra time now: {extraTime}");

        // Implement any additional logic if needed
    }

    private void ResetTimer()
    {
        // Reset the timer-related variables
        extraTime = 0f;

        // Reset any effects applied due to the timer (if needed)
    }

    private void ApplyPowerUpEffect(PowerUp powerUp)
    {
        switch (powerUp.type)
        {
            case PowerUpType.SpeedBoost:
                ApplySpeedBoost();
                break;
            case PowerUpType.ExtraTimer:
                ApplyExtraTimer();
                break;
        }

        Destroy(powerUp.gameObject);
    }
}
