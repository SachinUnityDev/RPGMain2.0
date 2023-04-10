using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class FeebleMinded : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Feebleminded;
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
            , charID, AttribName.waterRes, -20, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.earthRes, -20, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.darkRes, -20, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            int immuneBuffID = charController.charStateController
               .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                  , charID, CharStateName.LuckyDuck, timeFrame, castTime);
            allImmunityBuffs.Add(immuneBuffID); 
        }
        public override void StateApplyVFX()
        {

        }
        public override void StateDisplay()
        {
            str0 = "Can't use Buff or Heal skills";
            charStateCardStrs.Add(str0);
            str1 = "-20 Cold Res";
            charStateCardStrs.Add(str1);
            str2 = "Immune to<style=States> Lucky Duck</style>";
            charStateCardStrs.Add(str2);
        }
    }
}

