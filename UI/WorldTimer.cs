using TMPro;
using UnityEngine;

public class WorldTimer : MonoBehaviour
{
    public TextMeshProUGUI timer;

    public int startHour = 12;
    public int startMinute = 0;
    public float timerSpeed = 30f;
    public float fastSpeed = 400f;

    private float totalMinutes;
    private float currentSpeed;
    private bool isFastMode = false;

    private void Start()
    {
        totalMinutes = (startHour * 60) + startMinute;
        currentSpeed = timerSpeed;
    }

    private void Update()
    {
        totalMinutes += Time.deltaTime * currentSpeed / 60f;

        if (totalMinutes > 1440)
            totalMinutes -= 1440;

        UpdateDisplay();   
    }

    public void ToggleFastMode()
    {
        isFastMode = !isFastMode;

        if (isFastMode)
        {
            currentSpeed = fastSpeed;
        }
        else
        {
            currentSpeed = timerSpeed;
        }
    }

    public bool IsFastMode()
    {
        return isFastMode;
    }

    public void Add5Minutes()
    {
        totalMinutes += 5;
        CheckLoop();
        UpdateDisplay();
    }   

    private void CheckLoop()
    {
        if (totalMinutes > 1440)
            totalMinutes -= 1440;
        else if (totalMinutes < 0)
            totalMinutes += 1440;
    }

    void UpdateDisplay()
    {
        int hours = Mathf.FloorToInt(totalMinutes / 60);
        int minutes = Mathf.FloorToInt(totalMinutes % 60);

        timer.text = $"{hours:00}:{minutes:00}";
    }



}
