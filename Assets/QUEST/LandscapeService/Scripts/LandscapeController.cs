using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        public bool isApplied; 

        public LandBuffData(int buffID, CauseType causeType, int causeName, LandscapeNames landscapeName,
                        StatsName statModified, float val, bool isBuff = true
                        , float minVal =0, float maxVal=0, bool isApplied = false)
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
            this.isApplied = isApplied; 
        }
    }
    public class LandscapeController : MonoBehaviour
    {
        int buffIndex;
        List<LandBuffData> allLandBuffs = new List<LandBuffData> ();
        CharController charController;

        private void Awake()
        {
          
        }

        void Start()
        {
            LandscapeService.Instance.OnLandscapeEnter += OnLandscapeEnter;
            LandscapeService.Instance.OnLandscapeExit += OnLandscapeExit; 
            charController = GetComponent<CharController> ();
        }

        public int ApplyLandscapeBuff(CauseType causeType, int causeName
            , LandscapeNames landScapeName, StatsName statName, float val)
        {
            buffIndex++;
            LandBuffData landBuffData = new LandBuffData(buffIndex, causeType, causeName,
                landScapeName, statName, val);

            allLandBuffs.Add(landBuffData); 
            
            if(LandscapeService.Instance.currLandscape == landScapeName)
                ApplyLandBuffFX(landBuffData);  
            
            return buffIndex;
        }

        public void RemoveBuff(int buffID)
        {
            int i = allLandBuffs.FindIndex(t => t.buffID == buffID);
            LandBuffData landBuffData = allLandBuffs[i];

            if(landBuffData.isApplied)
                RemoveBuffFX(landBuffData);
            allLandBuffs.Remove(landBuffData);

        }

        void ApplyLandBuffFX(LandBuffData landBuff)
        {
            charController.ChangeStat(landBuff.causeType, landBuff.causeName
             , charController.charModel.charID, landBuff.statModified, landBuff.val);
        }

        public void RemoveBuffFX(LandBuffData landBuff)
        {
            // remove the statchange caused by landscape
            charController.ChangeStat(landBuff.causeType, landBuff.causeName
                , charController.charModel.charID, landBuff.statModified, -landBuff.val); 

        }

        void OnLandscapeEnter(LandscapeNames landScapeName)  // connect to the landscape event
        {           
            foreach (LandBuffData land in allLandBuffs)
            {
                if(land.landscapeName == landScapeName)
                {
                    ApplyLandBuffFX(land); 
                }
            }
        }
        void OnLandscapeExit(LandscapeNames landScapeName)  // connect to the landscape event
        {
            foreach (LandBuffData land in allLandBuffs)
            {
                if (land.landscapeName == landScapeName)
                {
                    RemoveBuffFX(land);
                }
            }
        }
    }
}