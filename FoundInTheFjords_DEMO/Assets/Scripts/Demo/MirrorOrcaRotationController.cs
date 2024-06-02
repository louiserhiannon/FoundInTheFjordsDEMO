using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorOrcaRotationController : MonoBehaviour
{
    public Transform reflectedObject;

    void Update()
    {
        RotateReflection();
    }

    private void RotateReflection()
    {
        transform.localEulerAngles = new Vector3(reflectedObject.localEulerAngles.x, 180 - reflectedObject.localEulerAngles.y, transform.localEulerAngles.z);
        return;
    }
}
