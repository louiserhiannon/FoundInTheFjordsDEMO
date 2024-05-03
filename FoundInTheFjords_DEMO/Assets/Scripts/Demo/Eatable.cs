using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatable : MonoBehaviour
{
    private Material herringMaterial;
    private Color hoverColor;
    private Color nonHoverColor;
    private float lifetime = 0;
    public bool hoverActivated = false;
    

    // Start is called before the first frame update
    void Start()
    {
        herringMaterial = GetComponentInChildren<SkinnedMeshRenderer>().material;
        hoverColor = Color.magenta;
        nonHoverColor = Color.white;

    }

    private void OnEnable()
    {
        lifetime = 0;
    }

    private void Update()
    {
        lifetime += Time.deltaTime;
        if(lifetime >= EatingControllerDEMO.ECDemo.herringLifetime)
        {
            transform.position = HerringSpawner.HS.transform.position;
            gameObject.SetActive(false);
            OnHoverEnd();
            if (TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
            }
            for (int i = EatingControllerDEMO.ECDemo.eatableHerrings.Count - 1; i > -1; i--)
            {
                if (!EatingControllerDEMO.ECDemo.eatableHerrings[i].gameObject.activeSelf)
                {
                    EatingControllerDEMO.ECDemo.eatableHerrings.RemoveAt(i);
                    
                }
            }
        }
    }

    public void OnHoverStart()
    {
        herringMaterial.color = hoverColor;
        hoverActivated = true;
    }

    public void OnHoverEnd()
    {
        herringMaterial.color = nonHoverColor;
        hoverActivated = false;
    }


}
