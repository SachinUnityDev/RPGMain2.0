using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Quest
{
    public class AttendToJob : QuestObjBase
    {
        public override QuestNames qMainNames => QuestNames.LostMemory; 

        public override ObjNames QObjNames => ObjNames.AttendToJob;
        //"Action 1: Go to your Job
        // Action 2: jobattended dia"
        public override void Action1()
        {
            //go to your job as in WOOD CUTTING 

        }

        public override void Action2()
        {
            //job Attended dialogue
        }

        public override void Action3()
        {
            
        }

        public override void Action4()
        {
            
        }
    }
}