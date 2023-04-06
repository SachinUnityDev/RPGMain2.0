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
                       , charID, AttribName.acc, -2, charStateModel.timeFrame, charStateModel.castTime, true);

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

