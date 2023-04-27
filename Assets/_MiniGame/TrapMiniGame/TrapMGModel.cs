using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrapMGModel
{
    public int wrongHits;
    public int maxWrongHits;
    public float timeElapsed; 
    public float netTime;

    public TrapMGModel(TrapMGSO trapMGSO)
    { 
        maxWrongHits = trapMGSO.maxWrongHits;        
        netTime = trapMGSO.netTime;
    }
}
