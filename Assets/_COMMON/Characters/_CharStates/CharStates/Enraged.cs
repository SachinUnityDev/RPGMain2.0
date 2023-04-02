﻿using System.Collections;
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
                 , charID, StatsName.dodge, +3, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);
            
            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, StatsName.focus, -1, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, StatsName.acc, -1, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, StatsName.waterRes, -30, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);
            CharStatesService.Instance.OnCharStateStart += ApplyBurnFX; 
       
        }
        void ApplyBurnFX(CharStateData charStateData)
        {
            if(charController.charModel.charID == charStateData.charStateModel.effectedCharID)
                if(charStateData.charStateModel.charStateName == CharStateName.BurnLowDOT ||
                    charStateData.charStateModel.charStateName == CharStateName.BurnHighDOT)
             
                    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                       , StatsName.fortitude, 12);
        }
        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "+3 Dodge, -1 Focus and -1 Acc";
            charStateModel.charStateCardStrs.Add(str0);

            str1 = "Dmg increases by 8 % each time attacked up to 40 %";
            charStateModel.charStateCardStrs.Add(str1);

            str2 = "Gain 12 <style=Fortitude>Fort</style> upon <style=Burn>Burning</style>";
            charStateModel.charStateCardStrs.Add(str2);
        }
        public override void EndState()
        {
            base.EndState();
            CharStatesService.Instance.OnCharStateStart -= ApplyBurnFX;
        }
    }
}

