using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardButton : MonoBehaviour
{
    Keyboard keyboard;
    [SerializeField] TextMeshProUGUI buttonText;

    public void Start()
    {
        keyboard = GetComponentInParent<Keyboard>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        if (buttonText.text.Length == 1)
        {
            NameToButtonText();
        }
    }

    public void NameToButtonText()
    {
        buttonText.text = gameObject.name;
    }
   
}
