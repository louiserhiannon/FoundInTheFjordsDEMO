using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class OrcaOscillation : MonoBehaviour
{
    [SerializeField] private float positionX;
    [SerializeField] private float positionY;
    [SerializeField] private float positionZ;
    [SerializeField] private float centrePositionX;
    [SerializeField] private float bottomPositionY;
    [SerializeField] private float centrePositionZ;
    [SerializeField] private float maxY;
    [SerializeField] private float maxXOffset;
    [SerializeField] private float maxZOffset;
    [SerializeField] private float timeOffsetX;
    [SerializeField] private float timeOffsetY;
    [SerializeField] private float timeOffsetZ;
    [SerializeField] private float timeStretchX;
    [SerializeField] private float timeStretchY;
    [SerializeField] private float timeStretchZ;
    [SerializeField] private float time = 0;

    private void Start()
    {
        maxY = 1.0f;
    }

    public void SetOscillationParameters()
    {
        centrePositionX = transform.position.x;
        bottomPositionY = transform.position.y;
        centrePositionZ = transform.position.z;
        positionX = transform.position.x;
        positionY = transform.position.y;
        positionZ = transform.position.z;
        maxXOffset = Random.Range(0.0f, 1f);
        maxZOffset = Random.Range(0.0f, 2.0f);
        timeOffsetX = Random.Range(0.0f, 10.0f);
        timeOffsetY = Random.Range(0.0f, 10.0f);
        timeOffsetZ = Random.Range(0.0f, 10.0f);
        timeStretchX = Random.Range(OrcaOscillationController.OOC.minStretch, OrcaOscillationController.OOC.maxStretch);
        timeStretchY = Random.Range(OrcaOscillationController.OOC.minStretch, OrcaOscillationController.OOC.maxStretch);
        timeStretchZ = Random.Range(OrcaOscillationController.OOC.minStretch, OrcaOscillationController.OOC.maxStretch);

    }

    // Update is called once per frame
    void Update()
    {
        if (OrcaOscillationController.OOC.isOscillating)
        {
            if (time > timeOffsetX)
            {
                positionX = centrePositionX + maxXOffset * Mathf.Sin((time - timeOffsetX) / timeStretchX);
            }
            else
            {
                positionX = transform.position.x;
            }
                

            if (time > timeOffsetY)
            {
                positionY = bottomPositionY + (maxY - bottomPositionY) * ((-1 * Mathf.Cos((time-timeOffsetY)/timeStretchY)) + 1) / 2;
            }
            else
            {
                positionY = transform.position.y;
            }

            if (time > timeOffsetZ)
            {
                positionZ = centrePositionZ + maxZOffset * Mathf.Sin((time - timeOffsetZ) / timeStretchZ);
            }
            else
            {
                positionZ = transform.position.z;
            }

            

            transform.position = new Vector3 (positionX, positionY, positionZ);

            time += Time.deltaTime;
        }

        else
        {
            time = 0;
        }
    }
}
