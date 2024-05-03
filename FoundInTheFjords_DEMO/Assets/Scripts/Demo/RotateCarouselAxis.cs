using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCarouselAxis : MonoBehaviour
{
    private Quaternion targetOrientation;
    public bool isRotating = true;

    
    void Update()
    {
        if (isRotating)
        {
            //calculates target axis rotation at randomized interval (avoids collisions)
            if (Random.Range(1, CarouselManager.CM.pathSensitivity) < 10)
            {
                targetOrientation = Random.rotation;
            }
            //smoothly interpolates towards target rotation

            transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientation, CarouselManager.CM.pathWanderSpeed * Time.deltaTime);
        }
        


    }
}
