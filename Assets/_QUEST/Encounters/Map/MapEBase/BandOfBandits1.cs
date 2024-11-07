using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Interactables;
using Town;
using Combat;
using UnityEngine.SceneManagement;

namespace Quest
{
    public class BandOfBandits1 : MapEbase, iResult
    {
        public override MapENames mapEName => MapENames.BandOfBanditsOne;

        public GameScene gameScene => GameScene.InMapInteraction;

       

        CharNames charJoined;
        Currency money2Lose; 
        public override void MapEContinuePressed()
        {
            if (isCombatToBePlayed )
            {
                if (!isCombatResult)
                {
                    CombatEventService.Instance.StartCombat(CombatState.INTactics, LandscapeNames.Sewers, EnemyPackName.RatPack3, this);
                }
                else
                {
                    if (mapEResult)
                    {
                        MapService.Instance.pathController.pathQView.Move2NextNode(mapEResult);
                    }
                    else
                    {
                        MapService.Instance.pathController.pathQView.Move2TownFail();
                    }
                    EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);                    
                }                 
            }
            else
            {
                mapEResult = true;    
                EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
                MapService.Instance.pathController.pathQView.Move2NextNode(mapEResult);
            }

        

        }

        public override void OnChoiceASelect()
        {
            float chance = 0f;
            if (chance.GetChance())
            {
                resultStr = "Bandits ambushed you. Watch out!";
                strFX = "Party debuff: Flat Footed, 3 rds";
                isCombatToBePlayed = false;
            }
            else
            {
                resultStr = "Time to fight!";
                strFX = "";
                isCombatToBePlayed = true;
            }
        }

        public override void OnChoiceBSelect()
        {
            bool hasMoney = EcoService.Instance.HasMoney(PocketType.Inv, new Currency(3,0));
            if (hasMoney)
            {
                money2Lose = new Currency(3, 0);               
            }            
            else
            {
                 money2Lose = EcoService.Instance.GetMoneyAmtInPlayerInv(); 
            }
            isCombatToBePlayed = false;
            EcoService.Instance.DebitPlayerInv(money2Lose);

            resultStr = "You agreed to pay a toll for free passage and Bandits seem symphatetic to your cause...";
            strFX = $"{money2Lose.silver} Silver and {money2Lose.bronze} Bronze lost";        
        }

        public void OnResult(Result result)
        {
            isCombatToBePlayed = false;

            if (result == Result.Victory)
            {
                resultStr = "You defeated the bandits!";
                strFX = "Party buff: +1 to all stats, 3 rds";
                mapEResult = true;
            }
            else if (result == Result.Defeat)
            {
                resultStr = "You were defeated by the bandits!";
                strFX = "Party debuff: -1 to all stats, 3 rds";
                mapEResult = false;
            }
            else if (result == Result.Draw)
            {
                resultStr = "You were defeated by the bandits!";
                strFX = $"<b>Party debuff:</b> -1 to all stats, 3 rds";
                mapEResult = false;
            }
            // TownScene Once it is loaded, Open Map Encounter view 
            // then load the Map Encounter view with the current Map Encounter  
            SceneMgmtService.Instance.LoadGameScene(GameScene.InMapInteraction);
            SceneManager.activeSceneChanged += ShowResult;

        }

        void ShowResult(Scene NewScene, Scene old)
        {
            Debug.Log("SCENE LOADED");
            MapService.Instance.LoadView(); 
          //  EncounterService.Instance.mapEController.ShowMapEResult2(this);
            SceneManager.activeSceneChanged += ShowResult;
        }

    }
}