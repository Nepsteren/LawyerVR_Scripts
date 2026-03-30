using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentTrigger : MonoBehaviour
{
    public CertifyCopiesWithSealTask task;
    public int documentNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (task == null) return;

        if (other.CompareTag("Seal") || other.gameObject.name.Contains("Seal"))
        {
            task.StampDocument(documentNumber);
            Debug.Log("Печать по идее поставлена");
        }
    }
}
