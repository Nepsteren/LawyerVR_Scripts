using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteractable : MonoBehaviour
{
    public DrawUpAnAgreementTask task;

    public void Interact()
    {
        if (task == null)
        {
            task = FindObjectOfType<DrawUpAnAgreementTask>();
        }

        if (task == null)
        {
            Debug.LogError("DrawUpAnAgreementTask не найден!");
            return;
        }

        if (gameObject.CompareTag("Computer"))
        {
            task.OnComputerClick();
        }
        else if (gameObject.CompareTag("Printer"))
        {
            task.OnPrinterClicked();
        }
        else if (gameObject.CompareTag("Document"))
        {
            task.OnDocumentPickedUp();
            gameObject.SetActive(false);
        }
    }
}
