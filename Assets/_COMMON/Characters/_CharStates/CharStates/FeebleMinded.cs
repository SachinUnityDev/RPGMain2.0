using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class FeebleMinded : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Feebleminded;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
            // Can't use Buff or Heal skills	
            //	-20 Cold Res (water earth dark)	
            // ...Immune to LuckyDuck...

            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, StatsName.waterRes, -20, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, StatsName.earthRes, -20, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, StatsName.darkRes, -20, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            int immuneBuffID = charController.charStateController
               .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                  , charID, CharStateName.LuckyDuck, charStateModel.timeFrame, charStateModel.castTime);
            allImmunityBuffs.Add(immuneBuffID); 
        }
        public override void StateApplyVFX()
        {

        }
        public override void StateDisplay()
        {
            str0 = "Can't use Buff or Heal skills";
            charStateModel.charStateCardStrs.Add(str0);
            str1 = "-20 Cold Res";
            charStateModel.charStateCardStrs.Add(str1);
            str2 = "Immune to<style=States> Lucky Duck</style>";
            charStateModel.charStateCardStrs.Add(str2);
        }
    }
}

