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
        public int fameYield = 1;
        
        [Header("FAME Change")]
        public List<FameChgData> allFameData = new List<FameChgData>();
        public FameModel(FameSO fameSO)
        {
            fameVal = fameSO.fameVal; 
            fameType= fameSO.fameType;
            fameYield = fameSO.FameYield; 
        }
    }
}

