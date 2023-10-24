﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    // -2 Focus	and +3 Haste
    //melee attacker is shocked for 1 rd 
    //melee attacker receives receives 1-7 air dmg   
    //lose 30 earth res 
    //immune to shock
    public class Charged : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Charged;       
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.focus, -2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.haste, +3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
               , charID, AttribName.earthRes, -30, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            int immuneBuffID = charController.charStateController
                .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
             , charID, CharStateName.Shocked, timeFrame, castTime);
            allImmunityBuffs.Add(immuneBuffID);
        }

        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "+3 Haste and -2 Focus";
            charStateCardStrs.Add(str0);

            str1 = "-30 Earth Res";
            charStateCardStrs.Add(str1);

            str2 = "Immune to <style=Air>Shocked</style>";
            charStateCardStrs.Add(str2);

            str3 = "On melee attacker:<style=Air>Shocked</style>,1 rd and\n 1-7 <style=Air>Air</style> dmg";
            charStateCardStrs.Add(str3);
        }
    }
}
