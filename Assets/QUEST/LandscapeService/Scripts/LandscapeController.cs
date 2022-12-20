using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Quest
{
 
    public class LandBuffData
    {
        public int buffID;        
        public CauseType causeType;
        public int causeName;
        public LandscapeNames landscapeName;       
        public StatsName statModified;        
        public float val;
        public bool isBuff;
        public float minVal; 
        public float maxVal;

        public LandBuffData(int buffID, CauseType causeType, int causeName, LandscapeNames landscapeName,
                     StatsName statModified, float val, bool isBuff = true, float minVal =0, float maxVal=0)
        {
            this.buffID = buffID;
            this.causeType = causeType;
            this.causeName = causeName;
            this.landscapeName = landscapeName;          
            this.statModified = statModified;
            this.val = val;
            this.isBuff = isBuff;
            this.minVal = minVal;
            this.maxVal = maxVal;
        }
    }

    //public int buffID;
    //public bool isBuff;   // true if BUFF and false if DEBUFF
    //public int startRoundNo;
    //public TimeFrame timeFrame;
    //public int buffedNetTime;
    //public int buffCurrentTime;
    //public CharModData charModData;
    //public string directString;

    public class LandscapeController : MonoBehaviour
    {
        int buffIndex;
        List<LandBuffData> allLandBuffs = new List<LandBuffData> ();
        // Start is called before the first frame update
        void Start()
        {
            LandscapeService.Instance.OnLandScapeChg += OnLandscapeChg; 
        }

        public int ApplyLandscapeBuff(CauseType causeType, int causeName
            , LandscapeNames landScapeName, StatsName statName, float val)
        {
            buffIndex++;

            LandBuffData landBuffData = new LandBuffData(buffIndex, causeType, causeName,
                landScapeName, statName, val);

            allLandBuffs.Add(landBuffData); 
            
            return buffIndex;
        }

        public void RemoveBuff(int buffID)
        {
            int i = allLandBuffs.FindIndex(t => t.buffID == buffID);
            LandBuffData landBuffData = allLandBuffs[i];


        }

        public void RemoveBuffFX(LandBuffData landBuff)
        {
            // remove the statchange caused by landscape

        }


        void OnLandscapeChg(LandscapeNames landScapeName)  // connect to the landscape event
        {
            // if buff not in the landscape remove FX else 
            // else add buff


        }

    }
}