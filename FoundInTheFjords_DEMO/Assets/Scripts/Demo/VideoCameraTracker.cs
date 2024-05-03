using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class VideoCameraTracker : MonoBehaviour
{
    //public float clipLength = 5.239f;
    public float initialRotationY = 180;
    public float finalRotationY = 181.65f;
    //[Range(0f, 6f)]
    //public float offset;
    //[SerializeField] private float time;
    //[SerializeField] private float increment;
    public VideoPlayer videoPlayer;
    private bool loopCamera = true;
    
    // Start is called before the first frame update
    void Start()
    {
        //increment = (finalRotationY - initialRotationY)/clipLength;
        //time = 0;
        StartCoroutine(MoveCamera());
    }

    //private void OnEnable()
    //{
    //    videoPlayer.loopPointReached += ResetCamera;
    //}

    private IEnumerator MoveCamera()
    {
        //while (Time.time <= offset)
        //{
        //    yield return null;
        //}
        float eulerY;

        while (loopCamera)
        {
            if ((ulong)videoPlayer.frame == 0)
            {
                ResetCamera();
            }
            eulerY = initialRotationY + (float)videoPlayer.frame / (float)videoPlayer.frameCount * (finalRotationY - initialRotationY);
            transform.eulerAngles = new Vector3(0, eulerY, 0);
            
            yield return null;

            //Debug.Log(loopCamera);
            //while (transform.eulerAngles.y < finalRotationY)
            //{
            //    //Debug.Log(time);
            //    //time += Time.deltaTime;
            //    transform.Rotate(0, increment * Time.deltaTime, 0);
            //    yield return null;
            //}

            //eulerY = initialRotationY - finalRotationY;
            //transform.Rotate(0, eulerY, 0);
            //time = 0;

            //while ((ulong) videoPlayer.frame < videoPlayer.frameCount)
            //{
                
            //}

            
            //yield return new WaitForSeconds(Time.deltaTime);
            //yield return new WaitForFixedUpdate();
            ////yield return new WaitForSeconds(Time.deltaTime);
            ////yield return new WaitForSeconds(Time.deltaTime);
            ////Debug.Log(FirstFrame());
            //transform.eulerAngles = new Vector3(0, initialRotationY, 0);
            //yield return null;
        }
        
        
    }

    private void ResetCamera()
    {
        //vp.frame = 1;
        transform.eulerAngles = new Vector3(0, initialRotationY, 0);
    }


}
