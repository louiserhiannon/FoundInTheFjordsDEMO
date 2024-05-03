using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    
    protected float speed;
    [SerializeField] protected bool turning = false;
    //[SerializeField] protected bool turningAway = false;
    
    protected virtual void Start()
    {
        speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
    }

    
    protected void Update()
    {
        if (Random.Range(1, FlockManager.FM.boundsSensitivity) < 10)
        {
            Bounds outerBounds = new(FlockManager.FM.transform.position, FlockManager.FM.outerLimits * 1.5f);
            if (!outerBounds.Contains(transform.position))
            {
                turning = true;
            }

            else
            {
                turning = false;

            }
        }

        if (turning)
        {
            Vector3 direction = FlockManager.FM.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), FlockManager.FM.boundsRotationSpeed * Time.deltaTime);
        }

        else
        {
            if (Random.Range(1, FlockManager.FM.flockSensitivity) < 10)
            {
                speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
                ApplyRules();
            }
        }
        
            

        this.transform.Translate(0, 0, speed * Time.deltaTime);

    }

    protected void ApplyRules()
    {
        //flocking algorithm based on three rules:
        //1. Move toward average position of the group (calculate group centre and determine direction of each fish towards it)
        //2. Align with the aveage heading of the group (calculate average heading (where it's going - where it is)/number of fish)
        //3. Avoid crowding other group members (know where closest neighbours are and turn away from them)

        //new heading = vector towards group centre + group heading vector + avoidance vector

        //GameObject[] gos; //local array to hold all flockers
        List<GameObject> gos;
        gos = FlockManager.FM.allFlockers;

        //initialize local variables
        Vector3 vCentre = Vector3.zero; //group flock centre
        Vector3 vAvoid = Vector3.zero; //vector to avoid neighboure
        float groupSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        //calculate neighbour group parameters
        foreach(GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position); //calulate distance between this herring group and each other one
                if(nDistance <= FlockManager.FM.neighbourDistance) //if the neighbour is closer than the neighbour distance
                {
                    vCentre += go.transform.position; //adds neighbour position to group position sum (to be used to calculate local group centre)
                    groupSize++; //adds neighbour to group count

                    if(nDistance < FlockManager.FM.avoidDistance) //if the neighbour is closer than the avoid distance
                    {
                        vAvoid += (this.transform.position - go.transform.position); //Add direction vector AWAY from fish
                    }

                    Flock anotherFlock = go.GetComponent<Flock>(); //grab the flock component on each comparison herring group
                    groupSpeed += anotherFlock.speed; //add to overall local group speed
                }
            }
        }

        if(groupSize > 0)
        {
            
            vCentre /= groupSize; //calculate centre of neighbour group
            //vCentre += (FlockManager.FM.goalPosition - this.transform.position); //Calculate Vector towards group centre 
            speed = groupSpeed / groupSize; //calculate average speed of group
            if(speed > FlockManager.FM.maxSpeed)
            {
                speed= FlockManager.FM.maxSpeed; //ensure that group speed doesn't go above max speed
            }
            Vector3 direction = (vCentre + vAvoid) - transform.position; //calcuate net direction for fish to head towards
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction), FlockManager.FM.flockRotationSpeed * Time.deltaTime); //smoothly roate towards heading at specified rotation speed)
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("orca"))
        {
            turning = true;
        }
        //if (other.CompareTag("shoal"))
        //{
        //    turningAway= true;  
        //}    

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("orca"))
        {
            turning = false;
        }
        //if (other.CompareTag("shoal"))
        //{
        //    turningAway = false;
        //}
        
    }
}
