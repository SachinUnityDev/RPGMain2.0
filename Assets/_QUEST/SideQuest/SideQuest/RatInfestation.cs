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

        public GameScene gameScene => GameScene.MAPINTERACT;
    

        public void OnResult(Result result)
        {

        }

        public void OnResultClicked(Result result)
        {
        }
        public override void QuestStarted()
        {
        }

        public override void Quest_Completed()
        {
        }
    }
}