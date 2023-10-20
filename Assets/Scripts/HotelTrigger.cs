using UnityEngine;

public class HotelTrigger : MonoBehaviour
{
    public GameObject timer; // Reference to the "Timer" game object

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the object entering the trigger is the player
        {
            timer.SetActive(true); // Set the "Timer" game object to active
        }
    }
}