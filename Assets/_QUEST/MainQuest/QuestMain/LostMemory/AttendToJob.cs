using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Quest
{
    public class AttendToJob : QuestObjBase
    {
        public override QuestNames qMainNames => QuestNames.LostMemory; 

        public override QuestObjNames QObjNames => QuestObjNames.AttendToJob;
        //"Action 1: Go to your Job
        // Action 2: jobattended dia"
        public override void Act1()
        {
            //go to your job as in WOOD CUTTING 

        }

        public override void Act2()
        {
            //job Attended dialogue
        }

        public override void Act3()
        {
            
        }

        public override void Act4()
        {
            
        }
    }
}