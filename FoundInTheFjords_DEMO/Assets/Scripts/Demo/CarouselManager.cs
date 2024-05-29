using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarouselManager : MonoBehaviour
{
    public static CarouselManager CM;
    public GameObject axisPrefab;
    public int numOrca;
    public List<GameObject> allAxes;
    public Transform orcaTransform;
    public bool stunHerring = true;
    //public GameObject orcaPrefab;
    //public GameObject[] orcas;

    [Header("Orca Settings")]
    [Range(0.0f, 5.0f)]
    public float minOffset;
    [Range(0.0f, 5.0f)]
    public float maxOffset;
    [Range(-0.03f, 0.0f)]
    public float minTimeShift;
    [Range(0.0f, 0.03f)]
    public float maxTimeShift;
    [Range(1f, 5f)]
    public float minTimeStretch;
    [Range(1f, 5f)]
    public float maxTimeStretch;
    [Range(15f, 25.0f)]
    public float minSpeed;
    [Range(15f, 25.0f)]
    public float maxSpeed;
    [Range(-1.0f, 0.0f)]
    public float minAcceleration;
    [Range(0.0f, 1.0f)]
    public float maxAcceleration;
    [Range(10, 1000)]
    public int pathSensitivity;
    [Range(0.005f, 0.05f)]
    public float pathWanderSpeed;
    [Range(100, 1000)]
    public int distanceSensitivity;
    [Range(100, 5000)]
    public int animationSensitivity;
    public bool controlSpeedWithDistance;
    public GameObject stunnedHerringPrefab;
    [Range(1, 10)]
    public int minSpawnedHerring;
    [Range(1, 10)]
    public int maxSpawnedHerring;
    public float spawnOffsetX;
    public float spawnOffsetY;
    public float spawnOffsetZ;
    public float maxSlapDistance = 6.0f;


    private void Awake()
    {
        CM = this;
        SpawnCarouselOrca();
    }

    public void SpawnCarouselOrca()
    {
        //instantiate "numOrca" of axes at random orientations
        for(int i = 0; i < numOrca; i++)
        {
            //Vector3 orca1 = new Vector3(100, 0, 90);
            //Vector3 orca2 = new Vector3(-130, 50, 90);
            //Vector3 orca3 = new Vector3(0, -220, 90);

            Vector3 orca1 = new Vector3(0, 0, 0);
            Vector3 orca2 = new Vector3(0, -120, 40);
            Vector3 orca3 = new Vector3(-8, -252, 40);

            Quaternion axisRotation;
            if(i == 0)
            {
                axisRotation = Quaternion.Euler(orca1);
            }
            else if (i == 1)
            {
                axisRotation = Quaternion.Euler(orca2);
            }
            else
            {
                axisRotation = Quaternion.Euler(orca3);
            }
            allAxes.Add(Instantiate(axisPrefab, transform.position, axisRotation, orcaTransform));
            //change parent to orca transform, child of carousel transform
            //make orca tranform rotate
            //calculate quaternion from direction
            

            //allAxes[i].GetComponentInChildren<CarouselMotion>().SetRadialOffset(i);
            //hide them (look at earth elevator for code)

        }
        
    }

    
}
