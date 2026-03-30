using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CourierInteractable : XRSimpleInteractable
{
    public HandOverToTheCourierTask task;
    public string objectType;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {

        if (task == null) return;

        if(objectType == "Folder")
        {
            task.TakeFolder();
            gameObject.SetActive(false);
        }
        else if (objectType == "Courier")
        {
            task.GiveToCourier();
            enabled = false;
        }


    }
}
