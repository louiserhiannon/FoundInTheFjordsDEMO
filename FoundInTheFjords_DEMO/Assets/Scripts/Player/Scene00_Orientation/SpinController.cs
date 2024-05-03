using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpinController : MonoBehaviour
{
    public InputActionReference spinReference = null;
    private Transform xrRig;
    private float zRotation;
    private float zRotationInitial;
    public float rotationSpeed;

    private void Awake()
    {
        xrRig = GetComponent<Transform>();
        spinReference.action.started += Spin;
        
    }

    private void OnDestroy()
    {
        spinReference.action.started -= Spin;
    }

    private void Spin(InputAction.CallbackContext context)
    {
        zRotationInitial = xrRig.transform.localEulerAngles.z;
        zRotation = 0f;
        StartCoroutine(Spin360());
    }

    IEnumerator Spin360()
    {
        while(zRotation <= 360)
        {
            float xRotation = xrRig.transform.localEulerAngles.x;
            float yRotation = xrRig.transform.localEulerAngles.y;
            float zRotation = xrRig.transform.localEulerAngles.z + (rotationSpeed * Time.deltaTime);
                
            xrRig.transform.localEulerAngles = new Vector3 (xRotation, yRotation, zRotation);
            zRotation += rotationSpeed * Time.deltaTime;
        }
        yield return null;
    }
}
