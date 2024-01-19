using Combat;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
 
    public class LandBuffData
    {
        public int buffID;        
        public CauseType causeType;
        public int causeName;
        public LandscapeNames landscapeName;
       // public StatName statModified;
        public AttribName attribModified;        
        public float val;
        public bool isBuff;
        public float minVal; 
        public float maxVal;
        public bool isApplied; 

        public LandBuffData(int buffID, CauseType causeType, int causeName, LandscapeNames landscapeName,
                        AttribName attribModified, float val, bool isBuff = true
                        , float minVal =0, float maxVal=0, bool isApplied = false)
        {
            this.buffID = buffID;
            this.causeType = causeType;
            this.causeName = causeName;
            this.landscapeName = landscapeName;          
            this.attribModified = attribModified;
            this.val = val;
            this.isBuff = isBuff;
            this.minVal = minVal;
            this.maxVal = maxVal;
            this.isApplied = isApplied; 
        }
        //public LandBuffData(int buffID, CauseType causeType, int causeName, LandscapeNames landscapeName,
        //              StatName statModified, float val, bool isBuff = true
        //              , float minVal = 0, float maxVal = 0, bool isApplied = false)
        //{
        //    this.buffID = buffID;
        //    this.causeType = causeType;
        //    this.causeName = causeName;
        //    this.landscapeName = landscapeName;
        //    this.statModified = statModified;
        //    this.val = val;
        //    this.isBuff = isBuff;
        //    this.minVal = minVal;
        //    this.maxVal = maxVal;
        //    this.isApplied = isApplied;
        //}
    }
    public class LandCharStateBuffData
    {
        public int buffID;
        public CauseType causeType;
        public int causeName;
        public LandscapeNames landscapeName;
        public CharStateName charStateName;
        public bool isImmunity;

        public LandCharStateBuffData(int buffID, CauseType causeType, int causeName, LandscapeNames landscapeName
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
        /*
         PURPOSE IS: to act as buff controller on landscape enter and exit 

        reduce code complexities to buff controller 
        provides its own buffId s that is independent of buff controller
         */
        int buffIDStat =-1;
        [SerializeField] List<LandBuffData> allLandBuffs = new List<LandBuffData> ();
        [SerializeField] List<LandCharStateBuffData> allLandNStateBuffs = new List<LandCharStateBuffData>(); 
        
        CharController charController;

        [Header("Landscape Mod")]
        [SerializeField] int currHungerMod;
        [SerializeField] int currThirstMod;
        int buffIDCS = -1;

        [Header("Land Model")]
        public List<LandModel> allLandModel = new List<LandModel>(); 

        [Header("Land Base")]
        public List<LandscapeBase> allLandBase = new List<LandscapeBase>();

        void Start()
        {
        
            LandscapeService.Instance.OnLandscapeEnter += OnLandscapeEnter;
            LandscapeService.Instance.OnLandscapeExit += OnLandscapeExit;
            LandscapeService.Instance.OnLandscapeEnter += OnEnterHungerNThirstModChg;
            LandscapeService.Instance.OnLandscapeExit += OnExitHungerNThirstModChg;

            LandscapeService.Instance.OnLandscapeEnter += OnLandEnterCharStateBuff;
            LandscapeService.Instance.OnLandscapeExit += OnLandExitCharStateBuff;

            charController = GetComponent<CharController>();
        }
        private void OnDisable()
        {
            LandscapeService.Instance.OnLandscapeEnter -= OnLandscapeEnter;
            LandscapeService.Instance.OnLandscapeExit -= OnLandscapeExit;
            LandscapeService.Instance.OnLandscapeEnter -= OnLandscapeEnter;
            LandscapeService.Instance.OnLandscapeExit -= OnLandscapeExit;

            LandscapeService.Instance.OnLandscapeEnter -= OnLandscapeEnter;
            LandscapeService.Instance.OnLandscapeExit -= OnLandscapeExit;
        }
        public void InitLandController(AllLandscapeSO allLandSO)
        { 
            foreach (LandscapeSO landSO in allLandSO.alllandscapeSO)
            {
                LandModel landModel = new LandModel(landSO);
                allLandModel.Add(landModel);
            }
            InitLandBase(); 
        }
        void InitLandBase()
        {
            foreach (LandModel landModel in allLandModel)
            {
                LandscapeBase landbase =
                        LandscapeService.Instance.landFactory.GetNewLandscape(landModel.landscapeName);                
                if (landbase != null)
                {
                    landbase.OnLandscapeInit(landModel);
                    allLandBase.Add(landbase);  
                }
            }
        }

        #region HUNGER AND THIRST MOD 
        void OnEnterHungerNThirstModChg(LandscapeNames land)
         {
            // get SO 
            LandscapeSO landSO = LandscapeService.Instance.allLandSO.GetLandSO(land);
            currHungerMod = landSO.hungerMod;
            currThirstMod= landSO.thirstMod;    
            foreach (CharController c in CharService.Instance.allCharsInPartyLocked)
            {
                c.charModel.hungerMod += currHungerMod;
                c.charModel.thirstMod += currThirstMod;
            }
         }
        void OnExitHungerNThirstModChg(LandscapeNames land)
        {
            foreach (CharController c in CharService.Instance.allCharsInPartyLocked)
            {
                c.charModel.hungerMod -= currHungerMod;
                c.charModel.thirstMod -= currThirstMod;
            }
        }

        #endregion
        public int ApplyNInitLandCharStateBuff(CauseType causeType, int causeName, int causeBuyCharID,
            LandscapeNames landscapeName, CharStateName stateName, bool isImmunity = false)
        {
            // CANNOT INIT APPLY A BUFF WHEN NOT IN Landscape
            buffIDCS++; 
            LandCharStateBuffData landNStateBuff = new LandCharStateBuffData(buffIDCS, causeType, causeName
                , landscapeName, stateName, isImmunity);

            allLandNStateBuffs.Add(landNStateBuff);

            if (landscapeName == LandscapeService.Instance.currLandscape)
                ApplyLandCharStateFX(landNStateBuff); 

            return buffIDCS;  // buff Ids used as ref by the source
        }

        public void RemoveLandNStateBuff(int buffID)
        {
            int i = allLandNStateBuffs.FindIndex(t => t.buffID == buffID);
            LandCharStateBuffData landStateBuffData = allLandNStateBuffs[i];
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
    

        // use to toggle on and off between land scape enter and exit
        void OnLandEnterCharStateBuff(LandscapeNames land)    // TOGGLES
        {
            foreach (LandCharStateBuffData buff in allLandNStateBuffs)
            {
                if (buff.landscapeName == land)
                {
                    ApplyLandCharStateFX(buff); 
                }
            }
        }

        void ApplyLandCharStateFX(LandCharStateBuffData buff)
        {
            if (!buff.isImmunity)
            {
                charController.charStateController.ApplyCharStateBuff(buff.causeType, (int)buff.landscapeName,
                   1, buff.charStateName, TimeFrame.Infinity, 1);
            }
            else
            {
                charController.charStateController.ApplyImmunityBuff(buff.causeType, (int)buff.landscapeName,
                   1, buff.charStateName, TimeFrame.Infinity, 1);
            }
        }

        void OnLandExitCharStateBuff(LandscapeNames land)   // TOGGLES
        {
            foreach (LandCharStateBuffData buff in allLandNStateBuffs)
            {
                if (buff.landscapeName == land)
                {
                    if (!buff.isImmunity)
                    {
                        charController.charStateController.RemoveCharState(buff.charStateName);
                    }
                    else
                    {
                        charController.charStateController.RemoveImmunityByCharState(buff.charStateName);
                    }
                }
            }
        }



        public int ApplyLandscapeBuff(CauseType causeType, int causeName
                                    , LandscapeNames landScapeName, AttribName statName, float val)
        {
            buffIDStat++;
            LandBuffData landBuffData = new LandBuffData(buffIDStat, causeType, causeName,
                landScapeName, statName, val);

            allLandBuffs.Add(landBuffData); 
            
            if(LandscapeService.Instance.currLandscape == landScapeName)
                ApplyLandBuffFX(landBuffData);  
            
            return buffIDStat;
        }

        public void RemoveBuff(int buffIDStat)   // PERMANENT REMOVE FROM TOGGLE CYCLE LAND SCAPE BUFF
        {
            int i = allLandBuffs.FindIndex(t => t.buffID == buffIDStat);
            LandBuffData landBuffData = allLandBuffs[i];

            if(landBuffData.isApplied)
                RemoveBuffFX(landBuffData);
            allLandBuffs.Remove(landBuffData);
        }

        void ApplyLandBuffFX(LandBuffData landBuff)  // TOGGLE ON LAND ENTER
        {
            charController.ChangeAttrib(landBuff.causeType, landBuff.causeName
             , charController.charModel.charID, landBuff.attribModified, landBuff.val);
        }

        public void RemoveBuffFX(LandBuffData landBuff)
        {
            // remove the statchange caused by landscape
            charController.ChangeAttrib(landBuff.causeType, landBuff.causeName
                , charController.charModel.charID, landBuff.attribModified, -landBuff.val); 

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