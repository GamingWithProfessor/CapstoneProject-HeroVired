using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Obstacles : MonoBehaviour
{
    public TextMeshProUGUI deliveryFailedText; // Text object to display "Delivery failed"
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Delivery Failed");
            // Stop the game
            Time.timeScale = 0;
            deliveryFailedText.gameObject.SetActive(true); // Show the "Delivery failed" text
        }
    }
}