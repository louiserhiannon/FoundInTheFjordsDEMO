using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class JellyFollow : MonoBehaviour
{
    public Transform targetGameObject;
    private Vector3 followDirection = new Vector3();
    [SerializeField] private float followDistance;
    private float followSpeed;
    public float lowSpeed = 0.3f;
    public float highSpeed = 0.8f;
    private float minFollowDistance = 2.5f;
    private float maxFollowDistance = 4.0f;
    [SerializeField] private float targetAngle;
    public float rotationSpeed;
    private Vector3 rotationVector = new Vector3(-1, 0, 0);
    private float rotateDirection;
    public DepthCalculator depthCalculator;
    

    // Update is called once per frame
    void Update()
    {
        FollowPosition();
        //FollowAngle();
        
        
    }

    private void FollowAngle()
    {
        //grab target angle
        //targetAngle = targetGameObject.localEulerAngles.x;
        //if(targetAngle > 90f)
        //{
        //    targetAngle -= 360f;
        //}
        //calculate follow distance
        //followDistance = Mathf.Sqrt(Mathf.Pow(followDirection.x, 2) + Mathf.Pow(followDirection.y, 2) + Mathf.Pow(followDirection.z, 2));
        //Normalize follow direction for move calculation
        //followDirection.Normalize();

        rotateDirection = Vector3.SignedAngle(transform.localPosition, targetGameObject.localPosition, rotationVector);

        if(targetAngle < transform.localEulerAngles.x)
        {
            transform.Rotate(rotationVector, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void FollowPosition()
    {
        //calculate follow direction
        followDirection = targetGameObject.position - transform.position;

        if (depthCalculator.headsetDepthCorrected > -1)
        {
           followDirection = new Vector3(followDirection.x, 0, followDirection.z);
        }
       
            
        //calculate follow distance
        followDistance = Vector3.Distance(targetGameObject.position, transform.position);
        if (depthCalculator.headsetDepthCorrected > -1)
        {
            Vector3 projectedTargetPosition = new Vector3(targetGameObject.position.x, transform.position.y, targetGameObject.position.z);
            followDistance = Vector3.Distance(projectedTargetPosition, transform.position);
        }
            //Normalize follow direction for move calculation
        followDirection.Normalize();

        if (followDistance - minFollowDistance > 0f)
        {
            if (followDistance > maxFollowDistance)
            {
                followSpeed = highSpeed;
                
            }
            else
            {
                followSpeed = lowSpeed;
            }
            transform.position += followDirection * followSpeed * Time.deltaTime;
            
        }
        else
        {
            transform.position += followDirection * 0;
        }
    }
}
