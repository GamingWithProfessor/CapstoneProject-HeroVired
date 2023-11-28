using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public Transform player;
    public Transform hotel; // Assign the hotel's transform in the Inspector
    public Transform goal;  // Assign the goal's transform in the Inspector

    public Color hotelColor = Color.blue; // Color for the arrow when pointing to the hotel
    public Color goalColor = Color.red;   // Color for the arrow when pointing to the goal

    private SpriteRenderer arrowRenderer;

    private void Start()
    {
        // Get the SpriteRenderer component on the compass UI element
        arrowRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player != null && (hotel != null || goal != null))
        {
            Transform target = GetTarget();

            if (target != null)
            {
                // Calculate the direction to the target
                Vector3 directionToTarget = (target.position - player.position).normalized;

                // Calculate the angle between the forward direction of the player and the direction to the target
                float angle = Vector3.SignedAngle(player.forward, directionToTarget, Vector3.up);

                // Update the rotation of the compass UI element
                transform.rotation = Quaternion.Euler(0f, 0f, -angle);

                // Change the arrow color based on the target
                arrowRenderer.color = (target == hotel) ? hotelColor : goalColor;
            }
        }
    }

    private Transform GetTarget()
    {
        // Determine which target (hotel or goal) is closer to the player
        if (hotel != null && goal != null)
        {
            float distanceToHotel = Vector3.Distance(player.position, hotel.position);
            float distanceToGoal = Vector3.Distance(player.position, goal.position);

            return (distanceToHotel < distanceToGoal) ? hotel : goal;
        }
        else if (hotel != null)
        {
            return hotel;
        }
        else if (goal != null)
        {
            return goal;
        }

        return null;
    }
}
