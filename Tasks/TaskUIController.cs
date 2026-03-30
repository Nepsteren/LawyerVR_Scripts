using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskUIController : MonoBehaviour
{
    public static TaskUIController instance;

    public TextMeshProUGUI textTask;
    public TextMeshProUGUI timerTask;

    private float currentTime;
    private bool isTimerActive = false; 

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (isTimerActive)
        {
            currentTime -= Time.deltaTime;

            if(currentTime <= 0)
            {
                currentTime = 0;
                isTimerActive = false;
            }
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerTask.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void ShowTask(string description, float timerTask)
    {
        textTask.text = description;
        currentTime = timerTask;
        isTimerActive = true;
    }

    public void UpdateDescription(string description)
    {
        textTask.text = description;
    }

    public void HideTask() 
    {
        textTask.text = "";
        timerTask.text = "";
        isTimerActive = false;
    }

    public void HideTimer()
    {
        timerTask.text = "";
        isTimerActive = false;
    }
}
