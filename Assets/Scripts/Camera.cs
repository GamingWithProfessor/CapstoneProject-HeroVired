using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float distance = 3f; // distance from the player
    public float height = 2f; // height above the player
    public float rotationSpeed = 5f;
    public float fovIncreaseSpeed = 5f; // Adjust the speed of FOV change

    void Start()
    {
        if (playerTransform != null)
        {
            // Set the initial position based on the player's rotation
            float initialAngle = playerTransform.eulerAngles.y;
            Quaternion initialRotation = Quaternion.Euler(0f, initialAngle, 0f);
            Vector3 initialPosition = playerTransform.position - (initialRotation * Vector3.forward * distance) + new Vector3(0f, height, 0f);
            transform.position = initialPosition;

            // Look at the player
            transform.LookAt(playerTransform.position);
        }
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate the desired rotation based on player's rotation
            float desiredAngle = playerTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, desiredAngle, 0f);

            // Calculate the desired position based on the rotated offset
            Vector3 desiredPosition = playerTransform.position - (rotation * Vector3.forward * distance) + new Vector3(0f, height, 0f);

            // Smoothly move the camera to the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

            // Look at the player
            transform.LookAt(playerTransform.position);

            // Adjust the field of view
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, Time.deltaTime * fovIncreaseSpeed); // Adjust 60f to your desired FOV
        }
    }
}
