using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Linq;
using Quest;
using UnityEngine.SceneManagement;
using Interactables;
using Town;

namespace Combat
{
  

    [Serializable]
    public class OnSOCBuffData
    {
        public CauseType causeType;
        public int causeName;
        public int causeByCharID;
        public AttribName attribName;
        public float value;
        public TimeFrame timeFrame;
        public int netTime;
        public bool isBuff;
        public int socBuffIndex;
        public int buffIndex; 

        public OnSOCBuffData(CauseType causeType, int causeName
            , int causeByCharID, AttribName attribName, float value, TimeFrame timeFrame, int netTime, bool isBuff)
        {
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causeByCharID;
            this.attribName = attribName;
            this.value = value;
            this.timeFrame = timeFrame;
            this.netTime = netTime;
            this.isBuff = isBuff;
        }
    }


    [System.Serializable]
    public class BuffData
    {
        
        public int buffID; 
        public bool isBuff;   // true if BUFF and false if DEBUFF
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int buffedNetTime;
        public int buffCurrentTime;  
        public AttribModData attribModData;
        public TimeState timeState; 

        public BuffData(int buffID, bool isBuff, int startRoundNo, TimeFrame timeFrame,
            int buffedNetTime, AttribModData attribModData, TimeState timeState =  TimeState.None)
        {
            this.buffID = buffID;
            this.isBuff = isBuff;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.buffedNetTime = buffedNetTime;
            this.buffCurrentTime = 0;
            this.attribModData = attribModData;
            this.timeState = timeState; 
           
        }
    }
    public class PosBuffData
    {
        public List<int> allPos = new List<int>();
        public int buffID;
        public bool isBuff;   // true if BUFF and false if DEBUFF
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int castTime;
        public int buffCurrentTime;
        public AttribModData attribModVal;
        public TimeState timeState;

        public CauseType causeType;
        public int causeName;
        public int causeByCharID;
        public AttribName attribName;
        public float attribVal;


        public PosBuffData(CauseType causeType, int causeName, int causeByCharID, AttribName attribName, float val, List<int> allPos, int buffID, bool isBuff, int startRoundNo, TimeFrame timeFrame,
            int castTime, AttribModData attribModData, TimeState timeState = TimeState.None)
        {
            this.allPos = allPos.DeepClone();
            this.buffID = buffID;
            this.isBuff = isBuff;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.castTime = castTime;
            this.buffCurrentTime = 0;
            this.attribModVal = attribModData;
            this.timeState = timeState;

            this.causeType= causeType;  
            this.causeName = causeName;
            this.causeByCharID = causeByCharID;
            this.attribName = attribName;
            this.attribVal = val;
        }
    }
    public class BuffController : MonoBehaviour, ISaveable
    {
        //public List<BuffData> allBuffs = new List<BuffData>();  
        //public List<BuffData> allDayNightbuffs = new List<BuffData>(); 
        //public List<PosBuffData> allPosBuffs = new List<PosBuffData>();  
      
        //public List<OnSOCBuffData> allBuffOnSOC = new List<OnSOCBuffData>();

      
        //[SerializeField]List<string> buffStrs = new List<string>();
        //[SerializeField]List<string> deDuffStrs = new List<string>();
       
        //public int buffIndex = 0;
        //[SerializeField] int SOCBuffIndex = 0;

        public BuffModel buffModel; 
        CharController charController; // ref to char Controller 

        public ServicePath servicePath => throw new NotImplementedException();

        void Start()
        {
            charController = GetComponent<CharController>();            
            QuestEventService.Instance.OnEOQ += EOQTick;
            CalendarService.Instance.OnChangeTimeState += ToggleBuffsOnTimeStateChg;
            CalendarService.Instance.OnStartOfTheWeek += EOWTick; 
            SceneManager.sceneLoaded += OnSceneLoaded;
            CombatEventService.Instance.OnSOC += OnSOC;
        }
        private void OnDisable()
        {
            QuestEventService.Instance.OnEOQ -= EOQTick;
            CalendarService.Instance.OnChangeTimeState -= ToggleBuffsOnTimeStateChg;
            SceneManager.sceneLoaded -= OnSceneLoaded;
            CombatEventService.Instance.OnSOC -= OnSOC;
        }

        public void InitOnLoad(BuffModel buffModel)
        {
           this.buffModel = buffModel.DeepClone();     
        }
        public void Init()
        {
            int charID = charController.charModel.charID;   
            buffModel = new BuffModel(charID); //pass in char Id      
        }


        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
            {
                CombatEventService.Instance.OnEOR1 += RoundTick;
                CombatEventService.Instance.OnEOC += EOCTick;
                GridService.Instance.OnPosChange -= PosChgTick;
                GridService.Instance.OnPosChange += PosChgTick;
                CombatEventService.Instance.OnEOR1 += RoundPosTick;
                CombatEventService.Instance.OnEOC += EOCPosTick;
                QuestEventService.Instance.OnEOQ += EOQPosTick;
            }
            else
            {
                CombatEventService.Instance.OnEOR1 -= RoundTick;
                CombatEventService.Instance.OnEOC -= EOCTick;
                CombatEventService.Instance.OnEOR1 -= RoundPosTick;
                CombatEventService.Instance.OnEOC -= EOCPosTick;
                QuestEventService.Instance.OnEOQ -= EOQPosTick;
            }
        }
        #region SOC
        void OnSOC()
        {
            foreach (OnSOCBuffData socBuff in buffModel.allBuffOnSOC)
            {  
               int index =  ApplyBuff(socBuff.causeType, socBuff.causeName, socBuff.causeByCharID, socBuff.attribName
                    , socBuff.value, socBuff.timeFrame, socBuff.netTime, socBuff.isBuff); 
                socBuff.buffIndex= index;
            }
        }

        public int ApplyBuffOnSOC(CauseType causeType, int causeName, int causeByCharID
                        , AttribName attribName, float value, TimeFrame timeFrame, int netTime, bool isBuff)
        {
            OnSOCBuffData socBuffData = new OnSOCBuffData(causeType, causeName, causeByCharID
                        , attribName, value, timeFrame, netTime, isBuff);
            buffModel.SOCBuffIndex++;
            socBuffData.socBuffIndex = buffModel.SOCBuffIndex; 
            buffModel.allBuffOnSOC.Add(socBuffData);  
            return buffModel.SOCBuffIndex; 
        }
        public bool RemoveBuffOnSOC(int socIndex)
        {
            int index = buffModel.allBuffOnSOC.FindIndex(t=>t.socBuffIndex == socIndex);
            if (index == -1)
                return false;
            else
            {
                OnSOCBuffData socBuffData = buffModel.allBuffOnSOC[index];
                int buffIndx = socBuffData.buffIndex;
                RemoveBuff(buffIndx); 
                buffModel.allBuffOnSOC.RemoveAt(index);
            }
            return true; 
        }
        #endregion



        #region  APPLY_BUFFS 
        public int ApplyBuff(CauseType causeType, int causeName, int causeByCharID
                                , AttribName attribName, float value, TimeFrame timeFrame, int netTime, bool isBuff)
        {
            charController = GetComponent<CharController>();
            AttribModData attribModVal =  charController.ChangeAttrib( causeType,  causeName, causeByCharID
                                            ,  attribName,  value, true);
            int currRd = CombatEventService.Instance.currentRound;
            buffModel.buffIndex++;
            BuffData buffData = new BuffData(buffModel.buffIndex,isBuff, currRd, timeFrame, netTime,
                                                                    attribModVal);
            buffModel.allBuffs.Add(buffData);               
                return buffModel.buffIndex;         
        }

        public int ApplyDmgArmorByPercent(CauseType causeType, int causeName, int causebyCharID, AttribName attribName, 
            float percentVal, TimeFrame timeFrame, int castTime, bool isBuff)
        {
            AttribData attribData = charController.GetAttrib(attribName);
            float val = attribData.currValue*(1 + (percentVal / 100));

            int buffID = ApplyBuff(causeType, causeName, causebyCharID, attribName, val, timeFrame, castTime, isBuff); 
            return buffID;
        }
        public int ApplyExpByPercent(CauseType causeType, int causeName, int causebyCharID,
       float percentVal, TimeFrame timeFrame, int castTime, bool isBuff)
        {
            CharModel charModel = charController.charModel;
            int charID = charModel.charID; 
            float val = charModel.mainExp * (1 + (percentVal / 100));
            int turn = -1; 
            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)            
                turn = CombatService.Instance.currentTurn;

            AttribModData attribModVal = new AttribModData(turn, causeType, causeName
                , causebyCharID, charID, AttribName.None, val, charModel.mainExp); 
            int currRd = CombatEventService.Instance.currentRound;
            buffModel.buffIndex++;
            BuffData buffData = new BuffData(buffModel.buffIndex, isBuff, currRd, timeFrame, castTime,attribModVal);
            buffModel.allBuffs.Add(buffData);
            return buffModel.buffIndex;
        
        }
        public void IncrBuffCastTime(int buffID, int incrBy)
        {
            foreach (BuffData buff in buffModel.allBuffs)
            {
                if(buff.buffID == buffID)
                {
                    buff.buffedNetTime += incrBy; 
                }
            }
        }

        public int SetDmgORArmor2Max(CauseType causeType, int causeName, int causeByCharID
                                , AttribName attribName, TimeFrame timeFrame, int netTime)
        {
            float val = 0; 
            AttribName chgAttrib = AttribName.None;
            if(attribName == AttribName.armorMax)
            {
                AttribData attribDataMax = charController.GetAttrib(AttribName.armorMax);
                AttribData attribDataMin = charController.GetAttrib(AttribName.armorMin);
                val = attribDataMax.currValue - attribDataMin.currValue;
                chgAttrib = AttribName.armorMin; 
            }
            if (attribName == AttribName.dmgMax)
            {
                AttribData attribDataMax = charController.GetAttrib(AttribName.dmgMax);
                AttribData attribDataMin = charController.GetAttrib(AttribName.dmgMin);
                val = attribDataMax.currValue - attribDataMin.currValue;
                chgAttrib = AttribName.dmgMin;
            }

            int buffID = ApplyBuff(causeType, causeName, causeByCharID, chgAttrib, val, timeFrame, netTime, true); 

            return buffID; 
        }
        public int SetDmgORArmor2Min(CauseType causeType, int causeName, int causeByCharID
                                , AttribName attribName, TimeFrame timeFrame, int netTime)
        {
            float val = 0;
            AttribName chgAttrib = AttribName.None;

            if (attribName == AttribName.armorMin)
            {
                AttribData attribDataMax = charController.GetAttrib(AttribName.armorMax);
                AttribData attribDataMin = charController.GetAttrib(AttribName.armorMin);
                val = attribDataMax.currValue - attribDataMin.currValue;
                chgAttrib = AttribName.armorMax;
            }else if (attribName == AttribName.dmgMin)
            {
                AttribData attribDataMax = charController.GetAttrib(AttribName.dmgMax);
                AttribData attribDataMin = charController.GetAttrib(AttribName.dmgMin);
                val = attribDataMax.currValue - attribDataMin.currValue;
                chgAttrib = AttribName.dmgMax;
            }
            else
            {
                Debug.LogError(" wrong ref"); 
                return-1;
            }

            int buffID = ApplyBuff(causeType, causeName, causeByCharID, chgAttrib, -val, timeFrame, netTime, false);

            return buffID;
        }
        #endregion

        #region REMOVE BUFFS 
        public bool RemoveBuff(int buffID)   // to be revised
        {
            BuffData buffData = null; 
            int index = buffModel.allBuffs.FindIndex(t => t.buffID == buffID);
            if (index == -1)
            {
                index = buffModel.allDayNightbuffs.FindIndex(t => t.buffID == buffID); 
                if(index == -1)
                {
                    index = buffModel.allPosBuffs.FindIndex(t => t.buffID == buffID);
                    if (index == -1)
                        return false;
                    else
                    {
                        PosBuffData posBuffData= buffModel.allPosBuffs[index];
                        buffModel.allPosBuffs.Remove(posBuffData);
                        return true;
                    }
                }
                else // remove day buff
                {
                    buffData = buffModel.allDayNightbuffs[index];
                    buffModel.allDayNightbuffs.Remove(buffData);
                    return true; 
                } 
            }
            else
            {
                buffData = buffModel.allBuffs[index];
                RemoveBuffData(buffData);
                return true; 
            }                
        }
        public void RemoveBuffData(BuffData buffData)
        {        
            if(buffData.attribModData.attribModified != AttribName.None)
            {
                charController.ChangeAttrib(buffData.attribModData.causeType,
                                        buffData.attribModData.causeName, buffData.attribModData.causeByCharID
                                        , buffData.attribModData.attribModified, -buffData.attribModData.chgVal, true);
            }
            else // attrib NOne case is for Exp 
            {
                charController.charModel.mainExp = (int)buffData.attribModData.baseVal; 
            }      
            buffModel.allBuffs.Remove(buffData);
        }
        #endregion

        public List<BuffData> GetBuffDebuffData()
        {
            List<BuffData> allBufftemp=new List<BuffData>();
            allBufftemp = buffModel.allBuffs.DeepClone();
            return allBufftemp; 
        }

        public List<string> GetBuffList()
        {
            //foreach (BuffData buffData in allBuffs)
            //{
            //    string str = ""; 
            //    if (buffData.isBuff)
            //    {
            //        // +1 Morale from Skills, 2 rds
            //        str= buffData.attribModData.chgVal 
            //    }
            //        buffStrs.Add();
            //}
            return buffModel.buffStrs;            
        }
        public List<string> GetDeBuffList()
        {
            //foreach (BuffData buffData in allBuffs)
            //{
            //    if (!buffData.isBuff)
            //        deDuffStrs.Add(buffData.directString); 
            //}
            return buffModel.deDuffStrs;          
        }
        public void RoundTick(int roundNo)
        {
            foreach (BuffData buffData in buffModel.allBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (buffData.buffCurrentTime >= buffData.buffedNetTime)
                    {
                        RemoveBuffData(buffData);
                    }
                    buffData.buffCurrentTime++;
                }
            }
        }
        public void EOCTick()
        {
            foreach (BuffData buffData in buffModel.allBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveBuffData(buffData);
                }
            }
        }
        public void EOQTick()
        {
            foreach (BuffData buffData in buffModel.allBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfQuest)
                {
                    RemoveBuffData(buffData);
                }
            }
        }
        public void EOWTick(WeekEventsName weekName, int weekCounter)
        {
            foreach (BuffData buffData in buffModel.allBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfWeek)
                {
                    RemoveBuffData(buffData);
                }
            }
        }
        #region POS BUFFS

        public int ApplyPosBuff(List<int> allPos, CauseType causeType, int causeName, int causeByCharID
                                , AttribName attribName, float value, TimeFrame timeFrame, int castTime, bool isBuff)
        {
            AttribModData attribModVal = null;
            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
            {
                DynamicPosData dyna = GridService.Instance.GetDyna4GO(gameObject);            

                if (allPos.Any(t => t == dyna.currentPos)) // apply if in pos // attribModVal == null will signal not applied
                    attribModVal = charController.ChangeAttrib(causeType, causeName, causeByCharID
                                                                    , attribName, value, true);
            }   

            int currRd = CombatEventService.Instance.currentRound;
            buffModel.buffIndex++;
            PosBuffData posBuffData = new PosBuffData(causeType, causeName, causeByCharID ,attribName, value,
                                        allPos, buffModel.buffIndex, isBuff, currRd, timeFrame, castTime,attribModVal);
            buffModel.allPosBuffs.Add(posBuffData);
            return buffModel.buffIndex;
        }
        #endregion

        void PosChgTick(DynamicPosData dyna , int pos)
        {
            if(charController.charModel.charID == dyna.charGO.GetComponent<CharController>().charModel.charID)
            {
                foreach (PosBuffData posBuffData in buffModel.allPosBuffs)
                {
                    if(posBuffData.allPos.Any(t=>t == pos))
                    {
                        if(posBuffData.attribModVal== null)
                        {
                            AttribModData 
                            attribModVal = charController.ChangeAttrib(posBuffData.causeType, posBuffData.causeName
                            , posBuffData.causeByCharID, posBuffData.attribName, posBuffData.attribVal, true);

                            posBuffData.attribModVal= attribModVal;                     
                        }
                    }
                }
            }
        }

        void EOCPosTick()
        {
            foreach (PosBuffData buffData in buffModel.allPosBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemovePosBuff(buffData.buffID);
                }
            }
        }
        public void EOQPosTick()
        {
            foreach (PosBuffData buffData in buffModel.allPosBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfQuest)
                {
                    RemovePosBuff(buffData.buffID);
                }
            }
        }
        public void RoundPosTick(int roundNo)
        {
            foreach (PosBuffData buffData in buffModel.allPosBuffs.ToList())
            {
                if (buffData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (buffData.buffCurrentTime >= buffData.castTime)
                    {
                        RemovePosBuff(buffData.buffID);
                    }
                    buffData.buffCurrentTime++;
                }
            }
        }

        public void RemovePosBuff(int buffID) 
        {
            foreach (PosBuffData buffData in buffModel.allPosBuffs.ToList())
            {
                if (buffData.buffID == buffID)
                    buffModel.allPosBuffs.Remove(buffData); 
            }
        }


        #region DAY BUFF MGMT
        public int ApplyNInitBuffOnDayNNight(CauseType causeType, int causeName, int causeByCharID
                               , AttribName statName, float value, TimeFrame timeFrame, int netTime
                                , bool isBuff, TimeState timeState)
        {

            AttribModData attribModVal = new AttribModData();

            if (CalendarService.Instance.calendarModel.currtimeState == TimeState.Day) // FOR DAY CORRECTION
            {
                attribModVal = charController.ChangeAttrib(causeType, causeName, causeByCharID
                                                        , statName, value, true);
            }
            if(CalendarService.Instance.calendarModel.currtimeState == TimeState.Night) // FOR NIGHT CORRECTION
            {
                attribModVal = charController.ChangeAttrib(causeType, causeName, causeByCharID
                                                        , statName, -value, true);  
            }
            int currRd = CombatEventService.Instance.currentRound;
            buffModel.buffIndex++;
            BuffData buffData = new BuffData(buffModel.buffIndex, isBuff, currRd, timeFrame, netTime,
                                                                  attribModVal, timeState);

            // allBuffs.Add(buffData);
            buffModel.allDayNightbuffs.Add(buffData);
            return buffModel.buffIndex;
        }

        void ToggleBuffsOnTimeStateChg(TimeState timeState) // ON start of the day
        {
            foreach (BuffData buffData in buffModel.allDayNightbuffs)
            {
                if (buffData.timeState == timeState)
                {  // APPLY temporarily
                    AttribModData attribModData = buffData.attribModData;
                    charController.ChangeAttrib(attribModData.causeType, attribModData.causeName
                    , attribModData.causeByCharID, attribModData.attribModified, attribModData.modCurrVal, true);
                }
                else
                {  // REMOVE temporarily
                    AttribModData attribModData = buffData.attribModData;
                    charController.ChangeAttrib(attribModData.causeType, attribModData.causeName
                    , attribModData.causeByCharID, attribModData.attribModified, -attribModData.modCurrVal, true);
                }
            }
        }
        #endregion


        #region BUFF ALL RES 

        public List<int> BuffAllRes(CauseType causeType, int causeName, int causeByCharID
                                ,  float value, TimeFrame timeFrame, int netTime, bool isBuff)
        {
            List<int> allbuffID = new List<int>();
            int buffID = ApplyBuff(causeType, causeName, causeByCharID, AttribName.waterRes, value, timeFrame, netTime, isBuff);
            allbuffID.Add(buffID);  
            buffID = ApplyBuff(causeType, causeName, causeByCharID, AttribName.fireRes, value, timeFrame, netTime, isBuff);
            allbuffID.Add(buffID);
            buffID = ApplyBuff(causeType, causeName, causeByCharID, AttribName.earthRes, value, timeFrame, netTime, isBuff);
            allbuffID.Add(buffID);    
            buffID = ApplyBuff(causeType, causeName, causeByCharID, AttribName.lightRes, value, timeFrame, netTime, isBuff);
            allbuffID.Add(buffID);
            buffID = ApplyBuff(causeType, causeName, causeByCharID, AttribName.darkRes, value, timeFrame, netTime, isBuff);
            allbuffID.Add(buffID);
            buffID = ApplyBuff(causeType, causeName, causeByCharID, AttribName.airRes, value, timeFrame, netTime, isBuff);
            allbuffID.Add(buffID);

            return allbuffID; 
        }
        public List<int> BuffElementalRes(CauseType causeType, int causeName, int causeByCharID
                                , float value, TimeFrame timeFrame, int netTime, bool isBuff)
        {
            List<int> allbuffID = new List<int>();
            int buffID = ApplyBuff(causeType, causeName, causeByCharID, AttribName.waterRes, value, timeFrame, netTime, isBuff);
            allbuffID.Add(buffID);  
            buffID =ApplyBuff(causeType, causeName, causeByCharID, AttribName.fireRes, value, timeFrame, netTime, isBuff);
            allbuffID.Add(buffID);   
            buffID = ApplyBuff(causeType, causeName, causeByCharID, AttribName.earthRes, value, timeFrame, netTime, isBuff);
            allbuffID.Add(buffID);
            buffID= ApplyBuff(causeType, causeName, causeByCharID, AttribName.airRes, value, timeFrame, netTime, isBuff);
            allbuffID.Add(buffID);
            

            return allbuffID;   
        }

        public void SaveState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath); 
            string buffPath = path + charController.charModel.charName;
            string buffJSON = JsonUtility.ToJson(buffModel);

            // save buff model

        }

        public void LoadState()
        {
            // load from buff folder from given charName path

        }

        public void ClearState()
        {
          // clear only specific file in the given path
        }
        #endregion

    }
}
