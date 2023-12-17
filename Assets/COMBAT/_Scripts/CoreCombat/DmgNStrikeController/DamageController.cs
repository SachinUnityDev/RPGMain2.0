using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using DG.Tweening;
using System.Linq;

namespace Combat
{
    public class DamageController : MonoBehaviour
    {
        //public event Action<DmgAppliedData> OnDamageApplied;
        
        const float hitChanceMin = 30f;
        const float hitChanceMax = 93f;
        CharController charController;
        CharController striker;
        StrikeType strikeType = StrikeType.Normal;

        [Header("Cheated Death")]
        public int cheatedDeathCount = 0;
        public int MAX_ALLOWED_CHEATDEATH = 1;


        float dmgMin, dmgMax;
        [Header("Damage Model")]
        public DmgModel dmgModel;

       

        void Start()
        {
            charController = GetComponent<CharController>();
            Init();
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
            return hitChance.GetChance();
        }
        void LuckCheck()
        {
            float luckVal = charController.GetAttrib(AttribName.luck).currValue;
            float luckChance = charController.GetStatChance(AttribName.luck, luckVal);

            if (luckVal < 6)
            {
                if (luckChance.GetChance())
                {
                    strikeType = StrikeType.Feeble;
                }
            }
            else if (luckVal > 6)
            {
                if (luckChance.GetChance())
                {
                    strikeType = StrikeType.Crit;
                }
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

        public void ApplyDamage(CharController striker, CauseType causeType, int causeName
                                    , DamageType _dmgType, float dmgPercentORVal, SkillInclination skillInclination = SkillInclination.None, bool ignoreArmorNRes = false
                                    , bool isTrueStrike = false)
        {
            this.striker = striker;
            AttackType attackType =
                            SkillService.Instance.GetSkillAttackType((SkillNames)causeName);
            // immune to skills Incli

            if (dmgModel.allImmune2Skills.Any(t => t == skillInclination))
                return; 
            
            
            // is dodge 

            if(!(causeType == CauseType.ThornsAttack))
            {
                if (!isTrueStrike)
                    if (skillInclination == SkillInclination.Physical && HitChance())
                    {
                        strikeType = StrikeType.Dodged;
                        DmgAppliedData dmgApplied = new DmgAppliedData(striker, causeType, causeName
                            , _dmgType, 0f, strikeType, charController, attackType);
                        CombatEventService.Instance.On_DmgApplied(dmgApplied);
                        CombatEventService.Instance.On_Dodge(dmgApplied);
                        return;
                    }

                if (skillInclination == SkillInclination.Magical)
                {
                    if (FocusCheck())
                    {
                        ApplyMisFire();
                    }
                }
            }
            // ask strike controller do you have a extra dmg buff against me 
            float damageAlt = striker.GetComponent<StrikeController>()
                                .GetDmgAlt(charController.charModel, attackType, _dmgType );
            float damageAltCharState = striker.GetComponent<StrikeController>()
                                .GetDmgCharStateAlt(charController);


            AttribData dmgSDMin = striker.GetAttrib(AttribName.dmgMin);
            AttribData dmgSDMax = striker.GetAttrib(AttribName.dmgMax);
            float percentDmg = dmgPercentORVal + damageAlt + damageAltCharState;
            // copy of Dmg value for magical and physical + Dmg modifiers 

            float dmg = (float)(UnityEngine.Random.Range(dmgSDMin.currValue, dmgSDMax.currValue) * (percentDmg / 100f));
            int strikerID = striker.charModel.charID;
           // Debug.Log("MIN AND MAX RANGE " + dmgSDMin.currValue + dmgSDMax.currValue + "DAMAGE " + dmg);


            switch (_dmgType)
            {
                case DamageType.None:
                    break;
                case DamageType.Physical:
                    float dmgVal = CritNFeebleApply(dmg);
                    float chgValue = dmgVal; 
                    if (ignoreArmorNRes)
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
                    float airDmg = dmgVal; 
                    if(ignoreArmorNRes && airRes > 0)
                            airDmg = dmgVal * (100 - airRes) / 100;
                     airDmg = airDmg < 0 ? 0 : airDmg;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -airDmg);
                    break;
                case DamageType.Water:
                    dmgVal = CritNFeebleApply(dmg);
                    float waterRes = charController.GetAttrib(AttribName.waterRes).currValue;
                    float waterDmg = dmgVal;
                    if (ignoreArmorNRes && waterRes > 0)
                         waterDmg = dmgVal * ((100f - waterRes) / 100f);
                     waterDmg = waterDmg < 0 ? 0 : waterDmg;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -waterDmg);
                    break;
                case DamageType.Earth:
                    dmgVal = CritNFeebleApply(dmg);
                    float earthRes = charController.GetAttrib(AttribName.earthRes).currValue;
                    float earthDmg = dmgVal;
                    if (ignoreArmorNRes && earthRes > 0)
                        earthDmg = dmgVal * ((100f - earthRes) / 100f);
                    
                      earthDmg = earthDmg < 0 ? 0 : earthDmg;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -earthDmg);
                    break;
                case DamageType.Fire:
                    dmgVal = CritNFeebleApply(dmg);
                    float fireRes = charController.GetAttrib(AttribName.fireRes).currValue;
                    float fireDmg = dmgVal;
                    if (ignoreArmorNRes && fireRes > 0)
                        fireDmg = dmgVal * (100 - fireRes) / 100;

                     fireDmg = fireDmg < 0 ? 0 : fireDmg;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -fireDmg);
                    break;
                case DamageType.Light:
                    dmgVal = CritNFeebleApply(dmg);
                    float lightRes = charController.GetAttrib(AttribName.lightRes).currValue;
                    float lightDmg = dmgVal;
                    if (ignoreArmorNRes && lightRes > 0)
                        lightDmg = dmgVal * ((100f - lightRes) / 100f);
                    
                       lightDmg = lightDmg < 0 ? 0 : lightDmg;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -lightDmg);
                    break;
                case DamageType.Dark:
                    dmgVal = CritNFeebleApply(dmg);
                    float darkRes = charController.GetAttrib(AttribName.darkRes).currValue;
                    float darkDmg = dmgVal;
                    if (ignoreArmorNRes && darkRes > 0)
                        darkDmg = dmgVal * ((100f - darkRes) / 100f);
                    
                       darkDmg = darkDmg < 0 ? 0 : darkDmg;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -darkDmg);
                    break;
                case DamageType.Pure:
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, -dmgPercentORVal);
                    break;
                case DamageType.Blank1:

                    break;
                case DamageType.Blank2:

                    break;
                case DamageType.Blank3:
                    break;
                case DamageType.Heal:
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.health, dmgPercentORVal*(1 + damageAlt/100));
                    break;
                case DamageType.FortitudeDmg:
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.fortitude, -dmgPercentORVal);
                    break;
                case DamageType.StaminaDmg: // no resistance, no armor etc ok no substractions for stamina , Fort and Pure
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatName.stamina, -dmgPercentORVal);
                    break;
                //case DamageType.HealthDmg:
                //    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.health, -dmgPercentVal);
                //    break; 
                default:
                    break;
            }
            CombatEventService.Instance.On_DmgApplied(new DmgAppliedData(striker, causeType, causeName
                                                , _dmgType, dmgPercentORVal, strikeType, charController, attackType));

        }
        public void HealingAsPercentOfMaxHP(CharController charController, CauseType causeType, int causeName, float val)
        {
            StatData statData = charController.GetStat(StatName.health);
            float healVal = ((val / 100) * statData.maxLimit);

            ApplyDamage(charController, causeType, causeName, DamageType.Heal, healVal, SkillInclination.Heal); 
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
        public void ApplyMisFire()
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
            charController.GetAttrib(AttribName.dmgMin).currValue = (int)dmgMin;
            charController.GetAttrib(AttribName.dmgMin).currValue = (int)dmgMax;
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
        public DmgAppliedData(CharController striker, CauseType causeType, int causeName, DamageType dmgType, float dmgValue
                            , StrikeType strikeType, CharController targetController, AttackType attackType)
        {
            this.striker = striker;
            this.causeType = causeType;
            this.causeName = causeName;
            this.dmgType = dmgType;
            this.dmgValue = dmgValue;
            this.strikeType = strikeType;
            this.targetController = targetController;
            this.attackType = attackType;
        }
    }

    #endregion
}


//public bool AccuracyCheck()// Physical 
//{
//    float accVal = charController.GetAttrib(AttribName.acc).currValue;
//    float accChance = charController.GetStatChance(AttribName.acc, accVal);

//    if (accVal == 0)
//    { // self inflicted

//        int buffId = charController.charStateController.ApplyCharStateBuff(CauseType.CharState, (int)CharStateName.Confused
//             , charController.charModel.charID, CharStateName.Blinded, TimeFrame.Infinity, -1);
//        return false;// miss the target .. i.e not going to hit/FX anyone.. 
//    }
//    else
//    {
//        return accChance.GetChance();
//    }
//}