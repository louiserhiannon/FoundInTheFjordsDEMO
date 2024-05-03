using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovementController : MonoBehaviour
{
    private float startHeight;
    public float translationSpeed = 10;
    public float rotationSpeed = 5;
    public float offset;
    public float range;

    private void Start()
    {
        startHeight = transform.position.y;
    }

    void Update()
    {
        //Move transform up and down (sin)
        float height = startHeight + range * Mathf.Sin(Time.time/translationSpeed + offset);
        transform.position = new Vector3(transform.position.x, height, transform.position.z);

        //rotate around centre
        transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);

    }
}
