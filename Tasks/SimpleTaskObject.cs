using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction;

public class SimpleTaskObject : MonoBehaviour
{
    public FindDocumentsInArchiveTask task;

    public void Interact()
    {
        if (task == null)
        {
            Debug.LogError("Task не назначен!");
            return;
        }

        if (gameObject.CompareTag("Box"))
        {
            Debug.Log("Коробка нажата!");
            task.OpenBox();
            gameObject.SetActive(false);
        }
        else if (gameObject.CompareTag("Folder"))
        {
            Debug.Log("Папка нажата!");
            task.CollectFolder(gameObject);
            gameObject.SetActive(false);
        }
    }
}
