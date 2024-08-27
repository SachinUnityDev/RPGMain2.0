using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Interactables;
using Town;
using Combat;

namespace Quest
{
    public class BandOfBandits1 : MapEbase
    {
        public override MapENames mapEName => MapENames.BandOfBanditsOne;

        CharNames charJoined;
        Currency money2Lose; 
        public override void MapEContinuePressed()
        {
            CombatEventService.Instance.StartCombat(CombatState.INTactics, LandscapeNames.Sewers, EnemyPackName.RatPack3);
            //if (isCombatToBePlayed)
            //{
               

            //    //if (combatResult == CombatResult.Victory)
            //    //{
            //    //    resultStr = "You defeated the bandits!";
            //    //    strFX = "Party buff: +1 to all stats, 3 rds";
            //    //}
            //    //else if(combatResult == CombatResult.Defeat)
            //    //{
            //    //    resultStr = "You were defeated by the bandits!";
            //    //    strFX = "Party debuff: -1 to all stats, 3 rds";
            //    //}else if(combatResult == CombatResult.Draw)
            //    //{
            //    //    resultStr = "You were defeated by the bandits!";
            //    //    strFX = "Party debuff: -1 to all stats, 3 rds";
            //    //}                
            //}
            //else
            //{

            //    if (mapEResult)
            //    {
            //        resultStr = "You passed through the bandits without any trouble!";
            //        strFX = "";
            //    }
            //    else
            //    {
            //        resultStr = "You were ambushed by the bandits!";
            //        strFX = "Party debuff: Flat Footed, 3 rds";
            //    }
            //}
            //EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
            //MapService.Instance.pathController.pathQView.Move2NextNode();

        }

        public override void OnChoiceASelect()
        {
            float chance = 50f;
            if (chance.GetChance())
            {
                resultStr = "Bandits ambushed you. Watch out!";
                strFX = "Party debuff: Flat Footed, 3 rds";
                isCombatToBePlayed = true;
            }
            else
            {
                resultStr = "Time to fight!";
                strFX = "";
                isCombatToBePlayed = false;
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
    }
}