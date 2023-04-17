using System.Collections;
using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using UnityEngine;



namespace Quest
{
    public class RetrieveTheDebt : QuestObjBase
    {
        public override QuestNames qMainNames => QuestNames.LostMemory; 

        public override QuestObjNames QObjNames => QuestObjNames.RetrieveTheDebt;
//        "Trigger:  meetKhalid dialogue (choose CharClass)
//Action 1: Unlock Tavern
//Action 2: Talk to Greybrow: meetGreybrow
//Action 3: retrieveDebt "
        public override void Act1()
        {
            // unlock tavern

        }

        public override void Act2()
        {
            // talk to greybrow: unlock Meet Abbas and GreyBrow(Dialogue)

        }

        public override void Act3()
        {
            // retrieve Debt : Abbas and Tahir

        }

        public override void Act4()
        {
            
        }
    }
}