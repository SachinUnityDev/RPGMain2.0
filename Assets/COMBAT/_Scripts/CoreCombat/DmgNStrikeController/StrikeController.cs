using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{

    //public class StrikeModel  //  Deprecated 
    //{
    //    public CharController striker;
    //    public DynamicPosData target;
    //    public SkillNames skillUsed;
    //    public DamageType dmgType;
    //    public CharStateName charStateName;
    //    public float dmgValue;
    //    public StrikeModel()
    //    {

    //    }
    //    public StrikeModel(CharController striker, DynamicPosData target, SkillNames skillUsed,
    //        DamageType dmgType, float dmgValue, CharStateName charStateName)
    //    {
    //        this.striker = striker;
    //        this.target = target;
    //        this.skillUsed = skillUsed;
    //        this.dmgType = dmgType;
    //        this.dmgValue = dmgValue;
    //        this.charStateName = charStateName;
    //    }
    //}

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


        float dmgMin, dmgMax; 
        private void Start()
        {
            charController = GetComponent<CharController>();
            otherTargetDynas = new List<DynamicPosData>(); 
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
            //brows


        }

        public void AddThornsFX()
        {

        }
        public void RemoveThornsFx()
        {


        }
      

        #endregion
    }

}




