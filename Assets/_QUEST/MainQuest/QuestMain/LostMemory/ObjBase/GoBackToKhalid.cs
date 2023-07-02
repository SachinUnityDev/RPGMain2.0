using System.Collections;
using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using UnityEngine;



namespace Quest
{
    public class GoBackToKhalid : QuestObjBase
    {
        public override QuestNames qMainNames => QuestNames.LostMemory;

        public override ObjNames QObjNames => ObjNames.GoBackToKhalid; 
        // "Action 1:  (choose Profession.)debtisClear dialogue
        // Action 2: Unlock Marketplace
        // forcedAction -> End day"
        public override void Action1()
        {
            // debt is clear Dialogue
        }
        public override void Action2()
        {
           // UnLock MarketPlace
        }
        public override void Action3()
        {
            // Forced action -> End Day
        }
        public override void Action4()
        {
            
        }
    }
}