using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CertifyCopiesWithSealTask : Task
{
    public Image charterDocumentImage;
    public Image innDocumentImage;

    public Image charterStampImage;
    public Image innStampImage;

    public GameObject sealObject3D;

    public TextMeshProUGUI taskText;

    public Color stampedColor = new Color(0.8f, 1f, 0.8f, 1f);

    private int stampedCount = 0;
    private const int totalDocuments = 2;

    private void Start()
    {
        Name = TaskName.CertifyCopiesWithSeal;
        RUDescription = "Заверить копии документов печатью";
        ENGDescription = "Certify document copies with company seal";

        InitializeTask();
    }

    void InitializeTask()
    {
        if (charterStampImage != null)
            charterStampImage.gameObject.SetActive(false);

        if (innStampImage != null)
            innStampImage.gameObject.SetActive(false);

        if (charterDocumentImage != null)
            charterDocumentImage.color = new Color(1, 1, 1, 0.9f);

        if (innDocumentImage != null)
            innDocumentImage.color = new Color(1, 1, 1, 0.9f);
    }

    public override void Terms()
    {
        base.Terms();
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Возьмите печать и поставьте на оба документа");
        }
    }

    public void StampDocument(int docNum)
    {
        if (!activated) return;

        bool wasStamped = false;

        if (docNum == 1)
        {
            if (charterStampImage != null && !charterStampImage.gameObject.activeSelf)
            {
                charterStampImage.gameObject.SetActive(true);
                wasStamped = true;
            }

            if (charterDocumentImage != null)
            {
                charterDocumentImage.color = stampedColor;
            }
        }
        else if (docNum == 2)
        {
            if (innStampImage != null && !innStampImage.gameObject.activeSelf)
            {
                innStampImage.gameObject.SetActive(true);
                wasStamped = true;
            }

            if (innDocumentImage != null)
                innDocumentImage.color = stampedColor;
        }

        if (wasStamped)
        {
            stampedCount++;
            if (TaskUIController.instance != null)
            {
                TaskUIController.instance.UpdateDescription($"Заверено документов: {stampedCount}/{totalDocuments}");
            }
            if (stampedCount >= totalDocuments)
            {
                CompleteTask();
            }
        }
    }

    void CompleteTask()
    {
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Все документы заверены печатью!");
        }

        if (charterDocumentImage != null)
            charterDocumentImage.color = Color.green;

        if (innDocumentImage != null)
            innDocumentImage.color = Color.green;

        Invoke("FinishTask", 2f);
    }

    void FinishTask()
    {
        Complete(TaskName.PrepareDocumentPackage);
        PlaySound();
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.ShowTask("Нажмите кнопку 'Показать документы' и распределите их по папкам", 900f);
        }
    }

    


}
