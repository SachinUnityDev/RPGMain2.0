using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Common;
using Interactables;
using Town;

namespace Quest
{
    public class BandOfBandits1 : MapEbase
    {
        public override MapENames mapEName => MapENames.BandOfBanditsOne;

        CharNames charJoined;
        Currency money2Lose; 
        public override void MapEContinuePressed()
        {
            EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
            MapService.Instance.pathController.pawnTrans.GetComponent<PawnMove>().Move();
        }

        public override void OnChoiceASelect()
        {
            float chance = 50f;
            if (chance.GetChance())
            {
                resultStr = "Bandits ambushed you. Watch out!";
                strFX = "Party debuff: Flat Footed, 3 rds";
            }
            else
            {
                resultStr = "Time to fight!";
                strFX = "";
            }
        }

        public override void OnChoiceBSelect()
        {
            bool hasMoney = EcoService.Instance.HasMoney(PocketType.Inv, new Currency(3,0));
            if (hasMoney)            
                money2Lose = new Currency(3,0);             
            else            
                money2Lose = EcoService.Instance.GetMoneyAmtInPlayerInv(); 
            
            EcoService.Instance.DebitPlayerInv(money2Lose); 
            
            resultStr = "You agreed to pay a toll for free passage and Bandits seem symphatetic to your cause...";
            strFX = $"{money2Lose.silver} Silver and {money2Lose.bronze} Bronze lost";        
        }
    }
}