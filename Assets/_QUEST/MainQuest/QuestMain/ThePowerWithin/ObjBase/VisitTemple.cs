using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{


    public class VisitTemple : QuestObjBase
    {
        public override QuestNames qMainNames => QuestNames.ThePowerWithin; 
        public override ObjNames QObjNames => ObjNames.VisitTemple;
        //  "Action 1: Visit temple, talk to Minami
        //  meetMinami Dialogue
        //  Action 2: UnlockRayyan"
        public override void Action1()
        {
            // meetMinami Dialogue
        }

        public override void Action2()
        {
            // UnLocking Rayyan // scroll roster 
        }
        public override void Action3()
        {

        }

        public override void Action4()
        {

        }
    }
}