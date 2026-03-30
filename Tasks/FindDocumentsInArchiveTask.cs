using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindDocumentsInArchiveTask : Task
{
    public GameObject closedBox;
    public GameObject openBox;
    public GameObject[] folders;

    //public TextMeshProUGUI taskText;

    private int collectedFolders = 0;
    private const int totalFolders = 3;

    private void Start()
    {
        Name = TaskName.FindDocumentsInArchive;
        //RUDescription = "Найти 3 папки в архивном деле";
        //ENGDescription = "Find 3 folders in archive";

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.ShowTask(GetDescription(), 900f);
        }

        HideAllFolders();
    }

    public override void Terms()
    {
        base.Terms();

        if (closedBox != null) {
            closedBox.SetActive(true);
        }

        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.UpdateDescription("Нажмите на коробку, чтобы открыть");
        }
    }

    //private void UpdateTaskText(string message)
    //{
    //    if (taskText != null)
    //    {
    //        taskText.text = message;
    //    }
    //}

    private void HideAllFolders()
    {
        if (folders == null) return;
        foreach (GameObject folder in folders)
        {
            if (folder != null)
            {
                folder.SetActive(false);
            }
        }
    }

    private void ShowAllFolders()
    {
        if (folders == null) return;
        foreach (GameObject folder in folders)
        {
            if (folder != null)
            {
                folder.SetActive(true);
            }
        }
        if (TaskUIController.instance != null)
        {
            string progressText;
            if (LanguageControllers.instance != null && LanguageControllers.instance.currentLanguage == Language.English)
                progressText = $"Folders collected: 0/{totalFolders}";
            else
                progressText = $"Собрано папок: 0/{totalFolders}";

            TaskUIController.instance.UpdateDescription(progressText);
        }
    }

    public void OpenBox()
    {
        if (!activated) return;

        if (closedBox != null)
        {
            closedBox.SetActive(false);
            openBox.SetActive(true);

        }
        

        ShowAllFolders();
    } 

    public void CollectFolder(GameObject folder)
    {
        if (!activated) return;

        folder.SetActive(false);
        collectedFolders++;

        if (TaskUIController.instance != null)
        {
            string progressText;
            
            if (LanguageControllers.instance != null && LanguageControllers.instance.currentLanguage == Language.English)
                progressText = $"Folders collected: {collectedFolders}/{totalFolders}";
            else
                progressText = $"Собрано папок: {collectedFolders}/{totalFolders}";

            TaskUIController.instance.UpdateDescription(progressText);
        }

        if (collectedFolders >= totalFolders)
        {
            CompleteTask();
        }
    }

    public void CompleteTask()
    {
        Complete(TaskName.DrawUpAnAgreement);
        PlaySound();
        if (TaskUIController.instance != null)
        {
            TaskUIController.instance.ShowTask("Подойдите к компьютеру и заполните форму", 900f);
        }
    }
}