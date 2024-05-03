using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMom : MonoBehaviour
{
    public Transform orcaMom;
    public float minFollowDistance = 1.5f;
    public float maxFollowDistance = 3.0f;
    private float followSpeed;
    public float highSpeed = 3f;
    public float lowSpeed = 2.2f;
    public Vector3 direction;

    private void Start()
    {
        direction = transform.position - orcaMom.position;
    }

    public void Follow()
    {
        //calculate rotation (match to mom)
        transform.rotation = orcaMom.rotation;
        //calculate position

        transform.position = orcaMom.position - direction;

        float followDistance = Vector3.Distance(orcaMom.position, transform.position);
        

        if (followDistance - minFollowDistance > 0f)
        {
            if (followDistance > maxFollowDistance)
            {
                followSpeed = highSpeed;

            }
            else
            {
                followSpeed = lowSpeed;
            }
            this.transform.Translate(0, 0, followSpeed * Time.deltaTime);

        }
        else
        {
            followSpeed = 0;
        }

        this.transform.Translate(0, 0, followSpeed * Time.deltaTime);
    }
}
