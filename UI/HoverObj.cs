using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverObj : MonoBehaviour
{
    public GameObject tipPanel;

    private XRGrabInteractable grab;

    private void Start()
    {
        grab = GetComponent<XRGrabInteractable>();

        grab.hoverEntered.AddListener((args) => tipPanel.SetActive(true));
        grab.hoverExited.AddListener((args) => tipPanel.SetActive(false));

        tipPanel.SetActive(false);
    }
}
