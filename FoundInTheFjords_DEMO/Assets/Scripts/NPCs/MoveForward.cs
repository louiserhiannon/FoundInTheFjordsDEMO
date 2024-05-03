using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;
    

    void Update()
    {
        Vector3 direction = transform.forward;
        transform.localPosition += speed * Time.deltaTime * direction;
    }
}
