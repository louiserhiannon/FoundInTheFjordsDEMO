using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorObjectMotionController : MonoBehaviour
{
    public Transform reflectedObject;
    private Vector3 reflectionLocation;
    private float reflectionLocationX;
    private float reflectionLocationY;
    private float orcaLocationZ;
    public float transformOffset;
    public bool rotatable = false;
    private float surfaceLocation = 2f;
    public float reflectionMultiplier;
    

    // Update is called once per frame
    void Update()
    {
        MoveReflection();
        RotateReflection();
    }

    public void MoveReflection()
    {
        reflectionLocationX = reflectedObject.position.x;
        orcaLocationZ = reflectedObject.position.z;
        reflectionLocationY = transformOffset + surfaceLocation + reflectionMultiplier*(surfaceLocation - reflectedObject.position.y);
        reflectionLocation = new Vector3(reflectionLocationX, reflectionLocationY, orcaLocationZ);
        transform.position = reflectionLocation;

    }

    public void RotateReflection()
    {
          
        if(rotatable == true)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180 + reflectedObject.localEulerAngles.z, -reflectedObject.localEulerAngles.z);
        }
        return;
    }
}
