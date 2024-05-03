using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager_Ripples : WaveManager
{
    public float maxTileX;
    public float minTileX;
    public float maxTileY;
    public float minTileY;
    private float cosTime;
    public float flowOffset;
    [SerializeField] private float textureScaleX;
    [SerializeField] private float textureScaleY;

    private Vector2 textureMorph;
    public override void FlowPattern()
    {
        cosTime = Mathf.Cos(Time.time);
        Debug.Log(cosTime);
        textureScaleX = minTileX + (1 + Mathf.Cos(Time.time * flowSpeedX)) / 2 * (maxTileX - minTileX);
        textureScaleY = minTileY + (1 + Mathf.Cos(Time.time * flowSpeedY + flowOffset)) / 2 * (maxTileY - minTileY);
        textureMorph = new Vector2(textureScaleX, textureScaleY);
        waterSurface.materials[0].mainTextureScale = textureMorph;

    }
}
