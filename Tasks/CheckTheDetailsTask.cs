using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckTheDetailsTask : Task
{
    public Button checkButton1;
    public Button checkButton2;
    public Button checkButton3;
    public Button completeButton;

    public GameObject[] checkObjects;
    public Material checkedMaterial;

    public GameObject verifiedHologram;
    public TextMeshProUGUI taskText;

    public Color checkedColor = Color.green;
    public Color defaultColor = Color.white;

    public int checksCompleted = 0;

    public Outline outline;

    private const int totalChecks = 3;

    private void Start()
    {
        Name = TaskName.CheckTheDetails;

        RUDescription = "Проверить реквизиты платежного поручения";
        ENGDescription = "Check payment order details";

        InitializeTask();
    }

    void InitializeTask()
    {
        if (verifiedHologram != null) 
            verifiedHologram.SetActive(false);

        if (completeButton != null)
        {
            completeButton.interactable = false;
            completeButton.onClick.AddListener(OnCompleteClicked);
        }
        

        SetupCheckButtons();

    }

    void SetupCheckButtons()
    {
        if (checkButton1 != null)
        {
            checkButton1.onClick.AddListener(() => CheckField(0, checkButton1));
            SetButtonText(checkButton1, "Проверить название компании");
        }

        if (checkButton2 != null)
        {
            checkButton2.onClick.AddListener(() => CheckField(1, checkButton2));
            SetButtonText(checkButton2, "Проверить ИНН");
        }

        if (checkButton3 != null)
        {
            checkButton3.onClick.AddListener(() => CheckField(2, checkButton3));
            SetButtonText(checkButton3, "Проверить расчетный счет");
        }
    }

    public override void Terms()
    {
        base.Terms();
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Проверьте реквизиты. Нажмите все 3 кнопки проверки");
        }
    }

    public void CheckField(int buttonIndex)
    {
        Debug.Log($"VR кнопка нажата: индекс {buttonIndex}");

        Button button = null;
        switch (buttonIndex)
        {
            case 0:
                button = checkButton1;
                Debug.Log("Найдена UI кнопка 1");
                break;
            case 1:
                button = checkButton2;
                Debug.Log("Найдена UI кнопка 2");
                break;
            case 2:
                button = checkButton3;
                Debug.Log("Найдена UI кнопка 3");
                break;
        }

        if (button != null)
        {
            Debug.Log($"Вызываем CheckField({buttonIndex}, {button.name})");
            CheckField(buttonIndex, button);
        }
        else
        {
            Debug.LogError("UI кнопка не найдена!");
        }
    }

    void CheckField(int fieldIndex, Button button)
    {
        if (!activated) return;

        checksCompleted++;

        if (button != null)
        {
            var colors = button.colors;
            colors.normalColor = checkedColor;
            colors.selectedColor = checkedColor;
            colors.highlightedColor = checkedColor;
            button.colors = colors;

            SetButtonText(button, "Проверено");

            button.interactable = false;
            Debug.Log(checksCompleted);
            Debug.Log("");

            int copyChecks = checksCompleted - 1;

            if (fieldIndex >= 0 && fieldIndex < checkObjects.Length)
            {
                Renderer objRend = checkObjects[fieldIndex].GetComponent<Renderer>();
                if (objRend != null)
                {
                    objRend.material = checkedMaterial;
                }
                else
                {
                    Debug.LogError("нет компонента render");
                }
        }

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription($"Проверено: {checksCompleted}/{totalChecks}");
        }
        
        if (checksCompleted >= totalChecks)
        {
            EnableCompleteButton();
        }
    }

    void EnableCompleteButton()
    {
        if (completeButton != null)
        {
            completeButton.interactable = true;
            if (TaskUIController.instance != null)
            {
                TaskUIController.instance.UpdateDescription("Все проверено! Нажмите 'Завершить проверку'");
            }
        }
    }}

    public void OnCompleteClicked()
    {
        if (!activated) return;

        if (verifiedHologram != null)
        {
            verifiedHologram.SetActive(true);
            if (TaskUIController.instance != null)
            {
                TaskUIController.instance.UpdateDescription("Проверка завершена! Голограмма 'VERIFIED' активирована.");
            }

            Invoke("CompleteTask", 2f);
        }
    }

    void CompleteTask()
    {
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("\"Реквизиты проверены успешно!\"");
        }
        Complete(TaskName.CertifyCopiesWithSeal);
        PlaySound();

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.ShowTask("Возьмите печать и поставьте на оба документа", 900f);
        }
    }

    void SetButtonText(Button button, string text) 
    {
        TextMeshProUGUI txt = button.GetComponent<TextMeshProUGUI>();
        if (txt != null)
            txt.text = text;
    }

    
}
