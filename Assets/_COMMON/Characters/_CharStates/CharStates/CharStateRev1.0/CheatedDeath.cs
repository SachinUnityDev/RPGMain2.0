using System.Collections;
using System.Collections.Generic;
using Town;
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
        public override StateFor stateFor => StateFor.Heroes;
        public override int castTime { get; protected set;}
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            // fort  Org changes
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                        , charID, AttribName.fortOrg, +8, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

        }
        public override void StateApplyVFX()
        {
            
        }
        public override void StateDisplay()
        {
            str1 = "+8 Fort Origin until eoc";
            charStateCardStrs.Add(str1);
        }
    }
}