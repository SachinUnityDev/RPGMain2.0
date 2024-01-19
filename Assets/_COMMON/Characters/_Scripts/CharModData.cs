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
        public int chgVal = 0;
        public bool isClamped = false; 

        public AttribModData() { }
        public AttribModData(int turnNo, CauseType causeType, int causeName, int causedByCharID
                                       , int effectedCharNameID, AttribName statModified, float modCurrVal = -1, float baseVal = -1, bool isClamped = false)
        {
            this.turnNo = turnNo;
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causedByCharID;
            this.effectedCharNameID = effectedCharNameID;
            this.attribModified = statModified;
            if (modCurrVal != -1)
                this.modCurrVal = modCurrVal;
            if (baseVal != -1)
                this.baseVal = baseVal;
            this.isClamped = isClamped; 
        }
        public AttribModData(int turnNo, CauseType causeType, int causeName, int causedByCharID
                                       , int effectedCharNameID, AttribName statModified,  float modCurrVal, int chgVal, bool isClamped = false)
        {
            this.turnNo = turnNo;
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causedByCharID;
            this.effectedCharNameID = effectedCharNameID;
            this.attribModified = statModified;
            if(modCurrVal != -1)
                this.modCurrVal = modCurrVal;
                this.chgVal = chgVal;
            this.isClamped = isClamped;   
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
        public int valChg = 0; 
        public int modVal = 0;
        public bool isClamped = false;  

        public StatModData(int turnNo, CauseType causeType, int causeName, int causedByCharID
            , int effectedCharNameID, StatName statModfified, int modVal, int valChg, bool isClamped = false)
        {
            this.turnNo = turnNo;
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causedByCharID;
            this.effectedCharNameID = effectedCharNameID;

            this.statModified = statModfified;
            this.modVal = modVal; 
            this.valChg = valChg;
            this.isClamped = isClamped;
        }
    
    }

}