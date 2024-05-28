using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using DG.Tweening;

public class LadderClimb : MonoBehaviour
{
    public LadderGrab lefthand;
    public LadderGrab righthand;
    public bool coroutineRunning = false;
    public Transform lefthandGrabPoint;
    public Transform righthandGrabPoint;
    public Transform xRRig;
    public Transform ladder;
    public Transform reverseParent;
    public float climbSpeed;
    public bool isFlashingLeft = true;
    public bool isFlashingRight = true;
    public CanvasGroup grabLadder;
    //public MovementControls moveControls;
    

    // Update is called once per frame
    void Update()
    {
        if (lefthand.isGrabbed && isFlashingLeft)
        {
            isFlashingLeft = false;
        }

        if (righthand.isGrabbed && isFlashingRight)
        {
            isFlashingRight = false;
        }

        if (lefthand.isGrabbed && righthand.isGrabbed)
        {
            if (!coroutineRunning)
            {
                Debug.Log("should be climbing ladder");
                StartCoroutine(ClimbLadder());
                coroutineRunning = true;
            }
        }
    }

    public IEnumerator ClimbLadder()
    {
        //deactivate moveControls
        //moveControls.DeactivateMovementControls();

        //fade panel
        grabLadder.DOFade(0, 1);

        Vector3 targetPosition = reverseParent.position;
        Vector3 finalDirection = reverseParent.forward;

        while (xRRig.transform.position.y < targetPosition.y - 0.05 || xRRig.transform.position.y > targetPosition.y + 0.1)
        {
            xRRig.transform.position = Vector3.MoveTowards(xRRig.transform.position, targetPosition, climbSpeed * Time.deltaTime); 
            yield return null;
        }
        Debug.Log("rig y position is " + xRRig.transform.position.y);

        while (xRRig.transform.eulerAngles.y > reverseParent.eulerAngles.y + 5 || xRRig.transform.eulerAngles.y < reverseParent.eulerAngles.y - 5)
        {
            xRRig.transform.rotation = Quaternion.Slerp(xRRig.transform.rotation, Quaternion.LookRotation(finalDirection), climbSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (xRRig.position.y < 2.7f)
        {
            xRRig.Translate(climbSpeed * Time.deltaTime * Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        while (righthandGrabPoint.localPosition.y < 0.30f)
        {
            righthandGrabPoint.Translate(climbSpeed * Time.deltaTime * Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (xRRig.position.y < 2.8f)
        {
            xRRig.Translate(climbSpeed * Time.deltaTime * Vector3.up);
            yield return null;
        }
        

        yield return new WaitForSeconds(1f);

        while (lefthandGrabPoint.localPosition.y < 0.40f)
        {
            lefthandGrabPoint.Translate(climbSpeed * Time.deltaTime * Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (xRRig.position.y < 2.90f)
        {
            xRRig.Translate(climbSpeed * Time.deltaTime * Vector3.up);
            yield return null;
        }
        

        while (righthandGrabPoint.localPosition.y < 0.50f)
        {
            righthandGrabPoint.Translate(climbSpeed * Time.deltaTime * Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (xRRig.position.y < 3.0f)
        {
            xRRig.Translate(climbSpeed * Time.deltaTime * Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (lefthandGrabPoint.localPosition.y < 0.60f)
        {
            lefthandGrabPoint.Translate(climbSpeed * Time.deltaTime * Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (xRRig.position.y < 3.1f)
        {
            xRRig.Translate(climbSpeed * Time.deltaTime * Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(5f);
        Debug.Log("total scene play time is " + Time.time);
        ChangeScene.instance.SceneSwitch("Scene05-360Zodiac");


    }
}
