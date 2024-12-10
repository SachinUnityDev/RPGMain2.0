using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
    public class QR1_RatInfest : QRoomBase, iResult
    {
        public override int roomNo => 1; 

        public override QuestNames questName => QuestNames.RatInfestation;

        public override ObjNames ObjNames => ObjNames.CleanseTheSewers;

        public GameScene gameScene => GameScene.QUEST; 

        public void OnResult_AfterSceneLoad(Result result)
        {
            // update interct data 
            OnPosChked();   
        }

        public void OnResultClicked(Result result)
        {
            // show result panel
            //.OnContinue();
        }

        public override void Trigger1()
        {
            base.Trigger1();
            // get Qmission Service QModel 
            float chance = 0; 
            QuestModel questModel = QuestMissionService.Instance.GetQuestModel(questName);
            switch (questModel.questMode)
            {
                case QuestMode.None:
                    chance = 0; 
                    break;
                case QuestMode.Stealth:
                    chance = 30f;
                    break;
                case QuestMode.Exploration:
                    chance = 60f; 
                    break;
                case QuestMode.Taunt:
                    chance = 90f;
                    break;
                default:
                    break;
            }
            chance = 100f; 
            if(chance.GetChance())
            {
                CombatEventService.Instance.StartCombat(CombatState.INTactics, LandscapeNames.Sewers, EnemyPackName.RatPack3, this);
            }

        }

        public override void Trigger2()
        {
            base.Trigger2();
        }

        public override void Trigger3()
        {
            base.Trigger3();
        }
    }
}