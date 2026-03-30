using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenu : MonoBehaviour
{
    public GameObject handMenuObj;

    private void Update()
    {
        if (transform.localEulerAngles.z > 80 && transform.localEulerAngles.z < 280)
            handMenuObj.SetActive(true);
        else
            handMenuObj.SetActive(false);
    }
}
