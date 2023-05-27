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
        public AttribName statModified;        
        public float val;
        public bool isBuff;
        public float minVal; 
        public float maxVal;
        public bool isApplied; 

        public LandBuffData(int buffID, CauseType causeType, int causeName, LandscapeNames landscapeName,
                        AttribName statModified, float val, bool isBuff = true
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
    public class LandStateBuffData
    {
        public int buffID;
        public CauseType causeType;
        public int causeName;
        public LandscapeNames landscapeName;
        public CharStateName charStateName;
        public bool isImmunity;

        public LandStateBuffData(int buffID, CauseType causeType, int causeName, LandscapeNames landscapeName
            , CharStateName charStateName, bool isImmunity)
        {
            this.buffID = buffID;
            this.causeType = causeType;
            this.causeName = causeName;
            this.landscapeName = landscapeName;
            this.charStateName = charStateName;
            this.isImmunity = isImmunity;
        }
    }


    public class LandscapeController : MonoBehaviour
    {
        int buffIndex =-1;
        List<LandBuffData> allLandBuffs = new List<LandBuffData> ();
        List<LandStateBuffData> allLandNStateBuffs = new List<LandStateBuffData>(); 
        CharController charController;

        void Start()
        {
            LandscapeService.Instance.OnLandscapeEnter += OnLandscapeEnter;
            LandscapeService.Instance.OnLandscapeExit += OnLandscapeExit; 
            charController = GetComponent<CharController> ();
        }
        public int ApplyLandscapeCharStateBuff(CauseType causeType, int causeName, int causeBuyCharID,
            LandscapeNames landscapeName, CharStateName stateName, bool isImmunity = false)
        {
            int buffID = -1;

            if (LandscapeService.Instance.currLandscape != landscapeName) return -1;

            if (!isImmunity)
            {
                buffID = charController.charStateController.ApplyCharStateBuff(causeType, (int)causeName,
                    causeBuyCharID, stateName, TimeFrame.Infinity, 100);
            }
            else
            {
                buffID = charController.charStateController.ApplyImmunityBuff(causeType, (int)causeName,
                    causeBuyCharID, stateName, TimeFrame.Infinity, 100);
            }
            LandStateBuffData landNStateBuff = new LandStateBuffData(buffID, causeType, causeName
                , landscapeName, stateName, isImmunity);

            allLandNStateBuffs.Add(landNStateBuff);

            return buffID;
        }

        public void RemoveLandNStateBuff(int buffID)
        {
            int i = allLandNStateBuffs.FindIndex(t => t.buffID == buffID);
            LandStateBuffData landStateBuffData = allLandNStateBuffs[i];
            if (!landStateBuffData.isImmunity)
            {
                charController.charStateController.RemoveCharState(landStateBuffData.charStateName);
            }
            else
            {
                charController.charStateController.RemoveImmunityBuff(buffID);
            }
            allLandNStateBuffs.Remove(landStateBuffData);
        }


        public int ApplyLandscapeBuff(CauseType causeType, int causeName
            , LandscapeNames landScapeName, AttribName statName, float val)
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
            charController.ChangeAttrib(landBuff.causeType, landBuff.causeName
             , charController.charModel.charID, landBuff.statModified, landBuff.val);
        }

        public void RemoveBuffFX(LandBuffData landBuff)
        {
            // remove the statchange caused by landscape
            charController.ChangeAttrib(landBuff.causeType, landBuff.causeName
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