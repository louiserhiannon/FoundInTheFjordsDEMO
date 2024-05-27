using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedHerringSelfDestruct : MonoBehaviour
{
    private float herringLifetime = 30;
    private void OnEnable()
    {
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(herringLifetime);
        gameObject.SetActive(false);
    }
}
