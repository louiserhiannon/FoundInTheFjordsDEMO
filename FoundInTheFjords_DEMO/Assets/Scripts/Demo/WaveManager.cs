using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //moves water surface texture 
    protected MeshRenderer waterSurface;
    public float flowSpeedX;
    public float flowSpeedY;
    public int index;
    protected float textureOffsetX;
    protected float textureOffsetY;
    protected Vector2 textureFlow;
   
    void Start()
    {
        waterSurface = GetComponent<MeshRenderer>();
        flowSpeedX = 0.002f;
        flowSpeedY = 0.002f;

    }

    
    void Update()
    {
        FlowPattern();
    }

    public virtual void FlowPattern()
    {
        textureOffsetX += flowSpeedX * Time.deltaTime;
        textureOffsetY += flowSpeedY * Time.deltaTime;
        textureFlow = new Vector2(textureOffsetX, textureOffsetY);
        waterSurface.materials[index].mainTextureOffset = textureFlow;
    }
}
