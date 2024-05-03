using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMoveToObjectParameters : MonoBehaviour
{
    public Transform orcaTargetCentre;
    private MoveToObject move;
    private float zOffset = 20f;
    private float xOffset = 15f;
    private float yOffset = 15f;
    private float speedMin = 1f;
    private float speedMax = 3f;
    private float rotateSpeedMin = 0.2f;
    private float rotateSpeedMax = 0.75f;
    public bool setParameters = false;

    void Awake()
    {
        move = GetComponent<MoveToObject>();
        setParameters = false;

    }

    public void SetParameters()
    {
        move.speed = Random.Range(speedMin, speedMax);
        move.rotationSpeed = Random.Range(rotateSpeedMin, rotateSpeedMax);
    }

    public void SetTransform()
    {
        move.targetTransform.position = orcaTargetCentre.position + Random.Range(-zOffset, zOffset) * orcaTargetCentre.forward + Random.Range(-xOffset, xOffset) * orcaTargetCentre.right + Random.Range(-yOffset, yOffset) * orcaTargetCentre.up;
    }

    private void Update()
    {
        if(setParameters)
        {
            if (Random.Range(1, 1000) < 3)
            {
                SetTransform();
            }

            if (Random.Range(1, 1000) < 10)
            {
                SetParameters();
            }

            move.MoveToMinimumDistance();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("net") || other.CompareTag("orca") || other.CompareTag("boat"))
        {
            move.targetTransform.position = transform.position + (transform.position - other.transform.position) * 5;
        }
    }

}
