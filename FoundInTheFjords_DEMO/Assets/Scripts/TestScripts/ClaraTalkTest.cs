using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaraTalkTest : MonoBehaviour
{
    public JellyTalkAnimation claraTalkAnimation;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TestClaraTalk());
    }

    public IEnumerator TestClaraTalk()
    {
        yield return new WaitForSeconds(5);
        claraTalkAnimation.isTalking = true;
        claraTalkAnimation.GetTalking();
        yield return new WaitForSeconds(45);
        claraTalkAnimation.isTalking = false;
        yield return new WaitForSeconds(10);
        //StartCoroutine(claraTalkAnimation.ClaraIsTalking());
        //claraTalkAnimation.isTalking = true;
        //yield return new WaitForSeconds(5);
        //claraTalkAnimation.isTalking = false;
        //yield return new WaitForSeconds(5);
        //StartCoroutine(claraTalkAnimation.ClaraIsTalking());
        //claraTalkAnimation.isTalking = true;
        //yield return new WaitForSeconds(5);
        //claraTalkAnimation.isTalking = false;
        //yield return new WaitForSeconds(5);
    }
}
