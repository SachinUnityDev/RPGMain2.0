using Common;
using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{


    public class BandOfBandits2 : MapEbase
    {
        public override MapENames mapEName => MapENames.BandOfBanditsTwo;
        [SerializeField] Currency money2Lose;

        public override void MapEContinuePressed()
        {
            //switch (combatResult)
            //{
            //    case CombatResult.None:
            //        //start combat with bandits
            //        //save game 
            //        //transition to next combat Scene
            //        // start combat with Bandits

            //        break;
            //    case CombatResult.Victory:
            //        resultStr = "You managed to defeat the bandits and continue your journey.";
            //        mapEModel.isCompleted = true;
            //        EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
            //        MapService.Instance.pathController.pathQView.Move2NextNode();
            //        break;
            //    case CombatResult.Draw:
            //        resultStr = "You couldn't manage to defeat the bandits and had to retreat.";
            //        mapEModel.isCompleted = false;
            //        MapService.Instance.pathController.pathQView.SpawnInTown();
            //        break;
            //    case CombatResult.Defeat:
            //        resultStr = "You were defeated by the bandits and had to retreat.";
            //        mapEModel.isCompleted = false;
            //        MapService.Instance.pathController.pathQView.SpawnInTown();
            //        break;
            //    default:
            //        break;
            //}
            EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
            MapService.Instance.pathController.pathQView.Move2NextNode(true);  // UPDATE WITH COMBAT INCLUSION
        }

        public override void OnChoiceASelect()
        {
                resultStr = "Bandits ambushed you. Watch out!";
                strFX = "Party debuff: Flat Footed, 3 rds";
        }

        public override void OnChoiceBSelect()
        {
            Currency playerInv =
                    EcoService.Instance.GetMoneyAmtInPlayerInv();
            int allyCount = CharService.Instance.allyInPlayControllers.Count;

            int bronzifyCurr = playerInv.BronzifyCurrency();

            int bronzeM = bronzifyCurr / (2 * allyCount); 
            money2Lose = new Currency(0, bronzeM).RationaliseCurrency();

            EcoService.Instance.DebitPlayerInv(money2Lose); 

            resultStr =
            "While trying to run away you had to throw away a coin purse to distract them and gain time.";
            strFX = $"{money2Lose.silver} Silver and {money2Lose.bronze} Bronze lost";
        }
    }
}