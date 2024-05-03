using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocomotionController_General : LocomotionController
{
    public float snapDelay;
    public float turnAmount;
    private bool isTurning = false;
    //public BoundingNetController net;
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



        cameraForward = mainCamera.transform.forward;

        //if (!net.canMove && moveSpeed > 0)
        //{
        //    Debug.Log(net.canMove);
        //    currentSpeed = 0;
        //}
        
        netCameraVector = currentSpeed * cameraForward;
        
             
        xrRig.transform.Translate(netCameraVector * Time.deltaTime, Space.World);

        
    }

    public override void MoveSidewaysRelativeToCamera(float relativeSidewaysSpeed)
    {

        //Rotate along local z axis
        spinAngleMax = 35f;
        //Add smoothing effect

        desiredAngle = -relativeSidewaysSpeed * spinAngleMax; //desired angle is between -25 (right) and +25 (left)
        rotationToDesiredAngle = (desiredAngle - currentAngle) / smoothingFactor;
        updatedAngle = currentAngle + rotationToDesiredAngle;


        xrRig.transform.localEulerAngles = new Vector3(xrRig.transform.localEulerAngles.x, xrRig.transform.localEulerAngles.y, updatedAngle);

        currentAngle = updatedAngle;

        //translate sideways
        sidewaysSpeed = relativeSidewaysSpeed * maxTranslateSpeed;
        cameraRight = mainCamera.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();
        sidewaysCameraVector = sidewaysSpeed * cameraRight;

        //if (net.canMove)
        //{
            xrRig.transform.Translate(sidewaysCameraVector * Time.deltaTime, Space.World);
        //}
        

    }

    public override void JumpUp(InputAction.CallbackContext context)
    {
        //if (net.canMove)
        //{
            xrRig.Translate(0f, jumpAmount, 0f);
        //}
            
    }

    public override void JumpDown(InputAction.CallbackContext context)
    {
        //if(net.canMove)
        //{
            xrRig.Translate(0f, -jumpAmount, 0f);
        //}
        
    }

    public override void Snap(float value)
    {
        //Debug.Log(value);
        
        //turn left
        if (value < -0.90f && !isTurning)
            {
                StartCoroutine(DelaySnapLeft());
                isTurning = true;
            }
        //turn right
        else if (value > 0.90f && !isTurning)
            {
                StartCoroutine(DelaySnapRight());
                isTurning = true;
            }
                
        //reset bool
        if (value > -0.1 && value < 0.1)
        {
            isTurning = false;
        }

    }


    private IEnumerator DelaySnapLeft()
    {
        yield return new WaitForSeconds(snapDelay);
        transform.Rotate(0, -turnAmount, 0);
        
    }

    private IEnumerator DelaySnapRight()
    {
        yield return new WaitForSeconds(snapDelay);
        transform.Rotate(0, turnAmount, 0);
        
    }


}
