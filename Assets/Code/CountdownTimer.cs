using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public GameObject GameoverObject;
    public AudioClip _clip;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    // Flag to check whether the game is over
    private Action onTimerExpired;

    public void Init(float duration, Action onExpiredCallback)
    {
        remainingTime = duration;
        onTimerExpired = onExpiredCallback;
    }

    private void Start()
    {
        // Memutar backsound saat memulai permainan
        SoundManager.Instance.PlayBacksound(_clip);
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
            {
                remainingTime = 0;
            }
        }
        else
        {
            remainingTime = 0;
            // Game over
            timerText.color = Color.red;
            GameoverObject.SetActive(true);

            // Stop background music when the game is over
            SoundManager.Instance.StopBacksound();

            // Call the callback when the timer expires
            if (onTimerExpired != null)
            {
                onTimerExpired.Invoke();
            }
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
