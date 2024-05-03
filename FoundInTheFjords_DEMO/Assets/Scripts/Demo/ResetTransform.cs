using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour
{
    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Awake()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != originalPosition)
        {
            Debug.Log("moved to position " + transform.position + " at " + Time.time);
            transform.position = originalPosition;
            
        }
    }
}
