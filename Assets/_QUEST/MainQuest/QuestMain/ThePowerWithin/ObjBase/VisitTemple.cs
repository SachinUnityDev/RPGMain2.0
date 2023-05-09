using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{


    public class VisitTemple : QuestObjBase
    {
        public override QuestNames qMainNames => QuestNames.ThePowerWithin; 
        public override QuestObjNames QObjNames => QuestObjNames.VisitTemple;
        //  "Action 1: Visit temple, talk to Minami
        //  meetMinami Dialogue
        //  Action 2: UnlockRayyan"
        public override void Act1()
        {
            // meetMinami Dialogue
        }

        public override void Act2()
        {
            // UnLocking Rayyan // scroll roster 
        }
        public override void Act3()
        {

        }

        public override void Act4()
        {

        }
    }
}