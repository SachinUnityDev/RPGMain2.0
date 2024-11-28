using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Quest
{
    public class GoBackToKhalid : ObjBase
    {
        public override QuestNames questName => QuestNames.LostMemory;

        public override ObjNames objName => ObjNames.GoBackToKhalid; 
        // "Action 1:  (choose Profession.)debtisClear dialogue
        // Action 2: Unlock Marketplace
        // forcedAction -> End day"
        
    }
}