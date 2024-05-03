using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class TailslapController : PlayerInputController
{
    public bool spawnable = false;
    private float vibrateTime;
    public float vibrateDuration;
    private float amplitude;
    public float maxAmplitude;
    public float amplitudeDecayPeriod;
    public float frequency;
    private Vector3 initialPosition;
    private Vector3 directionOfShake;
    public Transform carouselTransform;
    private float distance;
    public float minDistance;
    public HapticController hapticLeft;
    public HapticController hapticRight;
    private bool tailSlapSuccessful = false;
    public TailSlapTutorial tutorial;
    public AudioSource audioSource;
    public AudioClip voiceover12;
    //public TMP_Text tailChargeTextNew;
    //public Image chargingSliderNew;
    //public Image chargedSliderNew;
    public TMP_Text chargedTextNew;
    
    

    protected override void Awake()
    {
        base.Awake();
        directionOfShake = transform.up;
        
    }


    protected override void Update()
    {
        base.Update();
        TailSlap(gripValue);
        distance = Vector3.Distance(transform.position, carouselTransform.position);
        //tailChargeTextNew.text = tailChargeText.text;
        chargedTextNew.text = chargedText.text;

    }

    public override void TailSlap(float value)
    {
        base.TailSlap(value);
        float lastValue = 0f;
        

        if (value < 0.05f)
        {
            if (tailCharged == true)
            {
                if (!tailSlapSuccessful)
                {
                    StartCoroutine(tutorial.TailslapTutorial02());
                    tailSlapSuccessful = true;
                }
                if(distance< minDistance)
                {
                    spawnable= true;
                }
                else
                {
                    spawnable= false;
                }
                if (spawnable)
                {
                    SpawnHerring();
                }

                //vertical jiggle
                StartCoroutine(TailslapVibration());
                //haptics
            }
            else if (lastValue - value > 0.75f)
            {
                audioSource.PlayOneShot(voiceover12);
            }
            slapCharge = 0f;
            tailCharged = false;
            chargedText.text = string.Empty;
            for (int i = 0; i < chargedSliders.Count; i++)
            {
                chargedSliders[i].enabled = false;
            }
            //tailChargeText.text = string.Empty;

        }
        lastValue = value;


    }

    private void SpawnHerring()
    {
        int count = 0;
        int spawnedHerringCount = Random.Range(CarouselManager.CM.minSpawnedHerring, CarouselManager.CM.maxSpawnedHerring);

        for (int i = 0; i < HerringSpawner.HS.herringListCarousel.Count; i++)
        {
            if (count < spawnedHerringCount)
            {
                
                if (!HerringSpawner.HS.herringListCarousel[i].activeSelf)
                {
                    Vector3 pos = transform.position + transform.forward + 1.5f * transform.up;
                    pos += new Vector3(Random.Range(-CarouselManager.CM.spawnOffsetX, CarouselManager.CM.spawnOffsetX), Random.Range(-CarouselManager.CM.spawnOffsetY, CarouselManager.CM.spawnOffsetY), Random.Range(-CarouselManager.CM.spawnOffsetZ, CarouselManager.CM.spawnOffsetZ));
                    HerringSpawner.HS.herringListCarousel[i].transform.position = pos;
                    HerringSpawner.HS.herringListCarousel[i].SetActive(true);
                    if (HerringSpawner.HS.herringListCarousel[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                    {
                        if (HerringSpawner.HS.useGravityCarousel)
                        {
                            rigidbody.useGravity = true;
                            rigidbody.isKinematic = false;
                        }

                    }
                    count++;
                }

            }
            else
            {
                break;
            }
        }

        //for (int i = 0; i < spawnedHerringCount; i++)
        //{
        //    Vector3 pos = transform.position + transform.forward + 1.5f * transform.up;
        //    pos += new Vector3(Random.Range(-CarouselManager.CM.spawnOffsetX, CarouselManager.CM.spawnOffsetX), Random.Range(-CarouselManager.CM.spawnOffsetY, CarouselManager.CM.spawnOffsetY), Random.Range(-CarouselManager.CM.spawnOffsetZ, CarouselManager.CM.spawnOffsetZ));
        //    //Instantiate(CarouselManager.CM.stunnedHerringPrefab, pos, Random.rotation);
            

        //}
    }

    private IEnumerator TailslapVibration()
    {
        initialPosition = transform.position;
        vibrateTime = 0f;
        hapticLeft.ActivateHaptic(0.5f, vibrateDuration);
        hapticRight.ActivateHaptic(0.5f, vibrateDuration);
        while (vibrateTime < vibrateDuration)
        {
            amplitude = (Mathf.Cos(Mathf.PI * vibrateTime / amplitudeDecayPeriod) + 1) / 2 * maxAmplitude;
            transform.position = initialPosition + Mathf.Sin(2 * Mathf.PI * frequency * vibrateTime) * amplitude * directionOfShake;
            vibrateTime += Time.deltaTime;

            yield return null;
        }
    }

    
}
