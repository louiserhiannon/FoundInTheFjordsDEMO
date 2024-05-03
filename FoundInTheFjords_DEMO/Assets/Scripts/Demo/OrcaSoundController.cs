using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaSoundController : MonoBehaviour
{
    //add this script to any orca that you want to produce calls. You also need to add an audio source and meta spatial audio source to each (if not already there)
    
    public float minCallPause = 2f;
    public float maxCallPause = 5f;
    private bool isCalling = true; //this bool just keeps the coroutine looping. You can stop the coroutine easily by just setting this to false somewhere
    private AudioSource audioSource; //this is the sound source that will play the clips
    
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //this grabs the audio source that sits on the orca game object. It should also have a Meta spatial audio script added 
        StartCoroutine(OrcasAreCalling()); //a coroutine is a type of function that can be interrupted, so statements can be executed over multiple frames
    }

    private IEnumerator OrcasAreCalling()
    {
        while(isCalling) //the coroutine keeps looping as long as isCalling is true, which is as long as the game object exists.
        {
            float pause = Random.Range(minCallPause, maxCallPause); //this sets up a temporary float variable and assigns it a random decimal number between 2 and 5 (you can specify what you want that to be)
            yield return new WaitForSeconds(pause); //waits for the calculated number of seconds
            int index = GetClipIndex(); //sets up a temporary int variable and assigns it a random value by calling the "GetClipIndex" method below (note that random ranges for ints are inclusive of the min value and exclusive of the max value)
            audioSource.PlayOneShot(OrcaCalls.instance.calls[index]);//plays the randomly selected call file
            yield return new WaitForSeconds(OrcaCalls.instance.calls[index].length); //waits until the selected clip is finished

            yield return null; //flips back to the beginning of the while loop
        }
    }

    private int GetClipIndex()
    {
        return Random.Range(0, OrcaCalls.instance.calls.Count); //returns a random integer "index" for the list of calls.
    }
}
