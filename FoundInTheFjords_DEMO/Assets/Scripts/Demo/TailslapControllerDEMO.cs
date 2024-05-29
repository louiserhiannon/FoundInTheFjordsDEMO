using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SceneManagement;

public class TailslapControllerDEMO : MonoBehaviour
{
    public static TailslapControllerDEMO TCDemo;
    public GameObject mirrorOrca;
    private Animator mirrorOrcaAnimator;
    public InputActionReference tailSlapAction = null;
    public bool tailChargeable = false;
    protected float slapCharge = 0f;
    protected float tailChargeRequired = 100f;
    public bool tailCharged = false;
    public float tailChargeDisplayed;
    public TMP_Text chargedText;
    [SerializeField] protected float gripValue;
    public List<Image> chargingSliders;
    public List<Image> chargedSliders;
    public CanvasGroup tailSlapPanel;
    protected float sliderPosition;
    private float vibrateTime;
    public float vibrateDuration;
    private float amplitude;
    public float maxAmplitude;
    public float amplitudeDecayPeriod;
    public float frequency;
    private Vector3 initialPosition;
    private Vector3 directionOfShake;
    public HapticController hapticLeft;
    public HapticController hapticRight;
    public GameObject stunnedHerring;
    //public DEMOCoroutine2 demoCoroutine2;
    private Scene scene;


    private void Awake()
    {
        //destroy any duplicate instances of the script
        if (TCDemo != null && TCDemo != this)
        {
            Destroy(this);
        }
        if (TCDemo == null)
        {
            TCDemo = this;
        }

        if (chargedText != null)
        {
            chargedText.text = "Ready";
        }
        
        directionOfShake = transform.up;
        
        if(mirrorOrca != null)
        {
            mirrorOrcaAnimator = mirrorOrca.GetComponent<Animator>();
        }

       
    }

    private void OnEnable()
    {
        for (int i = 0; i < chargedSliders.Count; i++)
        {
            chargedSliders[i].enabled = false;
        }
        scene = SceneManager.GetActiveScene();
    }



    // Update is called once per frame
    void Update()
    {
        if (tailChargeable)
        {
            float gripPosition = tailSlapAction.action.ReadValue<float>();
            gripValue = gripPosition;
            TailSlap(gripValue);
        }
        

    }

    public void TailSlap(float value)
    {
        slapCharge += value;
        
        if (scene.name == "RootMenu")
        {
            if (slapCharge > 0)
            {
                if(tailSlapPanel.alpha < 1)
                {
                    tailSlapPanel.alpha = 1;
                }
            }
        }

        tailChargeDisplayed = Mathf.Clamp(slapCharge, 0f, tailChargeRequired);
        sliderPosition = tailChargeDisplayed / tailChargeRequired * 0.5f;
        for (int i = 0; i < chargedSliders.Count; i++)
        {
            chargingSliders[i].rectTransform.anchorMax = new Vector2(sliderPosition, chargingSliders[i].rectTransform.anchorMax.y);
        }

        if (slapCharge > tailChargeRequired) //there must be a better way of doing this that doesn't involve using the same if statement twice...
        {
            tailCharged = true;
            chargedText.text = "Charged";
            for (int i = 0; i < chargedSliders.Count; i++)
            {
                chargedSliders[i].enabled = true;
            }


        }
        

        if (value < 0.05f)
        {
            if (tailCharged)
            {
                //vertical jiggle
                StartCoroutine(TailslapVibration());
                if(scene.name == ("RootMenu"))
                {
                    mirrorOrcaAnimator.SetTrigger("Trigger_TailSlap");
                    Debug.Log("tailslap animation should be running");
                }
                if (scene.name == ("DEMO"))
                {
                    //spawn stunned herring
                    stunnedHerring.SetActive(true);
                }
                
            }
            slapCharge = 0f;
            tailCharged = false;
            chargedText.text = string.Empty;
            for (int i = 0; i < chargedSliders.Count; i++)
            {
                chargedSliders[i].enabled = false;
            }
        }
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

        //tailSlapPanel.DOFade(0, 1f);

        if (scene.name == "DEMO")
        {
            ActivateControlsDEMO.AC.DeActivateTailslapControls();
            StartCoroutine(DEMOCoroutine2.coroutine02.DEMOCoroutine02());
        }

        
        
        
    }
}
