using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager_Zodiac : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(BackToBigShip());
    }

    private IEnumerator BackToBigShip()
    {
        yield return new WaitForSeconds(30);
        ChangeScene.instance.SceneSwitch("Scene06-CalltoAction");
    }
}
