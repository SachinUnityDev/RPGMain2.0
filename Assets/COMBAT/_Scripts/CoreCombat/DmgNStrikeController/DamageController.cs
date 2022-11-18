using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using System.Security.Policy;
using Spine;

namespace Combat
{

    public class DmgAppliedData   /// Data broadcasted by target on being hit
    {
        public CharController striker;
        public CauseType causeType; 
        public int  causeName;
        public DamageType dmgType;
        public float dmgValue;
        public  StrikeType strikeType;
        public CharController targetController;       
        public DmgAppliedData(CharController striker, CauseType causeType, int causeName, DamageType dmgType, float dmgValue
                            , StrikeType strikeType, CharController targetController)
        {
            this.striker = striker;
            this.causeType = causeType;
            this.causeName = causeName;
            this.dmgType = dmgType;
            this.dmgValue = dmgValue;
            this.strikeType = strikeType;
            this.targetController = targetController; 
        }

        //public DmgAppliedData(CharController striker, CauseType causeType, int causeName, DamageType dmgType
        //    , float dmgValue, StrikeType strikeType, AttackType attackType) 
        //{
        //    this.striker = striker;
        //    this.causeType = causeType;
        //    this.causeName = causeName;
        //    this.dmgType = dmgType;
        //    this.dmgValue = dmgValue;
        //    this.strikeType = strikeType;
        //    this.attackType = attackType;
        //}
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
                            , int buffedNetTime,  DmgAltData altData)
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
        public float valPercent =0f;

        public DmgAltData(float valPercent, AttackType attackType, DamageType damageType, CultureType cultType, RaceType raceType )
        {
            this.attackType = attackType;
            this.damageType = damageType;
            this.cultType = cultType;
            this.raceType = raceType;
            this.valPercent = valPercent;
        }

        //public DmgAltData(AttackType attackType, float valPercent)
        //{
        //    this.attackType = attackType; 
        //    this.valPercent = valPercent;   
        //} 
        //public DmgAltData(DamageType damageType, float valPercent)
        //{
        //    this.damageType = damageType;
        //    this.valPercent = valPercent;
        //}
        //public DmgAltData(CultureType cultType, float valPercent)
        //{
        //    this.cultType = cultType;
        //    this.valPercent = valPercent;
        //}
        //public DmgAltData(RaceType raceType, float valPercent)
        //{
        //    this.raceType = raceType;
        //    this.valPercent = valPercent;
        //}

    }



    public class DamageController : MonoBehaviour
    {
        public event Action<DmgAppliedData> OnDamageApplied;

        const float hitChanceMin = 30f;

        const float hitChanceMax = 90f;
        CharController charController;
        CharController striker;
        StrikeType strikeType = StrikeType.Normal;


        [Header("Damage Model")]
        public DmgModel dmgModel;

        //bool isCritical = false;
        //bool isFeeble = false; 
        void Start()
        {
            charController = GetComponent<CharController>();
            Init();
        }

        public void Init()
        {
            dmgModel = new DmgModel();
        }
        public bool HitChance()  // only for physical 
        {
            // Wrong Hit apply same damage to wrong target           

            float hitChance = 0f;
            // this would not do calc chance

            float dodgeVal = charController.GetStat(StatsName.dodge).currValue;
            float dodge = charController.GetStatChance(StatsName.dodge, dodgeVal);

            // Performer
            GameObject performerGO = CombatService.Instance.currCharOnTurn.gameObject;
            float accVal = CombatService.Instance.currCharOnTurn.GetStat(StatsName.acc).currValue;
            float acc = CombatService.Instance.currCharOnTurn.GetStatChance(StatsName.acc, accVal);

            hitChance = 60f + 6 * (acc - dodge);
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

            if (CharStatesService.Instance.HasCharState(performerGO, CharStateName.Blinded))
            {
                hitChance = 12f;
            }
            return hitChance.GetChance();
        }
        void LuckCheck()
        {
            float luckVal = charController.GetStat(StatsName.luck).currValue;
            float luckChance = charController.GetStatChance(StatsName.luck, luckVal);

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
            LuckCheck();
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
                            , DamageType _dmgType, float dmgPercentVal, bool ignoreArmor = false)
        {
            this.striker = striker;
            AttackType attackType =
                            SkillService.Instance.GetSkillAttackType((SkillNames)causeName);
            float DamageModChg = ApplyDmgAltBuff(attackType, _dmgType);
            StatData dmgSD = striker.GetStat(StatsName.damage);
            float percentDmg = dmgPercentVal + DamageModChg; // copy of Dmg value for magical and physical + Dmg modifiers 

            float dmg = (float)(UnityEngine.Random.Range(dmgSD.minRange, dmgSD.maxRange) * (percentDmg / 100f));
            int strikerID = striker.charModel.charID;
            Debug.Log("MIN AND MAX RANGE " + dmgSD.minRange + dmgSD.maxRange + "DAMAGE " + dmg);

            switch (_dmgType)
            {
                case DamageType.None:
                    break;
                case DamageType.Physical:
                    float dmgVal = CritNFeebleApply(dmg);
                    StatData armorSD = charController.GetStat(StatsName.armor);
                    float armor = UnityEngine.Random.Range(armorSD.minRange, armorSD.maxRange);
                    float chgValue = dmgVal - armor;
                    chgValue = chgValue < 0 ? 0 : chgValue;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.health, -chgValue);
                    break;

                case DamageType.StaminaDmg: // no resistance, no armor etc ok no substractions for stamina , Fort and Pure
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.stamina, -dmgPercentVal);

                    break;
                case DamageType.Air:
                    dmgVal = CritNFeebleApply(dmg);
                    float airRes = charController.GetStat(StatsName.waterRes).currValue;
                    float airDmg = dmgVal * (100 - airRes) / 100;
                    // airDmg = airDmg < 0 ? 0 : airDmg;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.health, -airDmg);
                    break;
                case DamageType.Water:
                    dmgVal = CritNFeebleApply(dmg);
                    float waterRes = charController.GetStat(StatsName.waterRes).currValue;
                    float waterDmg = dmgVal * ((100f - waterRes) / 100f);
                    // waterDmg = waterDmg < 0 ? 0 : waterDmg;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.health, -waterDmg);
                    break;
                case DamageType.Earth:
                    dmgVal = CritNFeebleApply(dmg);
                    float earthRes = charController.GetStat(StatsName.earthRes).currValue;
                    float earthDmg = dmgVal * (100 - earthRes) / 100;
                    //  earthDmg = earthDmg < 0 ? 0 : earthDmg;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.health, -earthDmg);
                    break;
                case DamageType.Fire:
                    dmgVal = CritNFeebleApply(dmg);
                    float fireRes = charController.GetStat(StatsName.fireRes).currValue;
                    float fireDmg = dmgVal * (100 - fireRes) / 100;
                    // fireDmg = fireDmg < 0 ? 0 : fireDmg;

                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.health, -fireDmg);
                    break;
                case DamageType.Light:
                    dmgVal = CritNFeebleApply(dmg);
                    float lightRes = charController.GetStat(StatsName.lightRes).currValue;
                    float lightDmg = dmgVal * (100 - lightRes) / 100;
                    //   lightDmg = lightDmg < 0 ? 0 : lightDmg;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.health, -lightDmg);
                    break;
                case DamageType.Dark:
                    dmgVal = CritNFeebleApply(dmg);
                    float darkRes = charController.GetStat(StatsName.darkRes).currValue;
                    float darkDmg = dmgVal * (100 - darkRes) / 100;
                    //   darkDmg = darkDmg < 0 ? 0 : darkDmg;
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.health, -darkDmg);
                    break;
                case DamageType.Pure:
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.health, -dmgPercentVal);
                    break;
                case DamageType.Buff:

                    break;
                case DamageType.Debuff:

                    break;
                case DamageType.Guard:

                    break;
                case DamageType.Heal:
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.health, dmgPercentVal);
                    break;
                case DamageType.FortitudeDmg:
                    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.fortitude, -dmgPercentVal);

                    break;
                //case DamageType.HealthDmg:
                //    charController.ChangeStat(CauseType.CharSkill, (int)causeName, strikerID, StatsName.health, -dmgPercentVal);
                //    break; 
                default:
                    break;
            }
            OnDamageApplied?.Invoke(new DmgAppliedData(striker, causeType, causeName, _dmgType, dmgPercentVal, strikeType, charController));

        }
        // code it like buff return a index and get it sorted 

        // COMBAT CHAR MODEL WILL CONTAIN STRIKEDATA, DMGDATA,


 #region DAMAGE BUFF CONTROLLER


        public List<DmgBuffData> allDmgBuffData = new List<DmgBuffData>();

        int dmgBuffID = 0; 
        int ApplyDmgAltBuff(float valPercent, CauseType causeType, int causeName, int causeByCharID, 
             TimeFrame timeFrame, int netTime, bool isBuff,
            AttackType attackType = AttackType.None, DamageType dmgType = DamageType.None,
            CultureType cultType = CultureType.None, RaceType raceType = RaceType.None)
        {           
            DmgAltData dmgAltData = new DmgAltData(valPercent, attackType, dmgType, cultType, raceType);
            int startRoundNo = CombatService.Instance.currentRound;
            dmgBuffID++; 
            
            DmgBuffData dmgBuffData = new DmgBuffData(dmgBuffID, isBuff,startRoundNo, timeFrame
                            , netTime, dmgAltData);

            allDmgBuffData.Add(dmgBuffData);

            return dmgBuffID;
        }
        public void EOCTick()
        {
            foreach (DmgBuffData dmgBuffData in allDmgBuffData)
            {
                if (dmgBuffData.timeFrame == TimeFrame.EndOfCombat)
                {
                   // RemoveBuffData(buffData);
                }
            }
        }
        //{
        //    //float damageMod = 0f;




        //    // multi race n culture flexibility is must
        //     // add the damage Mod for this race 
        //     // add the damage mod for this cult 


        //    //switch (attackType)
        //    //{
        //    //    case AttackType.None:
        //    //        break;
        //    //    case AttackType.Melee:
        //    //        damageMod += charController.charModel.meleeAttackTypeMod;
        //    //        // MOVE THIS TO COMBAT CHAR MODEL

        //    //        break;
        //    //    case AttackType.Ranged:
        //    //        damageMod += charController.charModel.rangedAttackTypeMod;
        //    //        break;
        //    //    case AttackType.Remote:
        //    //        damageMod += charController.charModel.remoteAttackTypeMod;
        //    //        break;
        //    //    default:
        //    //        break;
        //    //}

        //    //switch (damageType)
        //    //{
        //    //    case DamageType.None:
        //    //        break;
        //    //    case DamageType.Physical:
        //    //        damageMod += charController.charModel.physicalSIMod;
        //    //        break;
        //    //    case DamageType.StaminaDmg:
        //    //        damageMod += charController.charModel.physicalSIMod;

        //    //        break;
        //    //    case DamageType.Air:
        //    //        damageMod += charController.charModel.physicalSIMod;


        //    //        break;
        //    //    case DamageType.Water:
        //    //        damageMod += charController.charModel.physicalSIMod;

        //    //        break;
        //    //    case DamageType.Earth:
        //    //        damageMod += charController.charModel.physicalSIMod;

        //    //        break;
        //    //    case DamageType.Fire:
        //    //        damageMod += charController.charModel.physicalSIMod;

        //    //        break;
        //    //    case DamageType.Light:
        //    //        break;
        //    //    case DamageType.Dark:
        //    //        break;
        //    //    case DamageType.Pure:
        //    //        break;
        //    //    case DamageType.Buff:
        //    //        break;
        //    //    case DamageType.Debuff:
        //    //        break;
        //    //    case DamageType.Guard:
        //    //        break;
        //    //    case DamageType.Heal:
        //    //        break;
        //    //    case DamageType.FortitudeDmg:
        //    //        break;
        //    //    case DamageType.DOT:
        //    //        break;
        //    //    case DamageType.Move:
        //    //        break;
        //    //    default:
        //    //        break;
        //    //}

        //    return damageMod;

        //}
#endregion
        void FortChgOnStrikingCritNFeeble()
        {
            if (strikeType == StrikeType.Normal)
                return;
            if (strikeType == StrikeType.Crit)
            {
                if (striker.charModel.charMode == CharMode.Ally)
                {
                    if (striker.charModel.orgCharMode == CharMode.Ally)
                        striker.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatsName.fortitude, +6);

                    foreach (CharController c in CharService.Instance.charsInPlayControllers)
                    {
                        if (c.charModel.charID != striker.charModel.charID
                            && c.charModel.orgCharMode == CharMode.Ally)
                            c.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatsName.fortitude, +3);
                    }
                }
               
            }
            if (strikeType == StrikeType.Feeble)
            {
                if (striker.charModel.charMode == CharMode.Ally)
                {
                    if (striker.charModel.orgCharMode == CharMode.Ally)
                        striker.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatsName.fortitude, -6);

                    foreach (CharController c in CharService.Instance.charsInPlayControllers)
                    {
                        if (c.charModel.charID != striker.charModel.charID
                            && c.charModel.orgCharMode == CharMode.Ally)
                            c.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatsName.fortitude, -3);
                    }
                }
            }
        }

        void FortChgOnGettingCritNFeeble()
        {
            if (strikeType == StrikeType.Normal)
                return;
            if (strikeType == StrikeType.Crit)
            {
                if (charController.charModel.charMode == CharMode.Ally)
                {
                    if (charController.charModel.orgCharMode == CharMode.Ally)
                        charController.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatsName.fortitude, -6);

                    foreach (CharController c in CharService.Instance.charsInPlayControllers)
                    {
                        if (c.charModel.charID != charController.charModel.charID
                            && c.charModel.orgCharMode == CharMode.Ally)
                            c.ChangeStat(CauseType.CriticalStrike, 0, charController.charModel.charID, StatsName.fortitude, -3);
                    }
                }
               
            }
            if (strikeType == StrikeType.Feeble)
            {
                if (charController.charModel.charMode == CharMode.Ally)
                {
                    if (charController.charModel.orgCharMode == CharMode.Ally)
                        charController.ChangeStat(CauseType.CriticalStrike, 0, charController.charModel.charID, StatsName.fortitude, +6);

                    foreach (CharController c in CharService.Instance.charsInPlayControllers)
                    {
                        if (c.charModel.charID != charController.charModel.charID
                            && c.charModel.orgCharMode == CharMode.Ally)
                            c.ChangeStat(CauseType.CriticalStrike, 0, striker.charModel.charID, StatsName.fortitude, +3);
                    }
                }
            }



        }
    
    }

}


