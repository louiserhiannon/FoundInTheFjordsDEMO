using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeetSnorkeler : MonoBehaviour
{
    public MovementControls moveControls;
    public Transform snorkelerTransform;
    public float speed;
    public float rotationSpeed;
    public bool interactWithSnorkeler = false;
    public ActivateIdentitySwap activateIdentitySwap;
    public AudioClip voiceover40;
    public AudioSource claraAudioSource;
    private bool actionsTriggered = false;

    void Start()
    {
        speed = 0.5f;
        rotationSpeed = 0.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interactWithSnorkeler)
        {
            if (other.CompareTag("Snorkeler"))
            {
                interactWithSnorkeler = false;
                //Debug.Log("should be interacting");
                //deactivate controls
                moveControls.DeactivateMovementControls();
                //play voiceover 40
                if(!actionsTriggered)
                {
                    actionsTriggered = true;
                    claraAudioSource.PlayOneShot(voiceover40);
                    StartCoroutine(EngageWithSnorkeler());
                }
                
                   

            }
        }
        
    }

    public IEnumerator EngageWithSnorkeler()
    {
        Vector3 targetPosition = snorkelerTransform.position - snorkelerTransform.forward * 1.5f - snorkelerTransform.up * 1.15f;
        Vector3 finalDirection = snorkelerTransform.forward;
        
        while(transform.position.z < targetPosition.z - 0.05 || transform.position.z > targetPosition.z + 0.05)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }

        //Activate Identity Swap Activation
        activateIdentitySwap.snorkelerActive = true; 
        
        while (transform.eulerAngles.y > snorkelerTransform.eulerAngles.y + 5 || transform.eulerAngles.y < snorkelerTransform.eulerAngles.y - 5)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(finalDirection), rotationSpeed * Time.deltaTime);
            yield return null;
        }

             
        yield return null;

    }
}
