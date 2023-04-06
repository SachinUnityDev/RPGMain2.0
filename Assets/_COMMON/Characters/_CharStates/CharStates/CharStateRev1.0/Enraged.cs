using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{

    public class Enraged : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Enraged;

        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }

        public override StateFor stateFor => StateFor.Mutual;

        public override int castTime { get; protected set;}

        public override void StateApplyFX()
        {
            // +3 Dodge, -1 Focus and -1 Acc
            // Dmg increases by 8 % each time attacked up to 40 %
            // gain 12 fortitude on burn
            // -30 water res
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                 , charID, AttribName.dodge, +3, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);
            
            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.focus, -1, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.acc, -1, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.waterRes, -30, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);
            CharStatesService.Instance.OnCharStateStart += ApplyBurnFX; 
       
        }
        void ApplyBurnFX(CharStateModData charStateModData)
        {
            if(charController.charModel.charID == charStateModData.effectedCharID)
                if(charStateModData.charStateName == CharStateName.BurnLowDOT ||
                    charStateModData.charStateName == CharStateName.BurnHighDOT)
             
                    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                       , StatName.fortitude, 12);
        }
        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "+3 Dodge, -1 Focus and -1 Acc";
            charStateModel.charStateCardStrs.Add(str0);

            str1 = "-30 water res";
            charStateModel.charStateCardStrs.Add(str1);

            str2 = "Dmg increases by 8 % each time attacked up to 40 %";
            charStateModel.charStateCardStrs.Add(str2);

            str3 = "Gain 12 <style=Fortitude>Fort</style> upon <style=Burn>Burning</style>";
            charStateModel.charStateCardStrs.Add(str3);
        }
        public override void EndState()
        {
            base.EndState();
            CharStatesService.Instance.OnCharStateStart -= ApplyBurnFX;
        }
    }
}

