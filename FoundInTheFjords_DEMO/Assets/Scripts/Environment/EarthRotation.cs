using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour
    //Controls rotation of the Earth
    
{
    public float rotateSpeed;
    public float rotationTime;
    public bool isRotating = true;

    void Update()
    {
        //rotates the earth on a vertical axis until a specified time point is reached
        if (isRotating)
        {
            transform.Rotate(-Vector3.up, rotateSpeed * Time.deltaTime);
        }
        
    }
}
