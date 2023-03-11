using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [System.Serializable]
    public class FameModel
    {
        [Header("FAME VALUES")]
        public int fameVal;


        [Header("FameType")]

        public FameType fameType;
       

        [Header("Modifier values")]
        public float fameYield = 1;

        
        [Header("FAME")]
        public List<FameChgData> allFameData = new List<FameChgData>();
    }
}

