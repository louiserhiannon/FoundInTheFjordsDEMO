using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSTracker : MonoBehaviour
{
    public float updateFrequency = 0f;

    private float targetFPS=72;
    private float currentFPS = 0;
    private float deltaTime = 0f;

    private TextMeshProUGUI textFPS;   
    
    void Start()
    {
        textFPS = GetComponent<TextMeshProUGUI>();
        StartCoroutine(DisplayFramesPerSecond());
    }

    // Update is called once per frame
    void Update()
    {
        GenerateFramesPerSecond();
    }

    private void GenerateFramesPerSecond()
    {
        //deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        //currentFPS = 1.0f / deltaTime;
        deltaTime = Time.unscaledDeltaTime;
        currentFPS = Mathf.Round(1.0f / deltaTime);
    }

    public IEnumerator DisplayFramesPerSecond()
    {
        while (true)
        {
            if(currentFPS >= targetFPS)
            {
                textFPS.color = Color.green;
            }
            else
            {
                textFPS.color = Color.red;
            }
            textFPS.text = currentFPS.ToString();
            
            yield return new WaitForSeconds(updateFrequency);
        }
        
        
    }
}
