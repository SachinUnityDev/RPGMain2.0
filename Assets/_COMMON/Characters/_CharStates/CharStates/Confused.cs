using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Confused : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Confused;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
            // -2 acc .... -3 fortitude regen ...Immune to concentrated
            // ...50% chance to hit friendly targets...50% chance to misfire 
            
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, StatsName.acc, -2, charStateModel.timeFrame, charStateModel.castTime, true);

            charController.charStateController
              .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                 , charID, CharStateName.Despaired, charStateModel.timeFrame, charStateModel.castTime);

        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "<style=States> Confused </style>";
            charStateModel.charStateCardStrs.Add(str0);
            str1 = $"-2<style=Attributes> Acc </style>";
            charStateModel.charStateCardStrs.Add(str1);
            str2 = $"-3<style=Fortitude> Fortitude </style>per rd";
            charStateModel.charStateCardStrs.Add(str2);
            str3 = "Immune to <style=States> Concentrated </style>";
            charStateModel.charStateCardStrs.Add(str3);
        }

    }
}


//        //CharController charController;
//        //public GameObject causedByGO;
//        //public override int castTime { get; set; }
//        //public override float dmgPerRound => 1.0f;
//        //public override CharStateName charStateName => CharStateName.Confused;
//        //public override StateFor stateFor => StateFor.Mutual;
//        //public int timeElapsed { get; set; }

//        //private void Start()
//        //{
//        //    charController = GetComponent<CharController>();
//        //    if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//        //        Destroy(this);
//        //    StateInit(-1, TimeFrame.None); 

//        //}

//        //public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        //{


//        //    charController.OnStatChanged += Tick2;

//        //}
//        ////StartEND : Focus 12(max) ...Focus drops below 12

//        //protected override void Tick()
//        //{



//        //}


//        //void Tick2(CharModData charModData)  //  change Stat subscribe 
//        //{
//        //    if (charModData.statModfified != StatsName.focus)
//        //        return;
//        //    if (charController.GetStat(StatsName.focus).currValue > 0)  // Exit condition 
//        //    {
//        //        EndState();

//        //    }

//        //}
//        //public override void EndState()
//        //{
//        //    charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 
//        //    charController.OnStatChanged -= Tick2;
//        //    Destroy(this);
//        //}
