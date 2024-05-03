using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRopeCoroutine : MonoBehaviour
{
    public RopeDescent ctd;
    public RopeDescent hydrophone;

    void Start()
    {
        StartCoroutine(LowerEquipment());
    }

    public IEnumerator LowerEquipment()
    {
        StartCoroutine(LowerCTD());
        
        yield return new WaitForSeconds(3f);
        StartCoroutine(LowerHydrophone());
    }

    public IEnumerator LowerCTD()
    {
        while(ctd.equipment.transform.position.y > -9)
        {
            ctd.LowerItem();
            yield return null;
        }
    }

    public IEnumerator LowerHydrophone()
    {
        while (hydrophone.equipment.transform.position.y > -6)
        {
            hydrophone.LowerItem();
            yield return null;
        }
    }
}
