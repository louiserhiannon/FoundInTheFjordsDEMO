using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HerringCounter : MonoBehaviour
{
    //public EatingControllerDEMO eatingControllerDEMO;
    public CanvasGroup counterDisplay;
    private TMP_Text counterText;
    public bool displayActive = false;
    



    // Start is called before the first frame update
    void Start()
    { 
        counterText = counterDisplay.GetComponentInChildren<TMP_Text>();
        //////Disable Panel
        //counterDisplay.alpha = 0f;
        //counterDisplay.blocksRaycasts = false;
        //counterDisplay.interactable = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (displayActive)
        {
            counterText.text = EatingControllerDEMO.ECDemo.eatenHerringCount.ToString();
        }
            
            
    }
}
