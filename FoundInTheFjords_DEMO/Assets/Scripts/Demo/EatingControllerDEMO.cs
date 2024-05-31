using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class EatingControllerDEMO : MonoBehaviour
{
    public InputActionReference biteAction = null;
    public GameObject mirrorOrca;
    private Animator mirrorOrcaAnimator;
    public static EatingControllerDEMO ECDemo;
    public Eatable thisEatableHerring;
    public List<Eatable> eatableHerrings;
    public int eatenHerringCount = 0;
    public int targetHerringCount = 3;
    [Range(1f, 60f)]
    public float herringLifetime;
    public AudioSource momAudioSource;
    public List<AudioClip> successClips;
    public List<AudioClip> failureClips;
    public bool targetActive = false;
    //public TailSlapTutorial tutorial;
    //public ExploreFishingBoatArea explore;
    //public TooClose tooClose;
    public HerringCounter herringCounter;
    //public bool eat_simple = false;
    //public bool eat_tailslap = false;
    //public bool eat_fishing = false;
    public Transform herringStorageAreaCarousel;
    public Transform herringStorageAreaFishing;
    //public DEMOCoroutine3 demoCoroutine03;
    private bool isOpen = false;
    public TMP_Text biteStatus;
    private Scene scene;

    private void Awake()
    {
        //destroy any duplicate instances of the script
        if (ECDemo != null && ECDemo != this)
        {
            Destroy(this);
        }
        if (ECDemo == null)
        {
            ECDemo = this;
        }

        if (mirrorOrca != null)
        {
            mirrorOrcaAnimator = mirrorOrca.GetComponent<Animator>();
        }
        if (biteStatus != null)
        {
            biteStatus.text = "Mouth Open";
        }
        
    }


    protected void OnEnable()
    {
        scene = SceneManager.GetActiveScene();
        //biteAction.action.started += Bite;
        biteAction.action.started += OpenMouth;
        biteAction.action.canceled += CloseMouth;
       
    }

    protected void OnDisable()
    {
        //biteAction.action.started -= Bite;
        biteAction.action.started -= OpenMouth;
        biteAction.action.canceled -= CloseMouth;
    }

    private void OnTriggerEnter(Collider other)
    {
        //add if statement referencing mouthopen bool
        if (isOpen)
        {
            if (eatenHerringCount < targetHerringCount)
            {
                if (other.CompareTag("stunnedHerring"))
                {
                    var eatable = other.GetComponent<Eatable>();
                    if (eatable != null)
                    {
                        thisEatableHerring = eatable;
                        eatableHerrings.Add(thisEatableHerring);
                        eatableHerrings[0].OnHoverStart();
                    }
                }

            }
        }
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("stunnedHerring"))
        {
            var eatable = other.GetComponent<Eatable>();
            if (eatableHerrings.Count > 0)
            {
                if (eatable == eatableHerrings[0])
                {
                    eatableHerrings[0].OnHoverEnd();
                    eatableHerrings.Clear();

                }
            }
        }

        
    }

    private void Update()
    {
        if (targetActive)
        {
            if (eatenHerringCount == targetHerringCount)
            {
                Debug.Log("target reached");
                targetActive = false;
                herringCounter.displayActive = false;
                StartCoroutine(DEMOCoroutine3.coroutine03.DEMOCoroutine03());
            }
        }
    }

    //public void Bite(InputAction.CallbackContext context)
    //{
    //    if (eatenHerringCount < targetHerringCount)
    //    {
    //        if (eatableHerrings.Count == 0)
    //        {
    //            //play other sounds
    //            PlayFailureSounds();
    //        }

    //        else if (eatableHerrings[0] != null)
    //        {
    //            //Destroy Active Herring and remove from list
    //            eatableHerrings[0].gameObject.SetActive(false);
    //            eatableHerrings[0].GetComponent<Eatable>().OnHoverEnd();
    //            eatableHerrings[0].transform.position = HerringSpawner.HS.transform.position; //Move HerringSpawnerScript into DEMO folder
    //            if (eatableHerrings[0].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
    //            {
    //                rigidbody.useGravity = false;
    //                rigidbody.isKinematic = true;
    //            }
    //            eatableHerrings.Clear();
    //            //increase count by 1
    //            eatenHerringCount++;
    //            //play success audio
    //            PlaySuccessSounds();
    //        }

    //        else
    //        {
    //            eatableHerrings.Clear();
    //            PlayFailureSounds();
    //        }
    //    }
    //}

    public void OpenMouth(InputAction.CallbackContext context)
    {
        //if scene "root menu", then trigger animation
        if(scene.name == "RootMenu")
        {
            //trigger mouth open animation
            if (!mirrorOrcaAnimator.GetBool("mouthOpen"))
            {
                mirrorOrcaAnimator.SetBool("mouthOpen", true);
            }
        }
        //if scene "demo" 
        if(scene.name == "DEMO")
        {
            //set bool to identify eatable fish to true;
            isOpen = true;
            biteStatus.text = "Mouth Open";
        }
    }

    public void CloseMouth(InputAction.CallbackContext context)
    {
        if (scene.name == "RootMenu")
        {
            //trigger mouth closed animation
            if (mirrorOrcaAnimator.GetBool("mouthOpen"))
            {
                mirrorOrcaAnimator.SetBool("mouthOpen", false);
            }
        }
        //if scene demo,

        if (scene.name == "DEMO")
        {
            if (eatenHerringCount < targetHerringCount)
            {
                if (eatableHerrings.Count == 0)
                {
                    //play other sounds
                    PlayFailureSounds();
                }

                else if (eatableHerrings[0] != null)
                {
                    //Destroy Active Herring and remove from list
                    eatableHerrings[0].gameObject.SetActive(false);
                    eatableHerrings[0].GetComponent<Eatable>().OnHoverEnd();
                    eatableHerrings[0].transform.position = HerringSpawner.HS.transform.position; //Move HerringSpawnerScript into DEMO folder
                    if (eatableHerrings[0].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                    {
                        rigidbody.useGravity = false;
                        rigidbody.isKinematic = true;
                    }
                    eatableHerrings.Clear();
                    //increase count by 1
                    eatenHerringCount++;
                    //play success audio
                    PlaySuccessSounds();
                }

                else
                {
                    eatableHerrings.Clear();
                    PlayFailureSounds();
                }
            }
            //then set bool to identify eatable fish to false;
            isOpen = false;
            biteStatus.text = "Mouth Closed";
        }
            
    }

    public void PlayFailureSounds()
    {
        int index = UnityEngine.Random.Range(0, failureClips.Count);
        var clip = failureClips[index];
        momAudioSource.PlayOneShot(clip);
    }

    public void PlaySuccessSounds()
    {
        int index = UnityEngine.Random.Range(0, successClips.Count);
        var clip = successClips[index];
        momAudioSource.PlayOneShot(clip);
    }
}
