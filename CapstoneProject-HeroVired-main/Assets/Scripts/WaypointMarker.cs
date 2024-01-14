using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class WaypointMarker : MonoBehaviour
{
    public Image img;
    public Transform player;
    public TextMeshProUGUI meterText;

    private float minimumX;
    private float maximumX;
    private float minimumY;
    private float maximumY;

    void Start()
    {
        minimumX = img.GetPixelAdjustedRect().width / 2;
        maximumX = Screen.width - minimumX;
        minimumY = img.GetPixelAdjustedRect().height / 2;
        maximumY = Screen.height - minimumY;
    }

    void Update()
    {
        // Assuming you have a currentGoal variable representing the current goal
        if (currentGoal != null)
        {
            Vector2 position = Camera.main.WorldToScreenPoint(currentGoal.position);
            position.x = Mathf.Clamp(position.x, minimumX, maximumX);
            position.y = Mathf.Clamp(position.y, minimumY, maximumY);

            if (Vector3.Dot(Camera.main.transform.forward, currentGoal.position - transform.position) < 0)
            {
                if (position.x < Screen.width / 2)
                {
                    position.x = maximumX;
                }
                else
                {
                    position.x = minimumX;
                }
            }

            img.transform.position = position;

            float distanceToGoal = Vector3.Distance(player.position, currentGoal.position);
            meterText.text = ((int)distanceToGoal).ToString() + "m";
        }
    }

    public void SetTargets(List<Transform> goals, Transform playerTarget)
    {
        if (goals.Count > 0)
        {
            currentGoal = goals[0]; // Set the current goal
            Vector3 targetPosition = currentGoal.position;
            img.transform.position = Camera.main.WorldToScreenPoint(targetPosition);
        }
    }

    public void EnableMarker(bool enable)
    {
        img.gameObject.SetActive(enable);
    }

    private Transform currentGoal; // Variable to store the current goal
}
