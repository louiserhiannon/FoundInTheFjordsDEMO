using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelfDestruct : MonoBehaviour
{

    private float lifetime = 0f;
    
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            lifetime += Time.deltaTime;
            if (lifetime >= EatingControllerDEMO.ECDemo.herringLifetime)
            {
                gameObject.SetActive(false);
                if (TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                {
                    rigidbody.useGravity = false;
                    rigidbody.isKinematic = true;
                }
            }
        }
        
    }
}
