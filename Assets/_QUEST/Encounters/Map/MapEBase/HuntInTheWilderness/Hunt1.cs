using Common;
using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
namespace Quest
{
    public class Hunt1 : MapEbase
    {
        public override MapENames mapEName => MapENames.Hunt1; 

        CharNames charJoined;
        Currency money2Lose;

        public override void MapEContinuePressed()
        {
            EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
            EncounterService.Instance.mapEController.ShowMapE(MapENames.Hunt2);
        }

        public override void OnChoiceASelect()
        {
            //            Continue    "Deer - MAP ENC        55%
            //Hyena pack combat        20 %
            //Nyala - MAP ENC        25 % "	
            //Go back to town

           
            if (55f.GetChance())
            {
                EncounterService.Instance.mapEController.ShowMapE(MapENames.BandOfBanditsOne);
            }
            else if (60f.GetChance())
            {
                EncounterService.Instance.mapEController.ShowMapE(MapENames.MigratoryBirds);
            }
            else
            {
                EncounterService.Instance.mapEController.ShowMapE(MapENames.BuffaloStampede);
            }

            resultStr = "You encountered a pair of deers. If you don't act fast they might run away and you will be left emptyhanded.";
            //    strFX = "Party debuff: Flat Footed, 3 rds";
            //}
            //else
            //{
            //    resultStr = "Time to fight!";
            //    strFX = "";
            //}
        }

        public override void OnChoiceBSelect()
        {
            bool hasMoney = EcoServices.Instance.HasMoney(PocketType.Inv, new Currency(3, 0));
            if (hasMoney)
                money2Lose = new Currency(3, 0);
            else
                money2Lose = EcoServices.Instance.GetMoneyAmtInPlayerInv();

            EcoServices.Instance.DebitPlayerInv(money2Lose);

            resultStr = "You agreed to pay a toll for free passage and Bandits seem symphatetic to your cause...";
            strFX = $"{money2Lose.silver} Silver and {money2Lose.bronze} Bronze lost";
        }
    }
}
