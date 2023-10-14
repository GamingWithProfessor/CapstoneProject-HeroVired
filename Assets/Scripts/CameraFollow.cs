using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player transform to follow.
    public float smoothSpeed = 0.125f; // The speed at which the camera will follow the player.

    private Vector3 offset; // The offset between the camera and the player.

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        // Calculate the desired position of the camera.
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera to the desired position.
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
