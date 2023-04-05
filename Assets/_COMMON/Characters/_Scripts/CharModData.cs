using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class AttribModData   // data class to store the current changes in the game 
    {

        public int turnNo;
        public CauseType causeType;  // add cause name here 
        public int causeName;
        public int causeByCharID;
        public int effectedCharNameID;
        public AttribName attribModified;       

        public float modCurrVal = 0f;
        public float baseVal = 0f;  

        public float modChgMaxR = 0f;
        public float modChgMinR = 0f;
        
        // ON STAT RANGE MODIFIED 
        public AttribModData(int turnNo, CauseType causeType, int causeName, int causedByCharID
            , int effectedCharNameID, AttribName statModfified, float modChgMaxR, float modChgMinR, bool isRangeMod)
        {
            this.turnNo = turnNo;
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causedByCharID;
            this.effectedCharNameID = effectedCharNameID;

            this.attribModified = statModfified;
            this.modChgMaxR = modChgMaxR;
            this.modChgMinR = modChgMinR;
        }


        // ON STAT MODIFIED 

        public AttribModData(int turnNo, CauseType causeType, int causeName, int causedByCharID
                                       , int effectedCharNameID, AttribName statModified,  float modCurrVal =-1, float baseVal =-1)
        {
            this.turnNo = turnNo;
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causedByCharID;
            this.effectedCharNameID = effectedCharNameID;
            this.attribModified = statModified;
            if(modCurrVal != -1)
                this.modCurrVal = modCurrVal;
            if (baseVal != -1)
                this.baseVal = baseVal;
              
        }

    }

    public class StatModData   // data class to store the current changes in the game 
    {

        public int turnNo;
        public CauseType causeType;  // add cause name here 
        public int causeName;
        public int causeByCharID;
        public int effectedCharNameID;
        public StatName statModified;

        public float modVal = 0f;

        public StatModData(int turnNo, CauseType causeType, int causeName, int causedByCharID
            , int effectedCharNameID, StatName statModfified, float modVal)
        {
            this.turnNo = turnNo;
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causedByCharID;
            this.effectedCharNameID = effectedCharNameID;

            this.statModified = statModfified;
            this.modVal = modVal; 
        }
    
    }

}