using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivateIdentitySwap : MonoBehaviour
{
    public Transform activeSnorkeler;
    public AudioSource claraAudioSource;//added for demo
    public AudioClip voiceover16; //added for demo
    private Camera head;
    public int count = 0;
    private int maxCount = 6;
    public bool snorkelerActive = false;
    [SerializeField] private bool counterActive = true;
    //public TMP_Text counter;
    public DEMOCoroutine4 coroutine04; //need to update for demo to call new IdentitySwapDEMO script
    
    


    // Start is called before the first frame update
    void Start()
    {
        head = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (snorkelerActive)
        {
            activeSnorkeler.eulerAngles = new Vector3(activeSnorkeler.eulerAngles.x, activeSnorkeler.eulerAngles.y, - head.transform.eulerAngles.z);
            if(count < maxCount)
            {
                if (counterActive)
                {
                    if ((head.transform.localEulerAngles.z > 25 && head.transform.localEulerAngles.z < 90) || (head.transform.localEulerAngles.z > 270 && head.transform.localEulerAngles.z < 335))
                    {
                        count++;
                        counterActive = false;
                        //if statement added for demo
                        if(count == 2)
                        {
                            claraAudioSource.PlayOneShot(voiceover16);
                        }
                    }
                }
                else if (head.transform.localEulerAngles.z < 2 || head.transform.localEulerAngles.z > 358)
                {
                    counterActive = true;
                }

            }

            else
            {
                
                StartCoroutine(coroutine04.SwitchBodies());
                snorkelerActive = false;

            }

            //counter.text = count.ToString();
            
        }
    }
}