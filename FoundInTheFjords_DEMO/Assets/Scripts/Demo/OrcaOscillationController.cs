using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaOscillationController : MonoBehaviour
{
    public static OrcaOscillationController OOC;
    public bool isOscillating;
    [Range(1f, 10f)] public float minStretch;
    [Range(1f, 10f)] public float maxStretch;
    private void Awake()
    {
        OOC = this;
        isOscillating = false;
    }
}
