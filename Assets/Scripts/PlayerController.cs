using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player is providing input
        float turnInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        if (turnInput != 0 || forwardInput != 0)
        {
            // Move the player forward
            float forwardMove = speed * forwardInput * Time.deltaTime;
            rb.MovePosition(transform.position + transform.forward * forwardMove);

            // Rotate the player based on the player's input
            transform.Rotate(Vector3.up * turnSpeed * turnInput * Time.deltaTime);
        }
    }
}