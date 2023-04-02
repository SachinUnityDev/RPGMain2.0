using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Soaked : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Soaked;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
        
            //-2 morale.... immune to burning...	+24 fire res....-40 air res
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, StatsName.morale, -2, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                     , charID, StatsName.fireRes, +24, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                     , charID, StatsName.airRes, -40, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

        
            List<int> immuneDOTBuffIDs =     
            charController.charStateController
                .ApplyDOTImmunityBuff(CauseType.CharState, (int)charStateName
                   , charID, CharStateName.BurnHighDOT, charStateModel.timeFrame, charStateModel.castTime, false);

            allImmunityBuffs.AddRange(immuneDOTBuffIDs);
        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "-2 Morale";
            charStateModel.charStateCardStrs.Add(str0);

            str1 = "+24 Fire Res and -40 Air Res";
            charStateModel.charStateCardStrs.Add(str1);

            str2 = "Immune to <style=Burn> Burning </style>";
            charStateModel.charStateCardStrs.Add(str2);
        }
    }
}
