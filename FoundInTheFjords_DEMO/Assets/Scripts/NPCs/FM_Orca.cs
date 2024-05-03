using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FM_Orca : FlockManager
{
    
    public override void Start()
    {
        base.Start();
        numFlockers = allFlockers.Count;
    }

    public override void Update()
    {
        base.Update(); 
    }


}
