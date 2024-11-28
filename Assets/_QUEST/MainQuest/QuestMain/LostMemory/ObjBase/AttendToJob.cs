using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Quest
{
    public class AttendToJob : ObjBase
    {
        public override QuestNames questName => QuestNames.LostMemory; 

        public override ObjNames objName => ObjNames.AttendToJob;
        //"Action 1: Go to your Job
        // Action 2: jobattended dia"
     
    }
}