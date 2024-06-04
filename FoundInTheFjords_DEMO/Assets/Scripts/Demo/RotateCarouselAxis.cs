using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCarouselAxis : MonoBehaviour
{
    private Quaternion targetOrientation;
    public bool isRotating = true;
    private float time = 0;

    
    void Update()
    {
        
        if (isRotating)
        {
            //calculates target axis rotation at randomized interval (avoids collisions)
            time += Time.deltaTime;
            if (time > 10)
            {
                targetOrientation = Random.rotation;
                time = 0;
            }
            //smoothly interpolates towards target rotation

            transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientation, CarouselManager.CM.pathWanderSpeed * Time.deltaTime);
        }
        


    }
}
