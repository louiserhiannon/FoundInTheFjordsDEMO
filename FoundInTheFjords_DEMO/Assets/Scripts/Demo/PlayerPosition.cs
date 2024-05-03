using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPosition
{
    public static bool isStanding;

    public static void DeterminePlayerPosition(bool position)
    {
        isStanding = position;
    } 
}
