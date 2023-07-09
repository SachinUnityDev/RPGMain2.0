using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Town
{
    [System.Serializable]
    public class SeqbarkModel 
    {
        public int DiaID;   // on Click track this ID
        public SeqBarkNames seqbarkName;

        public string barkTitle = "";

        public bool isLocked;
        public bool isRepeatable;

        public List<SeqBarkData> allBarkData = new List<SeqBarkData>();

        public SeqbarkModel(SeqBarkSO seqBarkSO) 
        {
            seqbarkName= seqBarkSO.seqbarkName;
            barkTitle= seqBarkSO.barkTitle; 
            
            isLocked= seqBarkSO.isLocked;
            isRepeatable= seqBarkSO.isRepeatable;

            allBarkData = seqBarkSO.allBarkData;
        }

    }

    public enum SeqBarkNames
    {
        None, 
        KhalidHouse, 
        TavernIntro, 
    }
}