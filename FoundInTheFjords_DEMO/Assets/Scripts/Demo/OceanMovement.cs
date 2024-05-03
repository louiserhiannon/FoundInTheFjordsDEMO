using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanMovement : MonoBehaviour
    //Controls the movement of the Ocean Boxes to give the sensation of fast movement. Blocks of underwater environment move rapidly in an "infinite runner" mechanic.
{
    public static OceanMovement OM;
    public List<Transform> activePoints;
    public List<GameObject> oceanBoxPrefabs;
    public float currentDistance;
    public float speed;
    public float maxSpeed = 15f;
    public float maxDistance = 10000f;
    public float endPositionZ = -500f;
    public float acceleration = 25f;
    public float resetDistance = 1500f;
    public bool isMoving = false;
    private bool wavesOn = false;
    public WaveManager waveManager;

    private void Awake()
    {
        OM = this;
    }
    void Start()
    {
        //instantiate ocean boxes
        for (int i = 0; i < activePoints.Count; i++)
        {
            Instantiate(oceanBoxPrefabs[i], activePoints[i], false);
        }

        //set current distance to 0
        currentDistance = 0f;
        isMoving= false;
        
        

    }
        
    void Update()
    {
        //Debug.Log(Time.deltaTime);
        if (speed > 0)
        {
            if (currentDistance < maxDistance)
            {

                for (int i = 0; i < activePoints.Count; i++)
                {
                    //if box is beyond the end of the range, flip it back to the beginning
                    if (activePoints[i].transform.position.z <= endPositionZ)
                    {
                        activePoints[i].transform.Translate(0f, 0f, resetDistance);
                    }
                    //otherwise move it along at a specific speed
                    activePoints[i].transform.Translate(0f, 0f, -speed * Time.deltaTime);


                }
                currentDistance += speed * Time.deltaTime; //update distance travelled

            }
        }

        if (isMoving)
        {


            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime; //control speed
                
            }
            else
            {
                speed = maxSpeed;
            }
            

        }
        else
        {
            if (speed > 0f)
            {
                speed -= acceleration * Time.deltaTime; //control speed

            }
            else
            {
                speed = 0f;
            }

        }

             
        if(speed < 0.1f && !isMoving)
        {
            currentDistance = 0f;   
        }

        if (waveManager != null)
        {
            if(isMoving)
            {
                waveManager.enabled = false;
                wavesOn = false;
            }
            else
            {
                if (!wavesOn)
                {
                    waveManager.enabled = true;
                    wavesOn = true;
                }
            }
        }

    }
}
