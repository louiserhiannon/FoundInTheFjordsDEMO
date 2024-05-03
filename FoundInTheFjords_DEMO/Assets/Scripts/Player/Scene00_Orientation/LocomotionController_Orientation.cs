using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController_Orientation : LocomotionController
{
    [SerializeField] private Vector3 newLocalEulerAngle;
    private Vector3 rotateAxis = new Vector3(-1, 0, 0);
    public float angularSpeed;
    //public Transform jellyTransform;

    public override void MoveForwardRelativeToCamera(float relativeForwardSpeed)
    {
        moveSpeed = relativeForwardSpeed * maxSpeed;
        if (moveSpeed == 0f)
        {
            timeAtZero += Time.deltaTime;
            if (timeAtZero > 2f)
            {
                currentSpeed = 0f;
                timeAtZero = 0f;
            }
        }

        if (currentSpeed < moveSpeed)
        {
            currentSpeed += acceleration;
        }

        if (currentSpeed > moveSpeed)
        {
            currentSpeed -= acceleration;
        }

        if(transform.localEulerAngles.x > 0f && transform.localEulerAngles.x <= 90f && currentSpeed > 0f)
        {
            transform.Rotate(rotateAxis, angularSpeed * Time.deltaTime, Space.Self);
            //jellyTransform.Rotate(rotateAxis, angularSpeed * Time.deltaTime, Space.Self);
        }
        
        if(transform.localEulerAngles.x <= 0f)
        {
            transform.localEulerAngles = new Vector3 (0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        //if(moveSpeed > 0)
        //{
        //    if (DepthCalculator.dc.headsetDepthCorrected < 0.5f)
        //    {
        //        xrRig.transform.Translate(currentSpeed * Time.deltaTime * transform.forward, Space.World);
        //    }
        //}
        //else
        //{
        //    xrRig.transform.Translate(currentSpeed * Time.deltaTime * transform.forward, Space.World);
        //}

        transform.Translate(currentSpeed * Time.deltaTime * transform.forward, Space.World);

        //Vector3.up
    }



    public override void MoveSidewaysRelativeToCamera(float relativeSidewaysSpeed)
    {
        //translate sideways
        sidewaysSpeed = relativeSidewaysSpeed * maxTranslateSpeed;
        Vector3 sidewaysVector = new Vector3(sidewaysSpeed, 0, 0); 
        transform.Translate(sidewaysVector * Time.deltaTime, Space.World);

        //Rotate along local z axis
        spinAngleMax = 35f;
        //Add smoothing effect

        desiredAngle = -relativeSidewaysSpeed * spinAngleMax; //desired angle is between -25 (right) and +25 (left)
        rotationToDesiredAngle = (desiredAngle - currentAngle) / smoothingFactor;
        updatedAngle = currentAngle + rotationToDesiredAngle;
        newLocalEulerAngle = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, updatedAngle);
        transform.localEulerAngles = newLocalEulerAngle;

        currentAngle = updatedAngle;

    }

    public override void JumpUp(InputAction.CallbackContext context)
    {
        xrRig.Translate(0f, 0f, 0f);
    }

    public override void JumpDown(InputAction.CallbackContext context)
    {
        xrRig.Translate(0f, 0f, 0f);
    }
}
