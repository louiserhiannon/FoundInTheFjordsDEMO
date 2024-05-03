using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumpbackTestController : MonoBehaviour
{
    public HumpbackSwimAnimation animationController;
    public MoveToObject humpbackSwimToBaitball;
    //public MoveToObject orcaFlee;
    public Transform humpbackTurnTarget;
    public Transform humpbackEatTarget;
    public Transform humpbackFleeTarget;
    public Transform orcaFleeTarget;
    public List<Transform> orcas;
    public List<MoveToObject> orcaFlees;
    public TailSlapTutorial tailslapTutorial;
    public ParticleSystem herringScales;
    public List<GameObject> interactionSignifiers;
    public AudioSource humpbackAudioSource;
    public AudioClip humpbackOmnomnom;
    public AudioSource bgmSource;
    public AudioClip exploreMusic;
    // public AudioMixer mainMixer; Will switch to mixer fades later on
    

    // Start is called before the first frame update
    private void Awake()
    {
        humpbackSwimToBaitball.targetTransform = humpbackTurnTarget;
        
    }

    //void Start()
    //{
    //    CarouselManager.CM.SpawnCarouselOrca();
    //    StartCoroutine(GoHumpback());
    //}

    public IEnumerator GoHumpback()
    {
        //start humpback animation
        humpbackSwimToBaitball.targetTransform = humpbackTurnTarget;
        animationController.StartSwim();
        humpbackSwimToBaitball.distance = Vector3.Distance(humpbackSwimToBaitball.targetTransform.position, humpbackSwimToBaitball.transform.position);
        while (humpbackSwimToBaitball.distance > humpbackSwimToBaitball.viewDistance)
        {
            humpbackSwimToBaitball.MoveToMinimumDistance();
            yield return null;
        }

        //orca swim off
        StartCoroutine(OrcaSwimAway());
        //Debug.Log("orca swim away");

        

        //humpback continues
        while (humpbackSwimToBaitball.distance > humpbackSwimToBaitball.minDistance)
        {
            humpbackSwimToBaitball.MoveToMinimumDistance();
            yield return null;
        }

        //humpback continues to surface
        StartCoroutine(Gulp());

        humpbackSwimToBaitball.targetTransform = humpbackEatTarget;
        humpbackSwimToBaitball.distance = Vector3.Distance(humpbackSwimToBaitball.targetTransform.position, humpbackSwimToBaitball.transform.position);
        Debug.Log(humpbackSwimToBaitball.distance);

        while (humpbackSwimToBaitball.distance > humpbackSwimToBaitball.minDistance)
        {
            humpbackSwimToBaitball.MoveToMinimumDistance();
            yield return null;
        }

        //Destroy herring (deactivate)
        for (int i = FlockManager_Circular.FM.numFlockers - 1; i > -1 ; i--)
        {
            if(i%20 == 0)
            {
                
            }
            else
            {

                FlockManager_Circular.FM.allFlockers[i].SetActive(false);
                FlockManager_Circular.FM.allFlockers.RemoveAt(i);
            }
        }

        //start herring scales particle system
        herringScales.Play();

        MovementControls.MC.ActivateMovementControls();

        
        StartCoroutine(StartExplore());

        //activate jelly interaction Signifiers
        for(int  i = 1; i < interactionSignifiers.Count; i++)
        {
            interactionSignifiers[i].SetActive(true);
        }

        humpbackSwimToBaitball.targetTransform = humpbackFleeTarget;
        humpbackSwimToBaitball.distance = Vector3.Distance(humpbackSwimToBaitball.targetTransform.position, humpbackSwimToBaitball.transform.position);
        animationController.StartSwim();
        //tailslapTutorial.ActivateMovementControls();

        while (humpbackSwimToBaitball.distance > humpbackSwimToBaitball.minDistance)
        {
            humpbackSwimToBaitball.MoveToMinimumDistance();
            yield return null;
        }

        humpbackSwimToBaitball.gameObject.SetActive(false);

    }

    public IEnumerator Gulp()
    {
        yield return new WaitForSeconds(1.9f);
        animationController.StopSwim();
        yield return new WaitForSeconds(0.1f);
        animationController.StartGulp();
        humpbackAudioSource.PlayOneShot(humpbackOmnomnom); 
        // aya memo: can add modifier to volume using `, 2`
    }

    public IEnumerator OrcaSwimAway()
    {
        
        for (int i = 0; i < CarouselManager.CM.allAxes.Count; i++)
        {
            //stop axis rotation
            CarouselManager.CM.allAxes[i].GetComponent<RotateCarouselAxis>().isRotating = false;
            //remove model from parent transform
            var model = CarouselManager.CM.allAxes[i].transform.Find("Orca_Shoaling_Animated");
            model.SetParent(null, true);
            //add to new list
            orcas.Add(model);
            

        }
        

        for(int i = 0; i < orcas.Count; i++)
        {
            //stop carousel rotation
            orcas[i].GetComponent<CarouselMotion>().isCarouselFeeding = false;
        }



        for (int i = 0; i < orcas.Count; i++)
        {
            var flee = orcas[i].GetComponent<MoveToObject>();
            orcaFlees.Add(flee);

            //set target transform
            orcaFlees[i].targetTransform = orcaFleeTarget;

            //calculate distance
            orcaFlees[i].distance = Vector3.Distance(orcaFlees[i].targetTransform.position, orcaFlees[i].transform.position);
        }


        while (orcaFlees[0].distance > orcaFlees[0].minDistance)
        {
            for (int i = 0; i < orcaFlees.Count; i++)
            {
                orcaFlees[i].MoveToMinimumDistance();
            }
            yield return null;
        }

        for (int i = 0; i < orcaFlees.Count; i++)
        {
            orcaFlees[i].gameObject.SetActive(false);
        }


    }

    private IEnumerator StartExplore()
    {
        tailslapTutorial.momAudioSource.PlayOneShot(tailslapTutorial.voiceover17a);
        yield return new WaitForSeconds(tailslapTutorial.voiceover17a.length);
        tailslapTutorial.momAudioSource.PlayOneShot(tailslapTutorial.voiceover17b);
        yield return new WaitForSeconds(tailslapTutorial.voiceover17b.length);
        //activate mom interaction signifier
        interactionSignifiers[0].SetActive(true);
        bgmSource.clip = exploreMusic;
        bgmSource.loop = true;
        bgmSource.Play();
        StartCoroutine(FadeAudioSource.StartFade(bgmSource, 5f, 1f));
    }
}
