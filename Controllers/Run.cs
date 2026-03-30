using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Run : MonoBehaviour
{
    public float walkSpeed = 1f;
    public float runSpeed = 3f;

    private ContinuousMoveProviderBase moveProvider;
    private InputDevice leftHand;
    private InputDevice rightHand;

    void Start()
    {
        moveProvider = GetComponent<ContinuousMoveProviderBase>();
        leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        if (moveProvider == null) return;

        bool leftStickPressed = false;
        bool rightStickPressed = false;

        if (leftHand.isValid)
            leftHand.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out leftStickPressed);

        if (rightHand.isValid)
            rightHand.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rightStickPressed);

        if (leftStickPressed || rightStickPressed)
        {
            moveProvider.moveSpeed = runSpeed;
        }
        else
        {
            moveProvider.moveSpeed = walkSpeed;
        }
    }
}