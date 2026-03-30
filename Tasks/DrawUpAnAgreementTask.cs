using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DrawUpAnAgreementTask : Task
{
    public GameObject computer;
    public GameObject printer;
    public GameObject printedDocument;

    public GameObject computerScreen;
    public TMP_InputField[] inputFields;
    public Button printButton;

    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;    

    public TextMeshProUGUI taskText;

    public Outline outlinePc;
    public Outline outlinePrinter;

    private string[] correctAnswers =
    {
        "1",
        "1",
        "1",
        "1"
    };

    private bool isFormFilled = false;
    private bool isDocumentPrinted = false;
    private Image[] fieldRenderes;

    private void Start()
    {
        Name = TaskName.DrawUpAnAgreement;
        RUDescription = "Составить соглашение на компьютере";
        ENGDescription = "Draw up an agreement on the computer";

        

        InitializeComputer();
    }

    void InitializeComputer()
    {
        if (computerScreen != null)
        {
            computerScreen.SetActive(true);            
        }
            


        if (printer != null)
            printer.SetActive(true);

        if (printedDocument != null)
            printedDocument.SetActive(false);

        if (printButton != null)
        {
            printButton.onClick.AddListener(OnPrintButtonClicked);
            printButton.interactable = false;
        }

        if (inputFields != null)
        {
            fieldRenderes = new Image[inputFields.Length];

            for (int i = 0; i < inputFields.Length; i++)
            {
                int index = i;

                inputFields[i].onValueChanged.AddListener((text) =>
                {
                    OnFieldValueChanged(index, text);
                });

                fieldRenderes[i] = inputFields[i].GetComponent<Image>();

            }
        } 
    }

    public override void Terms()
    {
        base.Terms();
        if (computer != null)
        {
            computer.SetActive(true);
            outlinePc.enabled = true;
        }

        if (printer != null)
            printer.SetActive(true);

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Подойдите к компьютеру и заполните форму");
        }

    }

    public void OnComputerClick()
    {
        if (!activated) return;

        if (computerScreen != null)
        {
            outlinePc.enabled = true;
            computerScreen.SetActive(true);
            //UpdateTaskText("Заполните все поля формы. Примеры:\n" +
            //              "Дата: 15.01.2024\n" +
            //              "Номер: ДГ-2024-001\n" +
            //              "Контрагент: ООО 'Ромашка'\n" +
            //              "Изменения: Изменение сроков поставки на 30 дней");
            if (TaskUIController.instance != null)
            {
                TaskUIController.instance.UpdateDescription("Заполните все поля формы. Примеры:\n" +
                          "1\n" +
                         "1\n" +
                         "1\n" +
                         "1");
            }

        }
    }

    private void OnFieldValueChanged(int filedIndex, string text)
    {
        if (!activated || fieldRenderes == null || filedIndex >= fieldRenderes.Length)
            return;

        bool isCorrect = text.Trim() == correctAnswers[filedIndex];

        if (fieldRenderes[filedIndex] != null)
        {
            fieldRenderes[filedIndex].color = isCorrect ? correctColor : wrongColor;

            CheckAllFields();
        }
    }

    private void CheckAllFields()
    {
        if (inputFields == null)
            return;

        isFormFilled = true;

        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i].text.Trim() != correctAnswers[i])
            {
                isFormFilled = false;
                break;
            }

            
        }

        if (printButton != null)
        {
            printButton.interactable = isFormFilled;

            if (isFormFilled)
            {
                if (TaskUIController.instance != null)
                {
                    TaskUIController.instance.UpdateDescription("Все поля заполнены верно! Нажмите 'Распечатать'");
                }
            }
        }
    }

    public void OnPrintButtonClicked()
    {
        if (!activated || !isFormFilled) return;

        if (computerScreen != null)
        {
            computerScreen.SetActive(false);
            outlinePrinter.enabled = true;
            outlinePc.enabled = false;
        }
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Документ отправлен на печать. Подойдите к принтеру");
        }
    }

    public void OnPrinterClicked()
    {
        if (!activated || !isFormFilled) return;

        if (printedDocument != null) 
        {
            printedDocument.SetActive(true);
            isDocumentPrinted = true;
            if (TaskUIController.instance != null)
            {
                TaskUIController.instance.UpdateDescription("Документ распечатан! Возьмите его");
            }
        }
    }

    public void OnDocumentPickedUp()
    {
        if (!activated || !isDocumentPrinted) return;

        if (printedDocument != null)
            printedDocument.SetActive(false);

        CompleteTask();

    }

    private void CompleteTask()
    {
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Соглашение составлено и распечатано!");
        }

        outlinePrinter.enabled = false;

        Invoke("GoToNextTask", 2f);
    }

    private void GoToNextTask()
    {
        Complete(TaskName.CheckTheDetails);
        PlaySound();

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.ShowTask("Проверьте реквизиты. Нажмите все 3 кнопки проверки", 900f);
        }
    }

    //private void UpdateTaskText(string message)
    //{
    //    if (taskText != null)
    //        taskText.text = message;
    //}

    public void CloseComputerScreen()
    {
        if (computerScreen != null)
            computerScreen.SetActive(false);
    }
}


