using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{

    public class TalkToKhalid : ObjBase
    {
        public override QuestNames questName => QuestNames.LostMemory;
        public override ObjNames objName => ObjNames.TalkToKhalid;

        public override void ObjStart()
        {
            base.ObjStart();  
        }

        public override void ObjComplete()
        {
            
            
        }

    }
}
