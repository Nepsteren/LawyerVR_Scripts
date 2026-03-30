using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PackageDocuments : XRGrabInteractable
{
    public PrepareDocumentPackageTask task;

    public bool isForClient;

    public string clientFolderTag = "ClientFolder";
    public string archiveFolderTag = "ArchiveFolder";

    private bool isPlaced = false;

    private void Start()
    {
        if (task == null)
            task = FindObjectOfType<PrepareDocumentPackageTask>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaced) return;
        if (task == null) return;

        if (other.CompareTag(clientFolderTag) && isForClient)
        {
            task.PlaceDocumentInClientFolder(gameObject);
            isPlaced = true;
            gameObject.SetActive(false);
        }
        else if (other.CompareTag(archiveFolderTag) && !isForClient)
        {
            task.PlaceDocumentInArchiveFolder(gameObject);
            isPlaced = true;
            gameObject.SetActive(false);
        }
        else if (((other.CompareTag(clientFolderTag) && !isForClient) ||
                 (other.CompareTag(archiveFolderTag) && isForClient)))
        {
            Debug.Log("Неверная папка! Документ вернется на место");
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (isPlaced) return;

    //    if ((isForClient && other.CompareTag(clientFolderTag)) ||
    //        (!isForClient && other.CompareTag(archiveFolderTag)))
    //    {
    //        GetComponent<Renderer>().material.color = Color.green;
    //    }
    //    else if ((isForClient && other.CompareTag(archiveFolderTag)) ||
    //             (!isForClient && other.CompareTag(clientFolderTag)))
    //    {
    //        GetComponent<Renderer>().material.color = Color.red;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (isPlaced) return;

    //    if (other.CompareTag(clientFolderTag) || other.CompareTag(archiveFolderTag))
    //    {
    //        GetComponent<Renderer>().material.color = Color.white;
    //    }
    //}


}
