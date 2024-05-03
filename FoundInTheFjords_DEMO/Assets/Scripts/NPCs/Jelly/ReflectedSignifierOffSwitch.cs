using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectedSignifierOffSwitch : MonoBehaviour
{
    public GameObject claraInteractable;

    // Update is called once per frame
    void Update()
    {
        if (!claraInteractable.activeSelf)
        {
            if(gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
