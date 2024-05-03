using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaCalls : MonoBehaviour
{
    //add this to the scene manager game object of the scene and then add the clips (and clip lengths) for all the calls you want to use. Each instance of the Orca Sound Controller (which is added to each orca) will reference this instance of the script.
    
    public static OrcaCalls instance; //sets this class as a static reference, so anything in the game can call it. You can add it to any game object, but must only be added to one object per scene
    public List<AudioClip> calls; //public list of orca calls that you can draw from. You just add the files to the list in the inspector
    //public List<float> callDurations; //public list of the durations of each of the calls added in the sound file list above


    private void Awake()
    {
        //this code just makes sure there isn't another instance of the static class already present in the scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

}
