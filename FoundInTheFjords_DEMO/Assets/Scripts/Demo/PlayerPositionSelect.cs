using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionSelect : MonoBehaviour
{
    public void SelectStanding()
    {
        PlayerPosition.DeterminePlayerPosition(true);
        //Load Next Scene
        ChangeScene.instance.SceneSwitch("RootMenu");
    }

    public void SelectSeated()
    {
        PlayerPosition.DeterminePlayerPosition(false);
        //Load Next Scene
        ChangeScene.instance.SceneSwitch("RootMenu");
    }

}

