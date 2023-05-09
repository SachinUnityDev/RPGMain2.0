using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class CheckOutTheShips : QuestObjBase
    {
        public override QuestNames qMainNames => QuestNames.ThePowerWithin;
        public override QuestObjNames QObjNames => QuestObjNames.CheckoutTheShips; 
        //        "Action 1: Go to quest node of ""Power Within""
        //       (its direct combat) and Succeed Quest"
        public override void Act1()
        {
            // unlock a question mark in the map "Power Within"
        }
        public override void Act2()
        {
            // Win the Combat
        }
        public override void Act3()
        {
            
        }
        public override void Act4()
        {
            
        }
    }
}