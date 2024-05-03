using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGrab : MonoBehaviour
{
    public string controllerTag;
    public bool isGrabbed = false;
    public Transform controllerModel;
    public Transform ladderParent;
    public Transform reverseParent;
    public GameObject sphere;
    public GameObject dummyController;
    
    private bool climbTriggered = false;
    public float moveSpeed = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(controllerTag))
        {
            if (!climbTriggered)
            {
                
                //parent controller to parent
                controllerModel.SetParent(null);
                controllerModel.SetParent(ladderParent);
                //deactivate sphere and dummy controller
                sphere.SetActive(false);
                dummyController.SetActive(false);
                isGrabbed = true;
                climbTriggered = true;
                StartCoroutine(MoveHandToLadder());
            }
        }
    }

    public IEnumerator MoveHandToLadder()
    {
        

        Vector3 targetPosition = reverseParent.position;
        Vector3 finalDirection = reverseParent.forward;

        while (controllerModel.transform.position.x < targetPosition.x - 0.05 || controllerModel.transform.position.x > targetPosition.x + 0.05)
        {
            controllerModel.transform.position = Vector3.MoveTowards(controllerModel.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        while (controllerModel.transform.eulerAngles.y > reverseParent.eulerAngles.y + 5 || controllerModel.transform.eulerAngles.y < reverseParent.eulerAngles.y - 5)
        {
            controllerModel.transform.rotation = Quaternion.Slerp(controllerModel.transform.rotation, Quaternion.LookRotation(finalDirection), moveSpeed * Time.deltaTime);
            yield return null;
        }

    }
    
}
