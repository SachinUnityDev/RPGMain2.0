using Combat;
using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Quest
{
    public class QR2_RatInfest : QRoomBase, iResult
    {
        public override int roomNo => 2;

        public override QuestNames questName => QuestNames.RatInfestation;

        public override ObjNames ObjNames => ObjNames.CleanseTheSewers;

        public GameScene gameScene => GameScene.QUEST; 

        public void OnResult_AfterSceneLoad(Result result)
        {
          // QRoomService.Instance.
        }

        public void OnResultClicked(Result result)
        {

        }

        public override void Trigger1()
        {
            Debug.Log("TRIGERR@1");
            base.Trigger1();

            CombatEventService.Instance.StartCombat(CombatState.INTactics, LandscapeNames.Sewers, EnemyPackName.RatPack3, this);
        }

        public override void Trigger2()
        {
            Debug.Log("TRIGERR@2");
            base.Trigger2();

            CombatEventService.Instance.StartCombat(CombatState.INTactics, LandscapeNames.Sewers, EnemyPackName.RatPack3, this);

        }

        public override void Trigger3()
        {
            base.Trigger3();

        }
    }
}
