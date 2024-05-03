using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerringCircularMotion : MonoBehaviour
{
    private Transform parentTransform;
    
    void Start()
    {
        //set random distance from centre
        parentTransform = transform.parent;
        //float offset = Random.Range(FlockManager_Circular.FM.minFlockRadialOffset, FlockManager_Circular.FM.maxFlockRadialOffset);
        //Vector3 dir = (transform.position - parentTransform.position).normalized;
        //transform.Translate(offset * dir);
    }

    // Update is called once per frame
    void Update()
    {
        //rotates around axis at given speed
        transform.RotateAround(parentTransform.position, parentTransform.up, -FlockManager_Circular.FM.rotationSpeed * Time.deltaTime);
    }
}
