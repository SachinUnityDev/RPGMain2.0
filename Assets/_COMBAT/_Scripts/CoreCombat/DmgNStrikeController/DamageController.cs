using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using DG.Tweening;
using System.Linq;
using static Combat.StrikeController;
using System.Globalization;

namespace Combat
{
    public class DamageController : MonoBehaviour
    {
        //public event Action<DmgAppliedData> OnDamageApplied;
        
        const float hitChanceMin = 40f;
        const float hitChanceMax = 96f;
        CharController charController;
        CharController striker;
        public StrikeType strikeType = StrikeType.Normal; // accessed outside to chk the dodge 
        public bool isMisFire; 

        [Header("Cheated Death")]
        public int cheatedDeathCount = 0;
        public int MAX_ALLOWED_CHEATDEATH = 1;


        float dmgMin, dmgMax;
        [Header("Damage Model")]
        public DmgModel dmgModel;

        void Start()
        {
            charController = GetComponent<CharController>();
            charController.damageController = this;
            CombatEventService.Instance.OnEOC += EOCTickDmgBuff;
            CombatEventService.Instance.OnEOR1 += EORTick; 
                
            Init();            
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnEOC -= EOCTickDmgBuff;
            CombatEventService.Instance.OnEOR1 -= EORTick;
        }
        public void Init()  /// OnSOC 
        {
            dmgModel = new DmgModel();
            cheatedDeathCount = 0;
            MAX_ALLOWED_CHEATDEATH = 1;
        }
        public bool HitChance()  // only for physical // for Dodge   
        {
            // Wrong Hit apply same damage to wrong target           

            float hitChance = 0f;
            // this would not do calc chance

            float dodgeVal = charController.GetAttrib(AttribName.dodge).currValue;
            float dodge = charController.GetStatChance(AttribName.dodge, dodgeVal);

            // Performer
            GameObject strikerGO = CombatService.Instance.currCharOnTurn.gameObject;
            float accVal = CombatService.Instance.currCharOnTurn.GetAttrib(AttribName.acc).currValue;
            float acc = CombatService.Instance.currCharOnTurn.GetStatChance(AttribName.acc, accVal);

        
            hitChance = (acc - dodge);
            // create a helper function in CharService to get stat specific data 
            //  GetComponent data and perform operation here 
            // outliers to be discussed here 
          
            if (hitChance < hitChanceMin)
            {
                hitChance = hitChanceMin;
            }
            if (hitChance > hitChanceMax)
            {
                hitChance = hitChanceMax;
            }

            if (charController.charStateController.HasCharState(CharStateName.Blinded))
            {
                hitChance= 12f;
            }
            bool ch = hitChance.GetChance();
            return ch;
        }
        void LuckCheck()
        {
            AttribData attribData = striker.GetAttrib(AttribName.luck);
            float luckVal = attribData.currValue; 
            float luckChance = charController.GetStatChance(AttribName.luck, luckVal);

            if (luckVal < 6)
            {
                if (luckChance.GetChance())                
                    strikeType = StrikeType.Feeble;                
            }
            else if (luckVal > 6)
            {
                if (luckChance.GetChance())                
                    strikeType = StrikeType.Crit;                
            }
            else
            {
                strikeType = StrikeType.Normal;
            }
        }
        float CritNFeebleApply(float dmg)
        {
            LuckCheck(); // modifies the strikeType
            float dmgNew = 0f;
            if (strikeType == StrikeType.Crit)
            {
                dmgNew = dmg * 1.6f;                
            }
            else if (strikeType == StrikeType.Feeble)
            {
                dmgNew = dmg * 0.6f;
            }
            else
            {
                dmgNew = dmg;
            }
            FortChgOnStrikingCritNFeeble();
            FortChgOnGettingCritNFeeble();
            return dmgNew;
        }
        public void ApplyDamage(CharController striker, CauseType causeType, int causeName, DamageType _dmgType
                                , float dmgPercent, SkillInclination  skillInclination = SkillInclination.None, bool ignoreArmorNRes = false, bool isTrueStrike = false)
        {
            this.striker = striker;
            AttackType attackType = SkillService.Instance.GetSkillAttackType(striker,(SkillNames)causeName);
                     
            isMisFire = false; 
            if (dmgModel.allImmune2Skills.Any(t => t == skillInclination))
                return; 
            // is dodge 

            if(!(causeType == CauseType.ThornsAttack))
            {
                DmgAppliedData dmgApplied = new DmgAppliedData(striker, causeType, causeName
                       , _dmgType, 0f, strikeType, charController, attackType);
                if (!isTrueStrike)
                    if (skillInclination == SkillInclination.Physical && !HitChance())
                    {
                        strikeType = StrikeType.Dodged;                   
                       // CombatEventService.Instance.On_DmgApplied(dmgApplied);
                        CombatEventService.Instance.On_Dodge(dmgApplied);
                        return;
                    }
                if (skillInclination == SkillInclination.Magical)
                {
                    if (FocusCheck())
                    {
                        dmgPercent = ApplyMisFire(dmgPercent);  // dmg val modfied if MIsfire
                    }
                }
            }
            // ask strike controller do you have a extra dmg buff against me 
            float damageAlt = striker.GetComponent<StrikeController>()
                                .GetDmgAlt(charController.charModel, attackType, _dmgType );
            float damageAltCharState = striker.GetComponent<StrikeController>()
                                        .GetDmgCharStateAlt(charController);
            float dmgReceivedAlt = GetDmgReceivedAlt(striker.charModel, attackType, _dmgType); 

            AttribData dmgSDMin = striker.GetAttrib(AttribName.dmgMin);
            AttribData dmgSDMax = striker.GetAttrib(AttribName.dmgMax);
            float percentDmg = dmgPercent + damageAlt  + damageAltCharState;

            // copy of Dmg value for magical and physical + Dmg modifiers 
            int dmgFrmRange = UnityEngine.Random.Range(dmgSDMin.currValue, dmgSDMax.currValue); 
            float dmg = (float)(dmgFrmRange * (percentDmg / 100f));
            int strikerID = striker.charModel.charID;

            float dmgVal = 0; float chgValue =0; 
            switch (_dmgType)
            {
                case DamageType.None:
                    break;
                case DamageType.Physical:
                    dmgVal = CritNFeebleApply(dmg);
                    chgValue = dmgVal; 
                    if (!ignoreArmorNRes)
                    {
                        AttribData armorSDMin = charController.GetAttrib(AttribName.armorMin);
                        AttribData armorSDMax = charController.GetAttrib(AttribName.armorMax);

                        float armor = UnityEngine.Random.Range(armorSDMin.currValue, armorSDMax.currValue);
                        chgValue = dmgVal - armor;
                        chgValue = chgValue < 0 ? 0 : chgValue;
                    }
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -chgValue);
                    break;
             
                case DamageType.Air:
                    dmgVal = CritNFeebleApply(dmg);
                    float airRes = charController.GetAttrib(AttribName.waterRes).currValue;
                    chgValue = dmgVal; 
                    if(!ignoreArmorNRes && airRes > 0)
                        chgValue = dmgVal * (100 - airRes) / 100;
                    chgValue = chgValue < 0 ? 0 : chgValue;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -chgValue);
                    break;
                case DamageType.Water:
                    dmgVal = CritNFeebleApply(dmg);
                    float waterRes = charController.GetAttrib(AttribName.waterRes).currValue;
                    chgValue = dmgVal;
                    if (!ignoreArmorNRes && waterRes > 0)
                        chgValue = dmgVal * ((100f - waterRes) / 100f);
                    chgValue = chgValue < 0 ? 0 : chgValue;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -chgValue);
                    break;
                case DamageType.Earth:
                    dmgVal = CritNFeebleApply(dmg);
                    float earthRes = charController.GetAttrib(AttribName.earthRes).currValue;
                    chgValue = dmgVal;
                    if (!ignoreArmorNRes && earthRes > 0)
                        chgValue = dmgVal * ((100f - earthRes) / 100f);
                    chgValue = chgValue < 0 ? 0 : chgValue;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -chgValue);
                    break;
                case DamageType.Fire:
                    dmgVal = CritNFeebleApply(dmg);
                    float fireRes = charController.GetAttrib(AttribName.fireRes).currValue;
                    chgValue = dmgVal;
                    if (!ignoreArmorNRes && fireRes > 0)
                        chgValue = dmgVal * (100 - fireRes) / 100;

                    chgValue = chgValue < 0 ? 0 : chgValue;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -chgValue);
                    break;
                case DamageType.Light:
                    dmgVal = CritNFeebleApply(dmg);
                    float lightRes = charController.GetAttrib(AttribName.lightRes).currValue;
                    chgValue = dmgVal;
                    if (!ignoreArmorNRes && lightRes > 0)
                        chgValue = dmgVal * ((100f - lightRes) / 100f);

                    chgValue = chgValue < 0 ? 0 : chgValue;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -chgValue);
                    break;
                case DamageType.Dark:
                    dmgVal = CritNFeebleApply(dmg);
                    float darkRes = charController.GetAttrib(AttribName.darkRes).currValue;
                    chgValue = dmgVal;
                    if (!ignoreArmorNRes && darkRes > 0)
                        chgValue = dmgVal * ((100f - darkRes) / 100f);

                    chgValue = chgValue < 0 ? 0 : chgValue;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -chgValue);
                    break;
                case DamageType.Pure:
                    chgValue = dmgPercent; 
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -chgValue);
                    break;                                 
              
                default:
                    break;
            }


            DmgAppliedData dmgAppliedData = new DmgAppliedData(striker, causeType, causeName
                                            , _dmgType, chgValue, strikeType, charController, attackType, isMisFire);
            if (isMisFire)
            {
                CombatEventService.Instance.On_Misfire(dmgAppliedData);
            }
            if ((SkillNames)causeName != SkillNames.Retaliate)  // to prevent infinite loop
                CombatEventService.Instance.On_DmgApplied(dmgAppliedData);

        }
   
        // Striking Crit and feeble 
        void FortChgOnStrikingCritNFeeble()
        {
            if (strikeType == StrikeType.Normal)
                return;
            if (strikeType == StrikeType.Crit)
            {
                if (striker.charModel.charMode == CharMode.Ally)
                {
                    if (striker.charModel.orgCharMode == CharMode.Ally)
                        striker.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatName.fortitude, +6);

                    foreach (CharController c in CharService.Instance.allCharInCombat)
                    {
                        if (c.charModel.charID != striker.charModel.charID
                            && c.charModel.orgCharMode == CharMode.Ally)
                            c.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatName.fortitude, +3);
                    }
                }
            }
            if (strikeType == StrikeType.Feeble)
            {
                if (striker.charModel.charMode == CharMode.Ally)
                {
                    if (striker.charModel.orgCharMode == CharMode.Ally)
                        striker.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatName.fortitude, -6);

                    foreach (CharController c in CharService.Instance.allCharInCombat)
                    {
                        if (c.charModel.charID != striker.charModel.charID
                            && c.charModel.orgCharMode == CharMode.Ally)
                            c.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatName.fortitude, -3);
                    }
                }
            }
        }
        // Getting Crit and Feeble
        void FortChgOnGettingCritNFeeble()
        {
            if (strikeType == StrikeType.Normal)
                return;
            if (strikeType == StrikeType.Crit)
            {
                if (charController.charModel.charMode == CharMode.Ally)
                {
                    if (charController.charModel.orgCharMode == CharMode.Ally)
                        charController.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatName.fortitude, -6);

                    foreach (CharController c in CharService.Instance.allCharInCombat)
                    {
                        if (c.charModel.charID != charController.charModel.charID
                            && c.charModel.orgCharMode == CharMode.Ally)
                            c.ChangeStat(CauseType.CriticalStrike, 0, charController.charModel.charID, StatName.fortitude, -3);
                    }
                }
               
            }
            if (strikeType == StrikeType.Feeble)
            {
                if (charController.charModel.charMode == CharMode.Ally)
                {
                    if (charController.charModel.orgCharMode == CharMode.Ally)
                        charController.ChangeStat(CauseType.CriticalStrike, 0, charController.charModel.charID, StatName.fortitude, +6);

                    foreach (CharController c in CharService.Instance.allCharInCombat)
                    {
                        if (c.charModel.charID != charController.charModel.charID
                            && c.charModel.orgCharMode == CharMode.Ally)
                            c.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatName.fortitude, +3);
                    }
                }
            }
        }
        public void CheatDeath()
        {
            if(cheatedDeathCount < MAX_ALLOWED_CHEATDEATH)
            {
                // Gain +% 30 health and +18 Fortitude instantly    -1 Fort Origin permanently(Base value)            
                charController.ChangeStat(CauseType.StatMinMaxLimit, 1, charController.charModel.charID
                                                                                , StatName.health, +30f);
                charController.ChangeStat(CauseType.StatMinMaxLimit, 1, charController.charModel.charID
                                                                                , StatName.fortitude, +18f);
                charController.ChangeAttribBaseVal(CauseType.StatMinMaxLimit, 1, charController.charModel.charID
                                                                                , AttribName.fortOrg, -1);
                charController.charStateController.ApplyCharStateBuff(CauseType.StatMinMaxLimit, (int)1
                       , charController.charModel.charID, CharStateName.CheatedDeath);

                cheatedDeathCount++;
            }
        }

        #region FOCUS CHK AND MISFIRE

        public bool FocusCheck()// Magical only .. 
        {
            // get focus statChance Data of performers focus 
            // depending on that decide ..TO be decided 
            float focusVal = charController.GetAttrib(AttribName.focus).currValue;
            float focusChance = 100f - charController.GetStatChance(AttribName.focus, focusVal);

            if (charController.charStateController.HasCharState(CharStateName.Confused))
            {
                return 50f.GetChance();
            }
            else
            {
                return focusChance.GetChance();
            }
        }
        public float ApplyMisFire(float dmgVal)
        {
            // SKIP AKILL APPLY DMG 
            SkillController1 skillController = SkillService.Instance.currSkillController;

            SkillBase skillBase = skillController.allSkillBases
                                .Find(t => t.skillName == SkillService.Instance.currSkillName);
            StrikeNos strikeNos = skillBase.skillModel.strikeNos; 
            isMisFire= true;
            if (strikeNos == StrikeNos.Single)
            {
                int netTargetCount = CombatService.Instance.mainTargetDynas.Count;
                if (netTargetCount > 1)
                {
                    CombatService.Instance.mainTargetDynas.Remove(SkillService.Instance.currentTargetDyna);
                    int random = UnityEngine.Random.Range(0, netTargetCount - 1);
                    SkillService.Instance.currentTargetDyna = CombatService.Instance.mainTargetDynas[random];
                    return dmgVal; 
                }
                else
                {
                    return dmgVal * 0.8f; 
                    //ReduceDmgPercent();
                    //SkillService.Instance.PostSkillApply += RevertDamageRange;
                }
            }
            else
            {
                return dmgVal * 0.8f;
                //ReduceDmgPercent();
                //SkillService.Instance.PostSkillApply += RevertDamageRange;
            }
        }
        #endregion

        #region BLEED , BURN AND POISONED DOT

        public void ApplyLowBleed(CauseType causeType, int causeName, int causeByCharID)
        {

            if (charController.charStateController.HasCharState(CharStateName.Poisoned))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.Poisoned).castTime;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.Poisoned).IncrCastTime(1);
            }
            charController.charStateController.ApplyCharStateBuff(causeType,causeName, causeByCharID
                                                    , CharStateName.Bleeding, TimeFrame.EndOfRound, 2); 
        }
        public void ApplyHighBleed(CauseType causeType, int causeName, int causeByCharID)
        {

            if (charController.charStateController.HasCharState(CharStateName.Poisoned))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.Poisoned).castTime;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.Poisoned).IncrCastTime(1);
            }
            if (charController.charStateController.HasCharState(CharStateName.Bleeding))
            {
                charController.ChangeStat(causeType, (int)causeName, 
                                            charController.charModel.charID, StatName.fortitude, -4);
            }           
            charController.charStateController.ApplyCharStateBuff(causeType, causeName, causeByCharID
                                                    , CharStateName.Bleeding, TimeFrame.EndOfRound, 4);
        }
        public void ApplyLowPoison(CauseType causeType, int causeName, int causeByCharID)
        {
            charController.charStateController.ApplyCharStateBuff(causeType, causeName, causeByCharID
                                                    , CharStateName.Poisoned, TimeFrame.EndOfRound, 2);

            if (charController.charStateController.HasCharState(CharStateName.Bleeding))
            {
                charController.ChangeStat(causeType, (int)causeName,
                               charController.charModel.charID, StatName.stamina, -4);
            }

            if (charController.charStateController.HasCharState(CharStateName.Burning))
            {
                if (GameService.Instance.currGameModel.gameScene != GameScene.InCombat) return; 

                CharController strikeCtrl = CombatService.Instance.currCharOnTurn; 
                ApplyDamage(strikeCtrl, causeType, (int)causeName, DamageType.Earth, 40f);
            }
        }
        public void ApplyHighPoison(CauseType causeType, int causeName, int causeByCharID)
        {
            charController.charStateController.ApplyCharStateBuff(causeType, causeName, causeByCharID
                                                    , CharStateName.Poisoned, TimeFrame.EndOfRound, 3);

            if (charController.charStateController.HasCharState(CharStateName.Bleeding))
            {
                charController.ChangeStat(causeType, (int)causeName,
                               charController.charModel.charID, StatName.stamina, -6);
            }

            if (charController.charStateController.HasCharState(CharStateName.Burning))
            {
                if (GameService.Instance.currGameModel.gameScene != GameScene.InCombat) return;

                CharController strikeCtrl = CombatService.Instance.currCharOnTurn;
                ApplyDamage(strikeCtrl, causeType, (int)causeName, DamageType.Earth, 60f);
            }
        }

        public void ApplyLowBurn(CauseType causeType, int causeName, int causeByCharID)
        {
            charController.charStateController.ApplyCharStateBuff(causeType, causeName, causeByCharID
                                                    , CharStateName.Burning, TimeFrame.EndOfRound,2);

            if (charController.charStateController.HasCharState(CharStateName.Bleeding))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.Bleeding).castTime;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.Bleeding).IncrCastTime(-1);                
            }

            if (charController.charStateController.HasCharState(CharStateName.Poisoned))
            {
                if (GameService.Instance.currGameModel.gameScene != GameScene.InCombat) return;

                CharController strikeCtrl = CombatService.Instance.currCharOnTurn;
                ApplyDamage(strikeCtrl, causeType, (int)causeName, DamageType.Fire, 50f);
            }
        }
        public void ApplyHighBurn(CauseType causeType, int causeName, int causeByCharID)
        {
            charController.charStateController.ApplyCharStateBuff(causeType, causeName, causeByCharID
                                                    , CharStateName.Burning, TimeFrame.EndOfRound, 4);

            if (charController.charStateController.HasCharState(CharStateName.Bleeding))
            {
                charController.charStateController.RemoveCharState(CharStateName.Bleeding);   
            }

            if (charController.charStateController.HasCharState(CharStateName.Poisoned))
            {
                if (GameService.Instance.currGameModel.gameScene != GameScene.InCombat) return;

                CharController strikeCtrl = CombatService.Instance.currCharOnTurn;
                ApplyDamage(strikeCtrl, causeType, (int)causeName, DamageType.Fire, 90f);
            }
        }


        #endregion
        #region DAMAGE RECEIVE BUFF ALTERER

        public List<DmgBuffData> allDmgReceivedBuffData = new List<DmgBuffData>();

        int dmgBuffID = 0;
        public int ApplyDmgReceivedAltBuff(float valPercent, CauseType causeType, int causeName, int causeByCharID,
             TimeFrame timeFrame, int netTime, bool isBuff,
            AttackType attackType = AttackType.None, DamageType dmgType = DamageType.None,
            CultureType cultType = CultureType.None, RaceType raceType = RaceType.None)
        {
            dmgBuffID = allDmgReceivedBuffData.Count + 1;

            DmgAltData dmgAltData = new DmgAltData(valPercent, attackType, dmgType, cultType, raceType);
            int startRoundNo = CombatEventService.Instance.currentRound;

            DmgBuffData dmgBuffData = new DmgBuffData(dmgBuffID, isBuff, startRoundNo, timeFrame
                            , netTime, dmgAltData);

            allDmgReceivedBuffData.Add(dmgBuffData);
            return dmgBuffID;
        }
        public void EOCTickDmgBuff()
        {
            foreach (DmgBuffData dmgBuffData in allDmgReceivedBuffData.ToList())
            {
                if (dmgBuffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveDmgBuffData(dmgBuffData);
                }
            }
        }

        public void EORTick(int roundNo)  // to be completed
        {
            foreach (DmgBuffData dmgBuffData in allDmgReceivedBuffData.ToList())
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
            allDmgReceivedBuffData.Remove(dmgBuffData);
        }
        public bool RemoveDmgReceivedAltBuff(int dmgBuffID)
        {
            int index = allDmgReceivedBuffData.FindIndex(t => t.dmgBuffID == dmgBuffID);
            if (index == -1) return false;
            DmgBuffData dmgBuffData = allDmgReceivedBuffData[index];
            RemoveDmgBuffData(dmgBuffData);
            return true;
        }
        public float GetDmgReceivedAlt1( AttackType attackType = AttackType.None, DamageType damageType = DamageType.None
                                        , RaceType raceType = RaceType.None, CultureType cultType = CultureType.None
                                        , CharStateName charStateName = CharStateName.None, TempTraitName tempTraitName = TempTraitName.None)
        {
            List<DmgBuffData> statAltData_attackType = allDmgReceivedBuffData.Where(t => t.altData.attackType == attackType).ToList();
            List<DmgBuffData> statAltData_damageType = statAltData_attackType.Where(t => t.altData.damageType == damageType ).ToList();

            List<DmgBuffData> statAltData_raceType = statAltData_damageType.Where(t => t.altData.raceType == raceType).ToList();
            List<DmgBuffData> statAltData_cultType = statAltData_raceType.Where(t => t.altData.cultType == cultType).ToList();
            List<DmgBuffData> statAltData_charState = statAltData_cultType.Where(t => t.altData.cultType == cultType).ToList();
            List<DmgBuffData> statAltData_tempTrait = statAltData_charState.Where(t => t.altData.cultType == cultType).ToList();

            float val = 0f;
            foreach (DmgBuffData statAltBuffData in statAltData_tempTrait)
            {
                val += statAltBuffData.altData.valPercent;
            }
            return val;
        }
        public float GetDmgReceivedAlt(CharModel strikerModel, AttackType attackType = AttackType.None
                                         , DamageType damageType = DamageType.None)
        {
            // 20% physical attack against beastmen            
            foreach (DmgBuffData dmgBuffData in allDmgReceivedBuffData.ToList())
            {
                DmgAltData dmgAltData = dmgBuffData.altData;
                if (dmgAltData.damageType != DamageType.None && dmgAltData.damageType == damageType)// Damage Type Block 
                {
                    float val = 0;
                    if (dmgAltData.raceType != RaceType.None
                        && dmgAltData.raceType == strikerModel.raceType
                                && dmgAltData.cultType != CultureType.None
                                && dmgAltData.cultType == strikerModel.cultType)
                    {

                        val = dmgAltData.valPercent; // COMBO RACE AND CULT

                    }
                    else   // NOT A COMBO OF RACE AND CULT
                    {
                        if (dmgAltData.raceType != RaceType.None
                                  && dmgAltData.raceType == strikerModel.raceType)
                        {
                            val = dmgAltData.valPercent;
                        }
                        if (dmgAltData.cultType != CultureType.None
                                        && dmgAltData.cultType == strikerModel.cultType)
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
                        && dmgAltData.raceType == strikerModel.raceType
                                && dmgAltData.cultType != CultureType.None
                                && dmgAltData.cultType == strikerModel.cultType)
                    {

                        val = dmgAltData.valPercent; // COMBO RACE AND CULT

                    }
                    else   // NOT A COMBO OF RACE AND CULT
                    {
                        if (dmgAltData.raceType != RaceType.None
                                  && dmgAltData.raceType == strikerModel.raceType)
                        {
                            val = dmgAltData.valPercent;
                        }
                        if (dmgAltData.cultType != CultureType.None
                                        && dmgAltData.cultType == strikerModel.cultType)
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

    }
    #region DATA CLASSES
    public class DmgAppliedData   /// Data broadcasted by target on being hit
    {
        public CharController striker;
        public CauseType causeType;
        public int causeName;
        public DamageType dmgType;
        public AttackType attackType;
        public float dmgValue;
        public StrikeType strikeType;
        public CharController targetController;
        public bool isMisFire = false; 
        public DmgAppliedData(CharController striker, CauseType causeType, int causeName, DamageType dmgType, float dmgValue
                            , StrikeType strikeType, CharController targetController, AttackType attackType, bool isMisFire = false)
        {
            this.striker = striker;
            this.causeType = causeType;
            this.causeName = causeName;
            this.dmgType = dmgType;
            this.dmgValue = dmgValue;
            this.strikeType = strikeType;
            this.targetController = targetController;
            this.attackType = attackType;
            this.isMisFire = isMisFire;
        }
    }

    #endregion
}
