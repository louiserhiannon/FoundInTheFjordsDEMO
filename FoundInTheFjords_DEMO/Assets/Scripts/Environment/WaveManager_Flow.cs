using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager_Flow : WaveManager
{
    public override void FlowPattern()
    {
        textureOffsetX = flowSpeedX * Time.time;
        textureOffsetY = flowSpeedY * Time.time;
        textureFlow = new Vector2(textureOffsetX, textureOffsetY);
        waterSurface.material.mainTextureOffset = textureFlow;

    }
}
