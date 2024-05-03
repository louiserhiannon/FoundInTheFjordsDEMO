using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject flockerPrefab; 
    public static FlockManager FM;
    public List<GameObject> allFlockers;
    public int numFlockers;
    public Vector3 outerLimits = new(1f, 1f, 1f);
    public Vector3 goalPosition = Vector3.zero;

    //public Bounds outerBounds;
    //public Bounds innerBounds;

    [Header("Flocker Settings")]
    [Range(0.1f, 10.0f)]
    public float minSpeed;
    [Range(0.1f, 10.0f)]
    public float maxSpeed;
    [Range(1.0f, 25.0f)]
    public float neighbourDistance;
    [Range(0.1f, 50.0f)]
    public float avoidDistance;
    [Range(0.1f, 5.0f)]
    public float flockRotationSpeed;
    [Range(0.1f, 5.0f)]
    public float boundsRotationSpeed;
    [Range(50, 1000)]
    public int goalSensitivity;
    [Range(50, 1000)]
    public int flockSensitivity;
    [Range(50, 1000)]
    public int boundsSensitivity;


    public virtual void Start()
    {
        FM = this;
        //allFlockers = new GameObject[numFlockers];
        for (int i = 0; i < numFlockers; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-outerLimits.x, outerLimits.x), Random.Range(-outerLimits.y, outerLimits.y), Random.Range(-outerLimits.z, outerLimits.z));
            //allFlockers[i] = Instantiate(flockerPrefab, pos, Quaternion.identity);
            allFlockers.Add(Instantiate(flockerPrefab, pos, Quaternion.identity));

        }
        goalPosition = this.transform.position;
    }

    public virtual void Update()
    {
        if (Random.Range(1, goalSensitivity) < 10)
        {
            goalPosition = this.transform.position + new Vector3(Random.Range(-outerLimits.x, outerLimits.x), Random.Range(-outerLimits.y, outerLimits.y), Random.Range(-outerLimits.z, outerLimits.z));
        }

    }
}
