using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class HandOverToTheCourierTask : Task
{
    public GameObject clientFolder;
    public GameObject courier;
    public GameObject receptonDesk;

    public TextMeshProUGUI taskText;
    public TextMeshProUGUI timerText;

    public float timeLimit = 60f;

    

    public Dialog dialogScript;

    private string message = "Привет, я курьер, передай мне эти документы пожайлуста, я устал ждать!!!";

    private float timeRemaining;
    private bool timeRunning;

    private bool hasFolder = false;
    private bool atReception = false;
    private bool courierFound = false;



    private void Start()
    {
        Name = TaskName.HandOverToTheCourier;
        RUDescription = "Передать папку курьеру до 18:00";
        ENGDescription = "Hand over folder to courier before 6 PM";

        InitializeTask();
    }

    void InitializeTask()
    {
        if (courier != null)
            courier.SetActive(false);

        hasFolder = false;
        atReception = false;
        courierFound = false;

        timeRemaining = timeLimit;
        UpdateTimerDisplay();

    }
    public override void Terms()
    {
        base.Terms();

        if (clientFolder != null)
            clientFolder.SetActive(true);

        timeRunning = true;

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("1. Возьмите папку 'Для Клиента'\n2. И подойдите к ресепшену");
        }
    }

    private void Update()
    {
        if (!activated || !timeRunning) return;

        timeRemaining -= Time.deltaTime;
        UpdateTimerDisplay();

        if (timeRemaining <= 0)
        {
            TimeExpired();
        }
    }
    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = $"До 18:00: {minutes:00}:{seconds:00}";

            if (timeRemaining < 30f)
                timerText.color = Color.red;
            else if (timeRemaining < 130f)
                timerText.color = Color.yellow;
            else
                timerText.color = Color.green;
        }

    }

    public void TakeFolder()
    {
        if (!activated) return;

        hasFolder = true;

        if (clientFolder != null)
            clientFolder.SetActive(false);

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Папка взята\n2. Идите к стойке ресепшена");
        }
    }

    public void ArriveAtReception()
    {
        if (!activated || !hasFolder) return;

        atReception = true; 
        dialogScript.StartDialog(message);

        if (courier != null)
        {
            courier.SetActive(true);
            courierFound = true;
        }

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Вы у ресепшена\n3. Передайте папку курьеру");
        }
    }

    public void GiveToCourier()
    {
        if (!activated || !hasFolder || !atReception || !courierFound) return;

        timeRunning = false;

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Папка передана курьеру!\nЗадание выполнено!");
        }

        Invoke("CompleteTask", 2f);
    }

    void TimeExpired()
    {
        timeRunning = false;

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Время вышло! Курьер уехал.\nЗадание провалено.");
        }

        if (courier != null)
            courier.SetActive(false);

        Invoke("RestartTask", 3f);
    }

    void RestartTask()
    {
        InitializeTask();
        Terms();

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Попробуйте снова!\n1. Возьмите папку 'Для Клиента'");
        }
    }

    void CompleteTask()
    {
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Все задания выполнены! Рабочий день окончен.");
        }
        Complete(TaskName.AllTaskDone);
        PlaySound();
    }

    
}
