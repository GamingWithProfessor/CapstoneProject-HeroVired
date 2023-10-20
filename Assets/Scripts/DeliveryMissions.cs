using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryManager : MonoBehaviour
{
    public float timeLeft = 60f;
    public TMP_Text timerText;
    public TMP_Text resultText;
    private bool isDelivering = false;
    private bool hasCollidedWithHotel = false;

    private void Update()
    {
        if (hasCollidedWithHotel && !isDelivering)
        {
            StartDelivery();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hotel")
        {
            hasCollidedWithHotel = true;
        }
        else if (collision.gameObject.tag == "Goal" && isDelivering)
        {
            isDelivering = false;
            timerText.gameObject.SetActive(false);
            resultText.gameObject.SetActive(true);
            resultText.text = "Delivery Successful!";
            StopCoroutine(UpdateTimer());
        }
    }
    public void StartDelivery()
    {
        isDelivering = true;
        timerText.gameObject.SetActive(true);
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (isDelivering)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time Left: " + Mathf.Ceil(timeLeft).ToString();

            if (timeLeft <= 0)
            {
                isDelivering = false;
                timerText.gameObject.SetActive(false);
                resultText.gameObject.SetActive(true);
                resultText.text = "Delivery Failed!";
            }

            yield return new WaitForSeconds(1f);
        }
    }
}