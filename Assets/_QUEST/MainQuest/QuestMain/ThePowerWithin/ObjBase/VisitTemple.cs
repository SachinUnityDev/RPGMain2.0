using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{


    public class VisitTemple : ObjBase
    {
        public override QuestNames questName => QuestNames.ThePowerWithin; 
        public override ObjNames objName => ObjNames.VisitTemple;
        //  "Action 1: Visit temple, talk to Minami
        //  meetMinami Dialogue
        //  Action 2: UnlockRayyan"
   
    }
}