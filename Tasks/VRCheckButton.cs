using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRCheckButton : XRSimpleInteractable
{
    public CheckTheDetailsTask task;
    public int buttonNumber = 0;


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (task == null)
        {
            task = FindObjectOfType<CheckTheDetailsTask>();
        }

        if (task != null)
        {
            task.CheckField(buttonNumber);

            enabled = false;
        }
    }
}
