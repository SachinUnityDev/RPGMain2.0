using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class CheckOutTheShips : QuestObjBase
    {
        public override QuestNames qMainNames => QuestNames.ThePowerWithin;
        public override ObjNames QObjNames => ObjNames.CheckoutTheShips; 
        //        "Action 1: Go to quest node of ""Power Within""
        //       (its direct combat) and Succeed Quest"
        public override void Action1()
        {
            // unlock a question mark in the map "Power Within"
        }
        public override void Action2()
        {
            // Win the Combat
        }
        public override void Action3()
        {
            
        }
        public override void Action4()
        {
            
        }
    }
}