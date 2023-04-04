using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Shocked : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Shocked;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
            //-3 Focus.... immune to Poison...	+24 earth res....+1-3 armor....
            //.Cannot use move skill Incli

           int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, AttribName.focus, -3, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);
            
            buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                     , charID, AttribName.earthRes, +24, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = 
            charController.buffController.ApplyBuffOnRange(CauseType.CharState, (int)charStateName
                       , charID, AttribName.armor, +1, +3, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            List<int> immuneBuffIDs = charController.charStateController
               .ApplyDOTImmunityBuff(CauseType.CharState, (int)charStateName
                  , charID, CharStateName.PoisonedHighDOT, charStateModel.timeFrame, charStateModel.castTime, false);

            allImmunityBuffs.AddRange(immuneBuffIDs);
        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "-3 Focus";
            charStateModel.charStateCardStrs.Add(str0);

            str1 = "+1-2 Armor";
            charStateModel.charStateCardStrs.Add(str1);

            str2 = "+24 Earth Res";
            charStateModel.charStateCardStrs.Add(str2);

            str3 = "Immune to <style=Poison> Poisoned </style>";
            charStateModel.charStateCardStrs.Add(str3);

            str4 = "Can not use<style=Move> Move </style>skills";
            charStateModel.charStateCardStrs.Add(str4);
        }
    }
}