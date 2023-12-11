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


        [Header(" retaliate Skill")]
        public int retaliateID = -1;

        private void Start()
        {
            charController = GetComponent<CharController>();
            otherTargetDynas = new List<DynamicPosData>();
            CombatEventService.Instance.OnDamageApplied += OnDmgDeliveredTick;
            CombatEventService.Instance.OnEOR1 += RoundTick;
            CombatEventService.Instance.OnEOC += EOCTick;
            // retaliate Events
            CombatEventService.Instance.OnDamageApplied += OnDmgDeliveredTickRetaliate;
            CombatEventService.Instance.OnEOR1 += RoundTickRetaliate;
            CombatEventService.Instance.OnEOC += EOCTickRetaliate;

        }

        /// <summary>
        /// to be checked only when move skill are used 
        /// Whenever you execute a move skill you get a haste check if its positive then 
        ///  then you get a chance to use another skill..(including move skill)
        /// 
        /// </summary>

        public void Init()
        {
            strikerModel = new StrikerModel();

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
                float dmgPercentValue = UnityEngine.Random.Range(thornData.thornsMin, thornData.thornsMax) * (0.6f + charLvl / 6);
                if (dmgPercentValue > 0)
                    dmgAppliedData.striker.GetComponent<DamageController>()
                        .ApplyDamage(charController, CauseType.ThornsAttack, -1, thornData.damageType, dmgPercentValue);
            }
        }

        #endregion

        #region RETALIATE

        public int AddRetailiateBuff(CauseType causeType, int causeName, TimeFrame timeFrame, int castTime)
        {
            int currRd = CombatService.Instance.currentRound;
            retaliateID = strikerModel.allRetaliateData.Count + 1;
            RetaliateBuffData retaliateBuffData = new RetaliateBuffData(retaliateID, causeType, causeName, timeFrame, castTime);
            strikerModel.allRetaliateData.Add(retaliateBuffData);
            return retaliateID;
        }

        public void ApplyRetaliate(CharController targetController)
        {
            SkillController1 skillController = GetComponent<SkillController1>();
            // get retaliate skills 
            int index =
                    skillController.allSkillBases.FindIndex(t => t.skillModel.skillType == SkillTypeCombat.Retaliate);
            if (index == -1) return;

            SkillBase skillBase = skillController.allSkillBases[index];

            skillBase.targetGO = targetController.gameObject;
            skillBase.targetController = targetController;
            skillBase.PreApplyFX();
            skillBase.ApplyFX1();
            skillBase.ApplyFX2();
            skillBase.ApplyFX3();
            skillBase.ApplyMoveFx();
            skillBase.ApplyVFx();
            skillBase.PostApplyFX();

        }
        public void RemoveRetaliateFx(int retaliateID)
        {
            strikerModel.RemoveThornDamage(retaliateID);
        }
        public void RoundTickRetaliate(int roundNo)
        {
            foreach (RetaliateBuffData retaliateBuffData in strikerModel.allRetaliateData.ToList())
            {
                if (retaliateBuffData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (retaliateBuffData.currentTime >= retaliateBuffData.castTime)
                    {
                        RemoveRetaliateFx(retaliateBuffData.retaliateID);
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
            if (strikerModel.allRetaliateData.Count > 0)
            {
                ApplyRetaliate(dmgAppliedData.striker);
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

            DmgAltData dmgAltData = new DmgAltData(valPercent, attackType, dmgType, cultType, raceType);
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

        void RemoveDmgBuffData(DmgBuffData dmgBuffData)
        {
            allDmgBuffData.Remove(dmgBuffData);
        }
        public bool RemoveDmgAltBuff(int dmgBuffID)
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
                if (dmgAltData.damageType != DamageType.None && dmgAltData.damageType == damageType)// Damage Type Block 
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
                else if (dmgAltData.attackType != AttackType.None && dmgAltData.attackType == attackType)// Attack type block
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
                else if (dmgAltData.attackType == AttackType.None && dmgAltData.damageType == DamageType.None)
                {
                    return dmgAltData.valPercent;
                }
            }
            return 0f;
        }
        #endregion

        #region  CHAR STATE DMG ALT BUFF DATA 

        public List<DmgBuffCharStateData> allDmgBuffCharStateData = new List<DmgBuffCharStateData>();

        int dmgCharStateBuffID = 0;
        public int ApplyCharStateDmgAltBuff(float valPercent, CauseType causeType, int causeName, int causeByCharID,
             TimeFrame timeFrame, int netTime, bool isBuff, CharStateName charStateName)
        {
            dmgCharStateBuffID = allDmgBuffCharStateData.Count + 1;

            DmgAltCharStateData dmgAltCSData = new DmgAltCharStateData(valPercent, charStateName);

            int startRoundNo = CombatService.Instance.currentRound;

            DmgBuffCharStateData dmgBuffCSData = new DmgBuffCharStateData(dmgCharStateBuffID, isBuff, startRoundNo, timeFrame
                            , netTime, dmgAltCSData);

            allDmgBuffCharStateData.Add(dmgBuffCSData);
            return dmgCharStateBuffID;
        }
        public void EOCTickDmgCharStateBuff()
        {
            foreach (DmgBuffCharStateData dmgBuffData in allDmgBuffCharStateData.ToList())
            {
                if (dmgBuffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveDmgBuffCharStateData(dmgBuffData);
                }
            }
        }

        public void EORCharStateTick(int roundNo)  // to be completed
        {
            foreach (DmgBuffCharStateData dmgBuffCSData in allDmgBuffCharStateData.ToList())
            {
                if (dmgBuffCSData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (dmgBuffCSData.buffCurrentTime >= dmgBuffCSData.buffedNetTime)
                    {
                        RemoveDmgBuffCharStateData(dmgBuffCSData);
                    }
                    dmgBuffCSData.buffCurrentTime++;
                }
            }
        }

        void RemoveDmgBuffCharStateData(DmgBuffCharStateData dmgBuffCSData)
        {
            allDmgBuffCharStateData.Remove(dmgBuffCSData);
        }
        public bool RemoveDmgAltCharStateBuff(int dmgCharStateBuffID)
        {
            int index = allDmgBuffCharStateData.FindIndex(t => t.dmgBuffID == dmgCharStateBuffID);
            if (index == -1) return false;
            DmgBuffCharStateData dmgBuffCSData = allDmgBuffCharStateData[index];
            RemoveDmgBuffCharStateData(dmgBuffCSData);
            return true;
        }

        public float GetDmgCharStateAlt(CharController targetController)
        {
            // 20% More Dmg against (CHAR STATE) Rooted Targets    
            foreach (DmgBuffCharStateData dmgBuffCSData in allDmgBuffCharStateData.ToList())
            {
                if (targetController.charStateController.HasCharState(dmgBuffCSData.altData.charStateName))
                {
                    return dmgBuffCSData.altData.valPercent;
                }
            }
            return 0f;

        }
        #endregion

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

            public DmgAltData(float valPercent, AttackType attackType, DamageType damageType, CultureType cultType, RaceType raceType)
            {

                this.attackType = attackType;
                this.damageType = damageType;
                this.cultType = cultType;
                this.raceType = raceType;
                this.valPercent = valPercent;
            }

        }
        public class DmgAltCharStateData
        {
            public CharStateName charStateName;
            public float valPercent = 0f;

            public DmgAltCharStateData(float valPercent, CharStateName charStateName = CharStateName.None)
            {
                this.charStateName = charStateName;
                this.valPercent = valPercent;
            }
        }
        public class DmgBuffCharStateData
        {
            public int dmgBuffID;
            public bool isBuff;   // true if BUFF and false if DEBUFF
            public int startRoundNo;
            public TimeFrame timeFrame;
            public int buffedNetTime;
            public int buffCurrentTime;
            public DmgAltCharStateData altData;  // contains value for the buff        

            public DmgBuffCharStateData(int dmgBuffID, bool isBuff, int startRoundNo, TimeFrame timeFrame
                                , int buffedNetTime, DmgAltCharStateData altData)
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
    }


}




