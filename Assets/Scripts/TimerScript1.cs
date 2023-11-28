using UnityEngine;
using TMPro; // This namespace is required for TextMeshPro

public class CountdownTimer : MonoBehaviour
{
   public float timeRemaining = 60f; // The initial time remaining in seconds
   public TextMeshProUGUI timerText; // The TextMeshPro text UI element that will display the timer
   public TextMeshProUGUI deliveryFailedText; // The TextMeshPro text UI element that will display the "Delivery Failed" text
   public TextMeshProUGUI deliverySuccessfulText; // The TextMeshPro text UI element that will display the "Delivery Successful" text
   public TextMeshProUGUI fiveStarText; // The TextMeshPro text UI element that will display the "Yeahh... you got 5 stars!" text
   public TextMeshProUGUI fourStarText; // The TextMeshPro text UI element that will display the "4 stars Still you can do Better" text

   private bool timerStarted = false; // Whether the timer has started
   private GameObject hotelObject; // The hotel object that will start the timer
   private GameObject goalObject; // The goal object that will stop the timer

   void Start()
   {
     hotelObject = GameObject.Find("Hotel"); // Find the hotel object in the scene
     goalObject = GameObject.Find("Goal"); // Find the goal object in the scene

     // Hide the timer, delivery failed, delivery successful, and five star texts by default
     timerText.gameObject.SetActive(false);
     deliveryFailedText.gameObject.SetActive(false);
     deliverySuccessfulText.gameObject.SetActive(false);
     fiveStarText.gameObject.SetActive(false);
     fourStarText.gameObject.SetActive(false);
   }

   void Update()
   {
     // If the timer has not started and the player is touching the hotel object, start the timer and show the timer text
     if (!timerStarted && playerIsTouchingObject(hotelObject))
     {
       timerStarted = true;
       timerText.gameObject.SetActive(true);
     }

     // If the timer has started, count it down by the delta time
     if (timerStarted)
     {
       timeRemaining -= Time.deltaTime;
     }

     // If the player is touching the goal object and the timer has not reached zero, stop the timer and show the "Delivery Successful" text
     if (timerStarted && playerIsTouchingObject(goalObject) && timeRemaining > 0)
     {
       timerStarted = false;
       timerText.gameObject.SetActive(false);
       deliverySuccessfulText.gameObject.SetActive(true);

       // If the player reached the goal in less than 10 seconds, show the "Yeahh... you got 5 stars!" text
       if (timeRemaining > 10)
       {
         fiveStarText.gameObject.SetActive(true);
       }

       // If the player reached the goal in less than 5 seconds, show the "4 stars Still you can do Better" text
       if (timeRemaining > 5 && timeRemaining <= 10)
       {
         fourStarText.gameObject.SetActive(true);
       }
     }

     // If the timer has reached zero, stop it and show the "Delivery Failed" text
     if (timeRemaining <= 0)
     {
       timerStarted = false;
       timerText.gameObject.SetActive(false);
       deliveryFailedText.gameObject.SetActive(true);
     }

     // Update the timer text UI element
     timerText.text = timeRemaining.ToString("0");
   }

   // Returns true if the player is touching the given object, false otherwise
   private bool playerIsTouchingObject(GameObject objectToTest)
   {
     // Get the player's position
     Vector3 playerPosition = GameObject.Find("Player").transform.position;

     // Get the object's position
     Vector3 objectPosition = objectToTest.transform.position;

     // Calculate the distance between the player and the object
     float distance = Vector3.Distance(playerPosition, objectPosition);

     // If the distance is less than the player's touch radius, then the player is touching the object
     return distance < 1f;
   }
}