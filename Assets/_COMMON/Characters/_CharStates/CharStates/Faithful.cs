using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{

    public class Faithful : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Faithful;

        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }

        public override StateFor stateFor => StateFor.Heroes;

        public override int castTime { get; protected set;}
        //Immune to Fortitude attacks for 3 rds
        //after 3 rds go back to origin	
        //+2 Focus, haste, Luck, Morale, Dodge and +(2-2) Armor + 20 resistances once per combat
        public override void StateApplyFX()
        {
            
        }

        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            
        }
    }
}

