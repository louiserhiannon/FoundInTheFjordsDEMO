using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class EatingController : PlayerInputController
{
    public static EatingController EC;
    public Eatable thisEatableHerring;
    public List<Eatable> eatableHerrings;
    public int eatenHerringCount = 0;
    public int targetHerringCount = 4;
    [Range(1f, 60f)]
    public float herringLifetime;
    private AudioSource audioSource;
    public List<AudioClip> successClips;
    public List<AudioClip> failureClips;
    public bool targetActive = false;
    public TailSlapTutorial tutorial;
    public ExploreFishingBoatArea explore;
    public TooClose tooClose;
    public HerringCounter herringCounter;
    public bool eat_simple = false;
    public bool eat_tailslap = false;
    public bool eat_fishing = false;
    public Transform herringStorageArea;
    

    //public Eatable eatenHerring;

    protected override void Awake()
    {
        base.Awake();
        if (EC != null && EC != this)
        {
            Destroy(this);
        }
        else
        {
            EC = this;
        }
        
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(eatenHerringCount < targetHerringCount)
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

            if (other.CompareTag("net"))
            {
                tooClose.OnHoverStart();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("stunnedHerring"))
        {
            var eatable = other.GetComponent<Eatable>();
            if(eatableHerrings.Count > 0)
            {
                if (eatable == eatableHerrings[0])
                {
                    eatableHerrings[0].OnHoverEnd();
                    eatableHerrings.Clear();

                }
            }
        }

        if (other.CompareTag("net"))
        {
            tooClose.OnHoverEnd();
        }
    }

    public override void Bite(InputAction.CallbackContext context)
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
                eatableHerrings[0].transform.position = HerringSpawner.HS.transform.position;
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
    }

    public void PlayFailureSounds()
    {
        int index = UnityEngine.Random.Range(0, failureClips.Count);
        var clip = failureClips[index];
        audioSource.PlayOneShot(clip);
    }

    public void PlaySuccessSounds()
    {
        int index = UnityEngine.Random.Range(0, successClips.Count);
        var clip = successClips[index];
        audioSource.PlayOneShot(clip);
    }

    protected override void Update()
    {
        if (targetActive)
        {
            if (eatenHerringCount == targetHerringCount)
            {
                Debug.Log("target reached");
                targetActive = false;
                herringCounter.displayActive = false;
                if (eat_simple)
                {
                    StartCoroutine(tutorial.TailslapTutorial01());
                    eat_simple = false;
                }

                if (eat_tailslap)
                {
                    StartCoroutine(tutorial.TailslapTutorial03());
                    eat_tailslap = false;
                }

                if (eat_fishing)
                {
                    StartCoroutine(explore.ExploreSurroundings01());
                    eat_fishing = false;
                }
            }
        }
        
       
    }
    
        
    
}
