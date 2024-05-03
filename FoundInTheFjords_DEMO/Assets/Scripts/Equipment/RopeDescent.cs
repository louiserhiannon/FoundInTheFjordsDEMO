using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDescent : MonoBehaviour
{
    public GameObject ropeTransform;
    public GameObject equipment;
    public float speed;

    public void LowerItem()
    {
        float scaleY = ropeTransform.transform.localScale.y;
        scaleY += speed * Time.deltaTime;
        ropeTransform.transform.localScale = new Vector3(ropeTransform.transform.localScale.x, scaleY, ropeTransform.transform.localScale.z);

        float posY = equipment.transform.position.y;
        posY -= speed * Time.deltaTime;
        equipment.transform.position = new Vector3(equipment.transform.position.x, posY, equipment.transform.position.z);
    }

    
}
