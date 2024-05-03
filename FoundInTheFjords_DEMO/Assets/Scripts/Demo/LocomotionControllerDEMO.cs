using DG.Tweening;
using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class LocomotionControllerDEMO : MonoBehaviour
{
    public static LocomotionControllerDEMO LCDemo;
    public GameObject mirrorOrca;
    private Animator mirrorOrcaAnimator;
    public InputActionReference moveSpeedReference = null;
    public Transform leftController;
    public Transform rightController;
    protected Transform xrRig;
    public Transform xRRigMovementBox;
    public float upDownSpeed;
    public float sidewaysSpeed;
    //[SerializeField] protected Vector3 rigUp = new Vector3();
    //[SerializeField] protected Vector3 rigRight = new Vector3();
    //[SerializeField] private float cameraAngle;
    //[SerializeField] private float cameraAngleRad;
    //[SerializeField] private float cameraSin;
    [Range(0f, 1f)]
    public float maxSpeed;
    protected float acceleration;
    public float limitDepth;
    public float limitHeight;
    public float limitRight;
    public float limitLeft;
    [SerializeField] private bool isSurfable = false;
    private float surfableDepth = 6.5f;
    public CanvasGroup surfsUpPanel;
    private int counter = 0;
    public float minControllerOffset;
    [SerializeField] private float rightControllerPositionX;
    [SerializeField] private float leftControllerPositionX;
    private Scene scene;
    //[SerializeField] private bool rotateBackX;
    //[SerializeField] private bool rotateBackY;
    //[SerializeField] private bool isRotatingUp = false;
    //[SerializeField] private bool isRotatingSideways = false;
    //public float rotateSpeed;

    //private Vector3 cameraDirection;


    void Awake()
    {
        if (LCDemo != null && LCDemo != this)
        {
            Destroy(this);
        }
        if (LCDemo == null)
        {
            LCDemo = this;
        }
        xrRig = GetComponent<Transform>();
        surfableDepth = 6.9f;
        if(mirrorOrca != null)
        {
            mirrorOrcaAnimator = mirrorOrca.GetComponentInChildren<Animator>();
        }
        

    }

    private void OnEnable()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 thumbstickPosition = moveSpeedReference.action.ReadValue<Vector2>();
        float gripValue = moveSpeedReference.action.ReadValue<float>();
        rightControllerPositionX = rightController.localPosition.x;
        leftControllerPositionX = leftController.localPosition.x;
        float cameraY = Camera.main.transform.forward.y;
        //cameraAngle = Camera.main.transform.eulerAngles.x;
        //cameraAngleRad = Mathf.Deg2Rad * cameraAngle;
        //cameraSin = Mathf.Sin(cameraAngleRad);
        MoveUp(gripValue, cameraY);
        MoveRight(gripValue, rightControllerPositionX, leftControllerPositionX);
       
        if (scene.name == "RootMenu")
        {
            if(gripValue > 0.05f)
            {
                //trigger animation
                if (!mirrorOrcaAnimator.GetBool("isSwimming"))
                {
                    mirrorOrcaAnimator.SetBool("isSwimming", true);
                    
                }
                //if (cameraY > 0.1f || cameraY < -0.1f)
                //{
                //    //Debug.Log(cameraY);
                //    if (!isRotatingUp)
                //    {
                //        StartCoroutine(RotateUp(cameraY));
                //        Debug.Log("coroutine initiated");
                //        isRotatingUp = true;
                //    }


                //}
                
                //if (rightControllerPositionX > minControllerOffset || leftControllerPositionX < -minControllerOffset)
                //{
                //    if (!isRotatingSideways)
                //    {
                //        StartCoroutine(RotateSide(gripValue, rightControllerPositionX, leftControllerPositionX));
                //        isRotatingSideways = true;
                //    }

                //}
                //else if (rotateBackY)
                //{
                //    StartCoroutine(ResetYRotation());
                //}

            }
            //else if (rotateBackX)
            //{
            //    StartCoroutine(ResetXRotation());

            //}

            else if (mirrorOrcaAnimator.GetBool("isSwimming"))
            {
                mirrorOrcaAnimator.SetBool("isSwimming", false);
            }



        }
    }

    

    public virtual void MoveUp(float relativeUpSpeed, float yDirection)
    {
        upDownSpeed = 3f * relativeUpSpeed * maxSpeed * yDirection;   
       
        if (xrRig.localPosition.y >= limitDepth && xrRig.localPosition.y <= limitHeight)
        {
            xrRig.Translate(Time.deltaTime * upDownSpeed * xrRig.up, Space.Self);

        }
        else if(xrRig.localPosition.y < limitDepth && upDownSpeed > 0)
        { 
            xrRig.Translate(Time.deltaTime * upDownSpeed * xrRig.up, Space.Self);
        }
        else if(xrRig.localPosition.y > limitHeight && upDownSpeed < 0)
        {
            xrRig.Translate(Time.deltaTime * upDownSpeed * xrRig.up, Space.Self);
        }

        if(surfsUpPanel != null)
        {
            if (xrRig.localPosition.y >= surfableDepth)
            {
                if (!isSurfable)
                {
                    isSurfable = true;
                    if (counter < 2)
                    {
                        StartCoroutine(SurfsUp());
                    }

                }
            }
            else
            {
                if (isSurfable)
                {
                    isSurfable = false;
                }
            }
        }

        
    }

    public virtual void MoveRight(float relativeSidewaysSpeed, float rightPosition, float leftPosition)
    {
        sidewaysSpeed = relativeSidewaysSpeed * maxSpeed;

        if (rightPosition > minControllerOffset || leftPosition < -minControllerOffset)
        {
            if (xrRig.localPosition.x >= limitLeft && xrRig.localPosition.x <= limitRight)
            {
                if (rightPosition > -leftPosition)
                {
                    xrRig.Translate(Time.deltaTime * sidewaysSpeed, 0, 0);

                }
                else
                {
                    xrRig.Translate(- Time.deltaTime * sidewaysSpeed, 0, 0);
                }
            }

        }

        else if (xrRig.localPosition.x < limitLeft && rightPosition > -leftPosition)
        {
            xrRig.Translate(Time.deltaTime * sidewaysSpeed, 0, 0);
        }
        else if (xrRig.localPosition.x > limitRight && leftPosition < -rightPosition)
        {
            xrRig.Translate(-Time.deltaTime * sidewaysSpeed, 0, 0);
        }

    }

    private IEnumerator SurfsUp()
    {
        counter++;
        //yield return new WaitForSeconds(0.5f);
        surfsUpPanel.DOFade(1, 0.25f);
        yield return new WaitForSeconds(5);
        surfsUpPanel.DOFade(0, 1);
        
    }

    //private IEnumerator ResetYRotation()
    //{
    //    float finalY = 0;
    //    Vector3 finalRotation;
    //    finalRotation = new Vector3(mirrorOrca.transform.localEulerAngles.x, finalY, mirrorOrca.transform.localEulerAngles.z);


    //    while (mirrorOrca.transform.localEulerAngles.y < finalY - 2 || mirrorOrca.transform.localEulerAngles.y > finalY + 2)
    //    {
    //        mirrorOrca.transform.localRotation = Quaternion.Slerp(mirrorOrca.transform.rotation, Quaternion.Euler(finalRotation), 0.1f * Time.deltaTime);
    //        yield return null;
    //    }

    //    rotateBackY = false;

    //}

    //private IEnumerator RotateSide(float gripValue, float rightControllerPositionX, float leftControllerPositionX)
    //{
    //    float finalY;
    //    Vector3 finalRotation;
    //    if (rightControllerPositionX > -leftControllerPositionX)
    //    {
    //        finalY = 45;
    //    }
    //    else
    //    {
    //        finalY = -45;
    //    }
    //    finalRotation = new Vector3(mirrorOrca.transform.localEulerAngles.x, finalY, mirrorOrca.transform.localEulerAngles.z);

    //    if (finalY > 0)
    //    {
    //        while (mirrorOrca.transform.localEulerAngles.y < finalY)
    //        {
    //            mirrorOrca.transform.localRotation = Quaternion.Slerp(mirrorOrca.transform.rotation, Quaternion.Euler(finalRotation), gripValue * 0.1f * Time.deltaTime);
    //            yield return null;
    //        }
    //    }
    //    else
    //    {
    //        while (mirrorOrca.transform.localEulerAngles.y > finalY)
    //        {
    //            mirrorOrca.transform.localRotation = Quaternion.Slerp(mirrorOrca.transform.rotation, Quaternion.Euler(finalRotation), gripValue * Time.deltaTime);
    //            yield return null;
    //        }
    //    }

    //    rotateBackY = true;



    //}

    //private IEnumerator ResetXRotation()
    //{

    //    Debug.Log("Rotation Resetting");
    //    //float finalX = 0;
    //    float currentX = mirrorOrca.transform.localEulerAngles.x;
    //    //Vector3 finalRotation;
    //    //finalRotation = new Vector3(finalX, mirrorOrca.transform.localEulerAngles.y, mirrorOrca.transform.localEulerAngles.z);
    //    if (mirrorOrca.transform.localEulerAngles.x < 0)
    //    {
    //        while (mirrorOrca.transform.localEulerAngles.x < 0)
    //        {
    //            currentX += rotateSpeed * Time.deltaTime;
    //            mirrorOrca.transform.localEulerAngles = new Vector3(currentX, mirrorOrca.transform.localEulerAngles.y, mirrorOrca.transform.localEulerAngles.z);
    //            yield return null;
    //        }
    //    }
    //    else if (mirrorOrca.transform.localEulerAngles.x > 0)
    //    {
    //        while (mirrorOrca.transform.localEulerAngles.x > 0)
    //        {
    //            currentX -= rotateSpeed * Time.deltaTime;
    //            mirrorOrca.transform.localEulerAngles = new Vector3(currentX, mirrorOrca.transform.localEulerAngles.y, mirrorOrca.transform.localEulerAngles.z);
    //            yield return null;
    //        }
    //    }




    //    rotateBackX = false;

    //}

    //private IEnumerator RotateUp(float cameraY)
    //{
    //    Debug.Log("SettingRotation");
    //    float finalX;
    //    float currentX = mirrorOrca.transform.localEulerAngles.x;
    //    //Vector3 finalRotation;
    //    if (cameraY > 0)
    //    {
    //        finalX = currentX - 45;
    //    }
    //    else
    //    {
    //        finalX = currentX + 45;
    //    }

    //    //finalRotation = new Vector3(finalX, mirrorOrca.transform.localEulerAngles.y,  mirrorOrca.transform.localEulerAngles.z);

    //    if (finalX > currentX)
    //    {
    //        while (currentX < finalX)
    //        {
    //            currentX += rotateSpeed * Time.deltaTime;
    //            mirrorOrca.transform.localEulerAngles = new Vector3(currentX, mirrorOrca.transform.localEulerAngles.y, mirrorOrca.transform.localEulerAngles.z);
    //            yield return null;
    //        }
    //    }
    //    else
    //    {
    //        while (currentX > finalX)
    //        {
    //            currentX -= rotateSpeed * Time.deltaTime;
    //            mirrorOrca.transform.localEulerAngles = new Vector3(currentX, mirrorOrca.transform.localEulerAngles.y, mirrorOrca.transform.localEulerAngles.z);
    //            yield return null;
    //        }
    //    }
    //    rotateBackX = true;
    //    isRotatingUp = false;

    //}



}
