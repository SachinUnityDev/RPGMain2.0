using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{


    [Serializable]
    public class TrapMGModel
    {
        GameDifficulty gameDifficulty; // just for ref
       
        public int correctHitsNeeded;      
        public int mistakesAllowed;

        public float timeElapsed;
        public float netTime;

        public TrapMGModel(TrapMGSO trapMGSO)
        {
            gameDifficulty = trapMGSO.gameDifficulty;

            correctHitsNeeded = trapMGSO.correctHitsNeeded;
            mistakesAllowed = trapMGSO.mistakesAllowed;
            netTime = trapMGSO.netTime;
        }
    }
}