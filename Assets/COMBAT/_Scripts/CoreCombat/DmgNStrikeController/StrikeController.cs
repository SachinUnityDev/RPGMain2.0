using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Combat
{

    public class StrikeMadeData  //  DEPRECATED  
    {
        public CharController striker;
        public List<DynamicPosData> targets; // to decide to keep or remove
        public List<CharController> targetControllers;
        public SkillNames skillUsed;
        public DamageType dmgType;
        public float dmgValue;
        public CharStateName charStateName;
        
        
        public StrikeMadeData(CharController striker, List<DynamicPosData> targets, SkillNames skillUsed,
            DamageType dmgType, float dmgValue, CharStateName charStateName)
        {
            this.striker = striker;
            this.targets = targets;
            this.skillUsed = skillUsed;
            this.dmgType = dmgType;
            this.dmgValue = dmgValue;
            this.charStateName = charStateName;
        }
    }

    [System.Serializable]
    public class StatChgData
    {
        public StatsName statName;
        public float value;

        public StatChgData(StatsName statName, float value)
        {
            this.statName = statName;
            this.value = value;
        }
    }

    public class StrikeController : MonoBehaviour
    {

        CharController charController;
        List<DynamicPosData> otherTargetDynas;
        public StrikeCharModel strikeCharModel;

        [Header("Thorns Damage related")]
        public int thornID =-1;


        float dmgMin, dmgMax; 
        private void Start()
        {
            charController = GetComponent<CharController>();
            otherTargetDynas = new List<DynamicPosData>();
            CombatEventService.Instance.OnDmgDelivered += OnDmgDeliveredTick;
        }


        public void Init()
        {
            strikeCharModel = new StrikeCharModel();

        }

        public bool FocusCheck()// Magical only .. 
        {
            // get focus statChance Data of performers focus 
            // depending on that decide ..TO be decided 

            float focusVal = charController.GetStat(StatsName.focus).currValue;
            float focusChance = 100f - charController.GetStatChance(StatsName.focus, focusVal);

            if (focusVal == 0)
            {   
                // GOT CONFUSED .. to be put in HERE .. 
                CharStatesService.Instance.ApplyCharState(gameObject,  CharStateName.Confused
                                     , charController, CauseType.StatChange, (int)StatsName.focus);
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
            SkillController skillController = SkillService.Instance.currSkillController;

            StrikeTargetNos strikeNos = skillController.allSkillBases.Find(t => t.skillName 
                                                == SkillService.Instance.currSkillName).strikeNos;
            if (strikeNos == StrikeTargetNos.Single)  
            {
                int netTargetCount = CombatService.Instance.mainTargetDynas.Count;
                if (netTargetCount > 1)
                {
                    CombatService.Instance.mainTargetDynas.Remove(SkillService.Instance.currentTargetDyna);                 
                    int random = UnityEngine.Random.Range(0, netTargetCount - 1);
                    SkillService.Instance.currentTargetDyna  = CombatService.Instance.mainTargetDynas[random]; 
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
            StatData dmg = charController.GetStat(StatsName.damage);
             dmgMin = dmg.minRange;
             dmgMax = dmg.maxRange;
            float chgMin = 0.2f * dmgMin;
            float chgMax = 0.2f * dmgMax;
            charController.ChangeStatRange(CauseType.StatChecks, (int)StatChecks.FocusCheck, charID
                , StatsName.damage,chgMin, chgMax); 
        }
        void RevertDamageRange()
        {
            charController.GetStat(StatsName.damage).minRange = dmgMin;
            charController.GetStat(StatsName.damage).maxRange = dmgMax;
        }

        public bool AccuracyCheck()// Physical 
        {
            float accVal = charController.GetStat(StatsName.acc).currValue;
            float accChance = charController.GetStatChance(StatsName.acc, accVal);

            if (accVal == 0)
            { // self inflicted
               
                CharStatesService.Instance.ApplyCharState(gameObject, CharStateName.Blinded
                                     , charController, CauseType.StatChange, (int)StatsName.acc);
                return false;// miss the target .. i.e not going to hit/FX anyone.. 
            }
            else
            {
                return accChance.GetChance();
            }           
        }


#region THORNS RELATED
        public void ApplyThornsFx()
        {
            // when ever attacked do this


        }

        public void AddThornsFXBuff(AttackType attackType, DamageType damageType, float thornsMin, float thornsMax)
        {
            thornID++;
            ThornsDmgData thornsDmgData = new ThornsDmgData(thornID, attackType, damageType, thornsMin, thornsMax);

            strikeCharModel.AddThornsDamage(thornsDmgData);

            //int currRd = CombatService.Instance.currentRound;
            //buffIndex++;
            //BuffData buffData = new BuffData(buffIndex, isBuff, currRd, timeFrame, netTime,
            //                                                      charModVal, directStr);

            //allBuffs.Add(buffData);
            //return buffIndex;
        }
        public void RemoveThornsFx(int thornID)
        {
            strikeCharModel.RemoveThornDamage(thornID);
        }

        void OnDmgDeliveredTick(DmgData dmgData)
        {             
            foreach (ThornsDmgData thornData in strikeCharModel.allThornsData )
            {
                if(thornData.attackType == dmgData.attackType)
                {
                    float dmgPercentValue = UnityEngine.Random.Range(thornData.thornsMin, thornData.thornsMax);
                    dmgData.striker.GetComponent<DamageController>()
                        .ApplyDamage(charController, CauseType.ThornsAttack, -1, thornData.damageType, dmgPercentValue, false);
                }
            }

        }

        // remove thorns that are time based 



        #endregion
    }

}




