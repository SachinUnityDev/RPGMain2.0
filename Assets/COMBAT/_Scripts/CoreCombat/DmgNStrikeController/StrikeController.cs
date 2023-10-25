using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;


namespace Combat
{
    [System.Serializable]
    public class StatChgData
    {
        public AttribName statName;
        public float value;
        public StatChgData(AttribName statName, float value)
        {
            this.statName = statName;
            this.value = value;
        }
    }

    public class StrikeController : MonoBehaviour
    {

        CharController charController;
        List<DynamicPosData> otherTargetDynas;
        public StrikerModel strikerModel;

        [Header("Thorns Damage related")]
        public int thornID = -1;
        float dmgMin, dmgMax;
        private void Start()
        {
            charController = GetComponent<CharController>();
            otherTargetDynas = new List<DynamicPosData>();
            CombatEventService.Instance.OnDamageApplied += OnDmgDeliveredTick;
            CombatEventService.Instance.OnEOR1 += RoundTick;
            CombatEventService.Instance.OnEOC += EOCTick; 
        }

        /// <summary>
        /// to be checked only when move skill are used 
        /// Whenever you execute a move skill you get a haste check if its positive then 
        ///  then you get a chance to use another skill..(including move skill)
        /// 
        /// </summary>
        void HasteCheck()
        {

        }

        public void Init()
        {
            strikerModel = new StrikerModel();
           
        }

        public bool FocusCheck()// Magical only .. 
        {
            // get focus statChance Data of performers focus 
            // depending on that decide ..TO be decided 

            float focusVal = charController.GetAttrib(AttribName.focus).currValue;
            float focusChance = 100f - charController.GetStatChance(AttribName.focus, focusVal);

            if (focusVal == 0)
            {
                // GOT CONFUSED .. to be put in HERE ..                
                int buffId = charController.charStateController.ApplyCharStateBuff(CauseType.CharState, (int)CharStateName.Confused
                   , charController.charModel.charID, CharStateName.Confused, TimeFrame.Infinity, -1);


                return false;  // MIsfire ..hit the wrong target .. 
            }
            else
            {
                return focusChance.GetChance();
            }
        }

        public void MisFireApply()
        {
            // SKIP AKILL APPLY DMG 
            SkillController1 skillController = SkillService.Instance.currSkillController;

            StrikeTargetNos strikeNos = skillController.allSkillBases.Find(t => t.skillName
                                                == SkillService.Instance.currSkillName).strikeNos;
            if (strikeNos == StrikeTargetNos.Single)
            {
                int netTargetCount = CombatService.Instance.mainTargetDynas.Count;
                if (netTargetCount > 1)
                {
                    CombatService.Instance.mainTargetDynas.Remove(SkillService.Instance.currentTargetDyna);
                    int random = UnityEngine.Random.Range(0, netTargetCount - 1);
                    SkillService.Instance.currentTargetDyna = CombatService.Instance.mainTargetDynas[random];
                }
                else
                {
                    ReduceDmgPercent();
                    SkillService.Instance.PostSkillApply += RevertDamageRange;
                }
            }
            else
            {
                ReduceDmgPercent();
                SkillService.Instance.PostSkillApply += RevertDamageRange;
            }
        }

        void ReduceDmgPercent()
        {
            int charID = charController.charModel.charID;
            AttribData dmgMin1 = charController.GetAttrib(AttribName.dmgMin);
            AttribData dmgMax1 = charController.GetAttrib(AttribName.dmgMax);
            dmgMin = dmgMin1.currValue;
            dmgMax = dmgMax1.currValue;
            float chgMin = 0.2f * this.dmgMin;
            float chgMax = 0.2f * dmgMax;

            charController.ChangeAttrib(CauseType.StatChecks, (int)StatChecks.FocusCheck, charID
                , AttribName.dmgMin, chgMin);
            charController.ChangeAttrib(CauseType.StatChecks, (int)StatChecks.FocusCheck, charID
                , AttribName.dmgMin, chgMax);

        }
        void RevertDamageRange()
        {
            // whats is this 

            charController.GetAttrib(AttribName.dmgMin).currValue = dmgMin;
            charController.GetAttrib(AttribName.dmgMin).currValue = dmgMax;
        }

        public bool AccuracyCheck()// Physical 
        {
            float accVal = charController.GetAttrib(AttribName.acc).currValue;
            float accChance = charController.GetStatChance(AttribName.acc, accVal);

            if (accVal == 0)
            { // self inflicted

                int buffId = charController.charStateController.ApplyCharStateBuff(CauseType.CharState, (int)CharStateName.Confused
                     , charController.charModel.charID, CharStateName.Blinded, TimeFrame.Infinity, -1);
                return false;// miss the target .. i.e not going to hit/FX anyone.. 
            }
            else
            {
                return accChance.GetChance();
            }
        }


    #region THORNS RELATED

        public int AddThornsBuff(DamageType damageType, float thornsMin, float thornsMax, TimeFrame timeframe, int castTime)
        {
            int currRd = CombatService.Instance.currentRound;
            thornID = strikerModel.allThornsData.Count + 1; 
            ThornBuffData thornBuffData = new ThornBuffData(thornID, damageType, thornsMin, thornsMax, timeframe, castTime);
            
            strikerModel.allThornsData.Add(thornBuffData);
            
            return thornID;
        }
        
        public void RemoveThornsFx(int thornID)
        {
            strikerModel.RemoveThornDamage(thornID);
        }
        public void RoundTick(int roundNo)
        {
            foreach (ThornBuffData thornBuffData in strikerModel.allThornsData.ToList())
            {
                if (thornBuffData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (thornBuffData.currentTime >= thornBuffData.castTime)
                    {
                        RemoveThornsFx(thornBuffData.thornID);
                    }
                    thornBuffData.currentTime++;
                }
            }
        }
        public void EOCTick()
        {
            foreach (ThornBuffData thornBuffData in strikerModel.allThornsData.ToList())
            {
                if (thornBuffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    strikerModel.allThornsData.Clear();
                }
            }
        }
        void OnDmgDeliveredTick(DmgAppliedData dmgAppliedData)
        {
            foreach (ThornBuffData thornData in strikerModel.allThornsData)
            {   
                int charLvl = this.GetComponent<CharController>().charModel.charLvl;
                float dmgPercentValue = UnityEngine.Random.Range(thornData.thornsMin,thornData.thornsMax)*(0.6f + charLvl/6);
                if (dmgPercentValue > 0)
                dmgAppliedData.striker.GetComponent<DamageController>()
                    .ApplyDamage(charController, CauseType.ThornsAttack, -1, thornData.damageType, dmgPercentValue, false);               
            }
        }

        #endregion

        #region RETALIATE

        public int AddRetailiateBuff(DamageType damageType, float dmgVal, TimeFrame timeframe, int castTime)
        {
            int currRd = CombatService.Instance.currentRound;
            thornID = strikerModel.allThornsData.Count + 1;
            RetaliateBuffData retaliateBuffData = new RetaliateBuffData(thornID, damageType, dmgVal, timeframe, castTime);

            strikerModel.allRetaliateData.Add(retaliateBuffData);

            return thornID;
        }

        public void RemoveRetaliateFx(int thornID)
        {
            strikerModel.RemoveThornDamage(thornID);
        }
        public void RoundTickRetaliate(int roundNo)
        {
            foreach (RetaliateBuffData retaliateBuffData in strikerModel.allRetaliateData.ToList())
            {
                if (retaliateBuffData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (retaliateBuffData.currentTime >= retaliateBuffData.castTime)
                    {
                        RemoveThornsFx(retaliateBuffData.retaliateID);
                    }
                    retaliateBuffData.currentTime++;
                }
            }
        }
        public void EOCTickRetaliate()
        {
            foreach (RetaliateBuffData retaliateBuffData in strikerModel.allRetaliateData.ToList())
            {
                if (retaliateBuffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    strikerModel.allRetaliateData.Clear();
                }
            }
        }
        void OnDmgDeliveredTickRetaliate(DmgAppliedData dmgAppliedData)
        {
            foreach (RetaliateBuffData retaliateBuffData in strikerModel.allRetaliateData)
            {
                int charLvl = this.GetComponent<CharController>().charModel.charLvl;
                //float dmgPercentValue = (retaliateBuffData.retaliateMax - retaliateBuffData.retaliateMin) * (0.6f + charLvl / 6);
                //if (dmgPercentValue > 0)
                    //dmgAppliedData.striker.GetComponent<DamageController>()
                    //    .ApplyDamage(charController, CauseType.ThornsAttack, -1, retaliateBuffData.damageType, dmgPercentValue, false);
            }
        }




        #endregion
        #region DAMAGE BUFF ALTERER

        public List<DmgBuffData> allDmgBuffData = new List<DmgBuffData>();

        int dmgBuffID = 0;
        public int ApplyDmgAltBuff(float valPercent, CauseType causeType, int causeName, int causeByCharID,
             TimeFrame timeFrame, int netTime, bool isBuff,
            AttackType attackType = AttackType.None, DamageType dmgType = DamageType.None,
            CultureType cultType = CultureType.None, RaceType raceType = RaceType.None)
        {
            dmgBuffID = allDmgBuffData.Count + 1; 

            DmgAltData dmgAltData = new DmgAltData( valPercent, attackType, dmgType, cultType, raceType);
            int startRoundNo = CombatService.Instance.currentRound;
          
            DmgBuffData dmgBuffData = new DmgBuffData(dmgBuffID, isBuff, startRoundNo, timeFrame
                            , netTime, dmgAltData);

            allDmgBuffData.Add(dmgBuffData);
            return dmgBuffID;
        }
        public void EOCTickDmgBuff()
        {
            foreach (DmgBuffData dmgBuffData in allDmgBuffData.ToList())
            {
                if (dmgBuffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveDmgBuffData(dmgBuffData);
                }
            }
        }

        public void EORTick(int roundNo)  // to be completed
        {
            foreach (DmgBuffData dmgBuffData in allDmgBuffData.ToList())
            {
                if (dmgBuffData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (dmgBuffData.buffCurrentTime >= dmgBuffData.buffedNetTime)
                    {
                        RemoveDmgBuffData(dmgBuffData);
                    }
                    dmgBuffData.buffCurrentTime++;
                }
            }
        }

        public void RemoveDmgBuffData(DmgBuffData dmgBuffData)
        {
            allDmgBuffData.Remove(dmgBuffData);
        }
        public bool RemoveDmgBuff(int dmgBuffID)
        {
            int index = allDmgBuffData.FindIndex(t => t.dmgBuffID == dmgBuffID);
            if (index == -1) return false;
            DmgBuffData dmgBuffData = allDmgBuffData[index];
            RemoveDmgBuffData(dmgBuffData);
            return true;
        }

        public float GetDmgAlt(CharModel targetModel, AttackType attackType = AttackType.None
            , DamageType damageType = DamageType.None)
        {
            // 20% physical attack against beastmen            
            foreach (DmgBuffData dmgBuffData in allDmgBuffData.ToList())
            {
                DmgAltData dmgAltData = dmgBuffData.altData;
                if (dmgAltData.damageType != DamageType.None && dmgAltData.damageType == damageType)
                {
                    float val = 0;
                    if (dmgAltData.raceType != RaceType.None
                        && dmgAltData.raceType == targetModel.raceType
                                && dmgAltData.cultType != CultureType.None
                                && dmgAltData.cultType == targetModel.cultType)
                    {

                        val = dmgAltData.valPercent; // COMBO RACE AND CULT

                    }
                    else   // NOT A COMBO OF RACE AND CULT
                    {
                        if (dmgAltData.raceType != RaceType.None
                                  && dmgAltData.raceType == targetModel.raceType)
                        {
                            val = dmgAltData.valPercent;
                        }
                        if (dmgAltData.cultType != CultureType.None
                                        && dmgAltData.cultType == targetModel.cultType)
                        {
                            val = dmgAltData.valPercent;
                        }
                    }
                    return val; 
                }
                else if (dmgAltData.attackType != AttackType.None && dmgAltData.attackType == attackType)
                {
                    float val = 0;
                    if (dmgAltData.raceType != RaceType.None
                        && dmgAltData.raceType == targetModel.raceType
                                && dmgAltData.cultType != CultureType.None
                                && dmgAltData.cultType == targetModel.cultType)
                    {

                        val = dmgAltData.valPercent; // COMBO RACE AND CULT

                    }
                    else   // NOT A COMBO OF RACE AND CULT
                    {
                        if (dmgAltData.raceType != RaceType.None
                                  && dmgAltData.raceType == targetModel.raceType)
                        {
                            val = dmgAltData.valPercent;
                        }
                        if (dmgAltData.cultType != CultureType.None
                                        && dmgAltData.cultType == targetModel.cultType)
                        {
                            val = dmgAltData.valPercent;
                        }
                    }
                    return val;
                }
            }
            return 0f;
        }
        #endregion
    }

    public class DmgBuffData
    {
        public int dmgBuffID;
        public bool isBuff;   // true if BUFF and false if DEBUFF
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int buffedNetTime;
        public int buffCurrentTime;
        public DmgAltData altData;  // contains value for the buff        

        public DmgBuffData(int dmgBuffID, bool isBuff, int startRoundNo, TimeFrame timeFrame
                            , int buffedNetTime, DmgAltData altData)
        {
            this.dmgBuffID = dmgBuffID;
            this.isBuff = isBuff;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.buffedNetTime = buffedNetTime;
            this.buffCurrentTime = 0;// time counter for the dmgBuff
            this.altData = altData;
        }
    }

    public class DmgAltData
    {
        public AttackType attackType = AttackType.None;
        public DamageType damageType = DamageType.None;
        public CultureType cultType = CultureType.None;
        public RaceType raceType = RaceType.None;
        public float valPercent = 0f;

        public DmgAltData( float valPercent, AttackType attackType, DamageType damageType, CultureType cultType, RaceType raceType)
        {
           
            this.attackType = attackType;
            this.damageType = damageType;
            this.cultType = cultType;
            this.raceType = raceType;
            this.valPercent = valPercent;
        }

    }


}




