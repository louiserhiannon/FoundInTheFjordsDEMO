using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager_Circular : MonoBehaviour
{
    //public GameObject flockerPrefab;
    public static FlockManager_Circular FM;
    public GameObject flock;
    public List<GameObject> allFlockers;
    public int numFlockers = 0;
    //public Transform carouselTransform;

    [Header("Flocker Settings")]
    [Range(1.0f, 25.0f)]
    public float rotationSpeed;
    //[Range(-2.0f, 0.0f)]
    //public float minFlockRadialOffset;
    //[Range(0.0f, 2.0f)]
    //public float maxFlockRadialOffset;
    //[Range(-2.0f, 0.0f)]
    //public float minFlockHeight;
    //[Range(0.0f, 2.0f)]
    //public float maxFlockHeight;


    // Start is called before the first frame update
    void Start()
    {
        FM = this;
        //flock = Instantiate(flockerPrefab, transform.position, Quaternion.identity, carouselTransform);
        flock.SetActive(true);
        foreach (Rigidbody r in flock.GetComponentsInChildren<Rigidbody>())
        {
            allFlockers.Add(r.gameObject);
            numFlockers++;
            Debug.Log(numFlockers);
        }
        //flock.SetActive(false);
       


    
        //float segmentAngle = 360 / numFlockers;
        //for (int i = 0; i < numFlockers; i++)
        //{

        //    Vector3 eulerAngle = new Vector3(0, segmentAngle * (i+1), 0);
        //    Quaternion axisRotation = Quaternion.Euler(eulerAngle);
        //    float height = Random.Range(minFlockHeight, maxFlockHeight);
        //    Vector3 spawnPos = transform.position + new Vector3(0, height, 0);
        //    allFlockers.Add(Instantiate(flockerPrefab, spawnPos, axisRotation, carouselTransform));
        //    //allFlockers[i].SetActive(false);

        //}
        
    }

    
}
