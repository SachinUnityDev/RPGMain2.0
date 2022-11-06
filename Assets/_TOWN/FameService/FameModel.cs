using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [System.Serializable]
    public class FameModel
    {
        [Header("FAME VALUES")]
        public float currGlobalFame;
        public float currNekkisariFame;

        [Header("FameType")]

        public FameType fametypGlobal;
        public FameType fametypNekkisari;

        [Header("Modifier values")]
        public float globalFameMod;
        public float nekkisariFameMod; 

        [Header("FAME")]
        public List<FameChgData> globalfameDataAll = new List<FameChgData>();
        public List<FameChgData> nekkisarifameDataAll = new List<FameChgData>();


    }
}

