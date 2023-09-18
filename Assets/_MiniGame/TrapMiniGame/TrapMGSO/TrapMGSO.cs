using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [Serializable]
    public class TimeNFlashTimeData
    {
        public float timeElapsedMark;
        public float flashTime;
    }

    [CreateAssetMenu(fileName = "TrapMGSO", menuName = "MiniGame/TrapMGSO")]

    public class TrapMGSO : ScriptableObject
    {
        public GameDifficulty gameDifficulty;
        public int correctHitsNeeded; 
        public int mistakesAllowed;
        public float netTime; 
    }
}