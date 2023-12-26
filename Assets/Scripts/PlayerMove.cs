using UnityEngine;
using UnityEngine.SceneManagement; // You need this for SceneManager

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 100f;

    private Rigidbody rb;
    private Animator animator;

    private bool isSpeedBoostActive = false;
    private float originalSpeed;
    private float speedBoostEndTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        originalSpeed = speed;
    }

    void Update()
    {
        // Check if the player is providing input
        float turnInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        // Check if the player is idle or skateboarding
        bool isIdling = Mathf.Approximately(forwardInput, 0f) && Mathf.Approximately(turnInput, 0f);
        bool isSkateboarding = !isIdling;

        // Update animator parameters
        animator.SetBool("IsSkateboarding", isSkateboarding);
        animator.SetBool("IsIdling", isIdling);

        if (isSkateboarding)
        {
            // Move the player forward
            float forwardMove = speed * forwardInput * Time.deltaTime;
            rb.MovePosition(transform.position + transform.forward * forwardMove);

            // Rotate the player based on the player's input
            transform.Rotate(Vector3.up * turnSpeed * turnInput * Time.deltaTime);
        }

        if (isSpeedBoostActive)
        {
            // Check if the speed boost duration has elapsed
            if (Time.time >= speedBoostEndTime)
            {
                DeactivateSpeedBoost();
            }
        }

        // Check if the player's Y position is below -25
        if (transform.position.y < -25f)
        {
            // Restart the level
            RestartLevel();
        }
    }

    // Method to activate the speed boost power-up
    public void ActivateSpeedBoost(float boostMultiplier, float boostDuration)
    {
        if (!isSpeedBoostActive)
        {
            isSpeedBoostActive = true;
            speed *= boostMultiplier;
            speedBoostEndTime = Time.time + boostDuration;
        }
    }
    // Method to deactivate the speed boost power-up
    private void DeactivateSpeedBoost()
    {
        isSpeedBoostActive = false;
        speed = originalSpeed;
    }
    // Method to restart the level
    private void RestartLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
