using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OpenDoor : MonoBehaviour
{
    private XRSimpleInteractable simpleInteractable;
    public Transform doorHingle;
    private int limit = 0;

    private void Start()
    {
        simpleInteractable = GetComponent<XRSimpleInteractable>();
        simpleInteractable.selectEntered.AddListener(DoorOpen);
    }

    void DoorOpen(SelectEnterEventArgs interactor)
    {
        if (limit == 0)
        {
            doorHingle.Rotate(0, -90, 0);
            limit = 1;
        }
        else
        {
            doorHingle.Rotate(0, 90, 0);
            limit = 0;
        }

    }
}
