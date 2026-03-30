using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PrepareDocumentPackageTask : Task
{
    public Button showDocumentsButton;
    
    public GameObject[] clientsDocuments = new GameObject[4];

    public GameObject[] archiveDocuments = new GameObject[4];

    public GameObject clientFolder;
    public GameObject archiveFolder;

    public GameObject readyClientFolder;
    public Material glowMaterial;

    public TextMeshProUGUI taskText;

    public TextMeshProUGUI clientCounterText;
    public TextMeshProUGUI archiveCounterText;

    private int clientDocumentsPlaced = 0;
    private int archiveDocumentsPlaced = 0;
    private const int totalClientDocs = 4;
    private const int totalArchiveDocs = 4;

    private List<GameObject> allDocuments = new List<GameObject>();

    private void Start()
    {
        Name = TaskName.PrepareDocumentPackage;
        RUDescription = "Комплектовать пакет документов";
        ENGDescription = "Prepare document package";

        InitializeTask();
    }

    void InitializeTask()
    {
        HideAllDocuments();

        if (readyClientFolder != null)
            readyClientFolder.SetActive(false);

        if(showDocumentsButton != null)
        {
            showDocumentsButton.onClick.AddListener(ShowAllDocuments);
            showDocumentsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Показать документы";
        }

        allDocuments.Clear();
        allDocuments.AddRange(clientsDocuments);
        allDocuments.AddRange(archiveDocuments);

        UpdateCounters();
    }

    public override void Terms()
    {
        base.Terms();
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Нажмите кнопку 'Показать документы' и распределите их по папкам");
        }

    }

    


    public void ShowAllDocuments()
    {
        if (!activated) return;

        foreach (var doc in allDocuments)
        {
            if (doc != null)
                doc.SetActive(true);
        }

        if (showDocumentsButton != null)
        {
            showDocumentsButton.interactable = false;
            showDocumentsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Документы показаны";
        }

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Распределите документы по папкам:\n" +
                      "4 документа → Папка 'Для Клиента'\n" +
                      "4 документа → Папка 'В архив'");
        }
        
    }

    void HideAllDocuments()
    {
        foreach (var doc in allDocuments)
        {
            if (doc != null)
                doc.SetActive(false);
        }
    }

    public void PlaceDocumentInClientFolder(GameObject document)
    {
        if (!activated) return;


        document.SetActive(false);
        clientDocumentsPlaced++;
        Debug.Log(clientDocumentsPlaced);

        UpdateCounters();

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription($"В папку 'Для Клиента' добавлено: {clientDocumentsPlaced}/{totalClientDocs}");
        }

        if (clientDocumentsPlaced >= totalClientDocs)
        {
            if (TaskUIController.instance != null)
            {
                TaskUIController.instance.UpdateDescription("Папка 'Для Клиента' заполнена!");
            }
        }

        CheckClientFolderCompletion();


    }

    public void PlaceDocumentInArchiveFolder(GameObject document)
    {
        if (!activated) return;
        document.SetActive(false);
        archiveDocumentsPlaced++;
        Debug.Log(archiveDocumentsPlaced);

        UpdateCounters();

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription($"В папку 'В архив' добавлено: {archiveDocumentsPlaced}/{totalArchiveDocs}");
        }

        if (archiveDocumentsPlaced >= totalArchiveDocs)
        {
            if (TaskUIController.instance != null)
            {
                TaskUIController.instance.UpdateDescription("Папка 'Для Клиента' заполнена!");
            }
        }

        CheckArchiveFolderCompletion();
    }

    void CheckClientFolderCompletion()
    {
        if (clientDocumentsPlaced >= totalClientDocs)
        {
            if (clientFolder != null)
            {
                if (TaskUIController.instance != null)
                {
                    TaskUIController.instance.UpdateDescription("Папка 'Для Клиента' заполнена!");
                }
            }

            CheckAllFoldersCompletion();
        }
    }

    void CheckArchiveFolderCompletion()
    {
        if (archiveDocumentsPlaced >= totalArchiveDocs)
        {
            if (archiveFolder != null)
            {
                if (TaskUIController.instance != null)
                {
                    TaskUIController.instance.UpdateDescription("Папка 'Для архива' заполнена!");
                }
            }

            CheckAllFoldersCompletion();
        }
    }

    void CheckAllFoldersCompletion()
    {
        if (!activated) return;

        if (clientDocumentsPlaced >= totalClientDocs && archiveDocumentsPlaced >= totalArchiveDocs)
        {
            if (TaskUIController.instance != null)
            {
                TaskUIController.instance.UpdateDescription("Все документы распределены! Готовая папка доступна.");
            }
            if (readyClientFolder != null)
            {
                clientFolder.SetActive(false);
                readyClientFolder.SetActive(true);

                //if (glowMaterial != null)
                //{
                //    Renderer renderer = readyClientFolder.GetComponent<Renderer>();
                //    if (renderer != null)
                //        renderer.material = glowMaterial;
                //}
            }

            CompleteTask();
        }


    }

    void CompleteTask()
    {
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Пакет документов готов к передаче!");
        }

        Invoke("FinishTask", 3f);
    }

    void FinishTask()
    {
        Complete(TaskName.HandOverToTheCourier);
        PlaySound();
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.ShowTask("1. Возьмите папку 'Для Клиента'\n2. И подойдите к ресепшену", 900f);
        }
    }

    void UpdateCounters()
    {
        if (clientCounterText != null)
            clientCounterText.text = $"Клиент: {clientDocumentsPlaced}/{totalClientDocs}";

        if (archiveCounterText != null)
            archiveCounterText.text = $"Архив: {archiveDocumentsPlaced}/{totalArchiveDocs}";
    }

    

}
