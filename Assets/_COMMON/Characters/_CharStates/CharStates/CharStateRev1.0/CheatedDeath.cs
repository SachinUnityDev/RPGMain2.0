using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class CheatedDeath : CharStatesBase
    {
        // you can cheat death once per combat (can change by skills or items or curios)	
        // Gain +%30 health and +18 Fortitude instantly(CHEAT DEATH ACTION WILL EXE THIS )
        //	-1 Fort Origin permanently (Base value)
        //	+8 Fort Origin until eoc
        public override CharStateName charStateName => CharStateName.CheatedDeath;      
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Heroes;
        public override int castTime { get; protected set;}

        public override void StateApplyFX()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.fortOrg, +8, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            charController.ChangeAttribBaseVal(CauseType.CharState, (int)charStateName, charID
                                       , AttribName.fortOrg, -1);
        }
        public override void StateApplyVFX()
        {
            
        }
        public override void StateDisplay()
        {
            str0 = "Gain 30% Hp and +18 Fort";
            charStateCardStrs.Add(str0);

            str1 = "+8 Fort Origin until eoc";
            charStateCardStrs.Add(str1);

            str2 = "-1 Fort Origin permanently";
            charStateCardStrs.Add(str2);
        }
    }
}