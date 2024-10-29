using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
    public class RatInfestation : QuestBase, iResult
    {
        public override QuestNames questName => QuestNames.RatInfestation;

        public GameScene gameScene => GameScene.InMapInteraction;
        public override void EndQuest()
        {
            
        }   
        public override void OnObj_Completed(ObjNames objNames)
        {
            
        }
        public override void OnObj_Failed(ObjNames objNames)
        {
            
        }

        public void OnResult(Result result)
        {
            

        }

        public override void StartObj(ObjNames objName)
        {
            
        }
    }
}