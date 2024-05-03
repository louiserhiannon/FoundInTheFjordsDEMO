using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObject : MonoBehaviour
{
    public float distance;
    public float viewDistance = 30f;
    public float minDistance = 12f;
    public float speed = 0.5f;
    public float rotationSpeed = 2f;
    public Transform targetTransform;


    private void Awake()
    {
        if(targetTransform == null)
        {
            targetTransform = transform;
        }
    }

    private void Start()
    {
        distance = Vector3.Distance(targetTransform.position, transform.position);
    }
    public void CalculateDistance()
    {
        distance = Vector3.Distance(targetTransform.position, transform.position);
    }
    public void MoveToMinimumDistance()
    {
        if(targetTransform != null)
        {
            Vector3 direction = targetTransform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

            this.transform.Translate(0, 0, speed * Time.deltaTime);

            distance = Vector3.Distance(targetTransform.position, transform.position);
        }
        
    }

    public void RotateToAlign()
    {
        if(targetTransform != null)
        {
            Vector3 direction = targetTransform.forward;
            //Debug.Log(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            //  Debug.Log(transform.rotation);
            


        }
        
    }

    public void RotateToFace()
    {
        if (targetTransform != null)
        {
            Vector3 direction = - targetTransform.forward;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }

    }

    public void RotateToFaceObject()
    {
        if (targetTransform != null)
        {
            Vector3 direction = targetTransform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }
    }

    public void TranslateToMinimumDistance()
    {
        if (targetTransform != null)
        {
            Vector3 direction = (targetTransform.position - transform.position).normalized;
            this.transform.Translate(speed * Time.deltaTime * direction);

            distance = Vector3.Distance(targetTransform.position, transform.position);
        }
    }
}
