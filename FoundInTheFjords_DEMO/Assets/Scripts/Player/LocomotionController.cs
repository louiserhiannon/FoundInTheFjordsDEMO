using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocomotionController : MonoBehaviour
{
    public InputActionReference moveSpeedReference = null;
    public InputActionReference upJumpReference = null;
    public InputActionReference downJumpReference = null;
    public InputActionReference snapTurn = null;
    protected Transform xrRig;
    protected Camera mainCamera;
    [SerializeField] protected Vector3 cameraForward = new Vector3();
    [SerializeField] protected Vector3 cameraRight = new Vector3();
    protected Vector3 netCameraVector = new Vector3();
    protected Vector3 sidewaysCameraVector = new Vector3();
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float sidewaysSpeed;
    protected float maxSpeed;
    protected float maxTranslateSpeed;
    protected float spinAngleMax;
    [SerializeField] protected float currentSpeed;
    protected float acceleration;
    [SerializeField] protected float desiredAngle;
    [SerializeField] protected float rotationToDesiredAngle;
    [SerializeField] protected float updatedAngle;
    [SerializeField] protected float currentAngle;
    protected float smoothingFactor = 10f;
    protected float jumpAmount;
    [SerializeField] protected float timeAtZero = 0f;
    
    // Start is called before the first frame update
    void Awake()
    {
        xrRig = GetComponent<Transform>(); 
        mainCamera = GetComponentInChildren<Camera>();
        currentSpeed = 0f;
        currentAngle= 0f;
        acceleration = 0.05f;
        maxTranslateSpeed = 0.5f;
        maxSpeed = 2f;
        jumpAmount = 0.5f;

    }

    void OnEnable()
    {
        upJumpReference.action.started += JumpUp;
        downJumpReference.action.started += JumpDown;
    }

    void OnDisable()
    {
        upJumpReference.action.started -= JumpUp;
        downJumpReference.action.started -= JumpDown;
    }

    void Update()
    {
        Vector2 thumbstickPosition = moveSpeedReference.action.ReadValue<Vector2>();
        float yvalue = thumbstickPosition.y;
        float xvalue = thumbstickPosition.x;        
        MoveForwardRelativeToCamera(yvalue);
        MoveSidewaysRelativeToCamera(xvalue);

        Vector2 snapPosition = snapTurn.action.ReadValue<Vector2>();
        float snapValue = snapPosition.x;
        Snap(snapValue);



    }


    public virtual void MoveForwardRelativeToCamera(float relativeForwardSpeed)
    {
        throw new NotImplementedException();
    }

    public virtual void MoveSidewaysRelativeToCamera(float relativeSidewaysSpeed)
    {

        throw new NotImplementedException();

    }

    public virtual void JumpUp(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public virtual void JumpDown(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public virtual void Snap(float value)
    {
        
    }

    
}
