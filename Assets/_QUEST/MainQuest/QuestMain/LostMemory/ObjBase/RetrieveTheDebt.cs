using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Quest
{
    public class RetrieveTheDebt : QuestObjBase
    {
        public override QuestNames qMainNames => QuestNames.LostMemory; 

        public override ObjNames QObjNames => ObjNames.RetrieveTheDebt;
//        "Trigger:  meetKhalid dialogue (choose CharClass)
//Action 1: Unlock Tavern
//Action 2: Talk to Greybrow: meetGreybrow
//Action 3: retrieveDebt "
        public override void Action1()
        {
            // unlock tavern

        }

        public override void Action2()
        {
            // talk to greybrow: unlock Meet Abbas and GreyBrow(Dialogue)

        }

        public override void Action3()
        {
            // retrieve Debt : Abbas and Tahir

        }

        public override void Action4()
        {
            
        }
    }
}