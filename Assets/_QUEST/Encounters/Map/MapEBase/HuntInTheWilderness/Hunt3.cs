using Common;
using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
    public class Hunt3 : MapEbase
    {
        public override MapENames mapEName => MapENames.Hunt3;

        CharNames charJoined;
        Currency money2Lose;
        public override void MapEContinuePressed()
        {
            EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
            MapService.Instance.pathController.pathQView.Move2NextNode();

            // MapService.Instance.pathController.pawnTrans.GetComponent<PawnMove>().Move();
            // move the pawn 
        }
        //        "50% COMBAT xx 
        //Hyena Pack"	"50% COMBAT xx
        //Lion Pack"
        //You encountered a Hyena pack, get ready to fight!
        //    You encountered a Lion pack, get ready to be killed!
        public override void OnChoiceASelect()
        {
            int choice = UnityEngine.Random.Range(0, 101); 
            if(choice >=0 && choice < 50)
            {
                // start a Hyena Pack
                resultStr = "You encountered a Hyena pack, get ready to fight!";
            }
            else
            {
                // start a combat with lion Pack
                resultStr = "You encountered a Lion pack, get ready to be killed!";
            }
        }

        public override void OnChoiceBSelect()
        {
            MapService.Instance.pathController.pawnTrans.GetComponent<PawnMove>().Move2TownOnFail();
            EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
        }
    }
}