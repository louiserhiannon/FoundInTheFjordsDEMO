using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MirrorOrcaAnimationControllerDEMO : MonoBehaviour
{
    public InputActionReference biteAction = null;
    public InputActionReference swimAction = null;
    public InputActionReference tailSlapAction = null;
    public LocomotionControllerDEMO locomotionController;

    // Start is called before the first frame update
    void Awake()
    {
        //grab animator component

        //hide tailslapcharge sliders
    }

    // Update is called once per frame
    void Update()
    {
        //Check for inputs
        //sideways locomotion (right/left arm position)
        if (LocomotionControllerDEMO.LCDemo.sidewaysSpeed > 0.05f)
        {
            //Slerp coroutine to final right value, use speed as rotate speed (y axis)
        }
        else if (LocomotionControllerDEMO.LCDemo.sidewaysSpeed < -0.05f)
        {
            //Slerp coroutine to final left value (y axis)
        }
        //up-down locomotion (head angle)
        if (LocomotionControllerDEMO.LCDemo.upDownSpeed > 0.05f)
        {
            //Slerp coroutine to final up value, use speed as rotate speed (x-axis)
        }
        else if(LocomotionControllerDEMO.LCDemo.upDownSpeed < -0.05f)
        {
            //Slerp coroutine to final up value (x-axis)
        }

        //tailslap (right grip)
        //bite (left grip)
    }
}
