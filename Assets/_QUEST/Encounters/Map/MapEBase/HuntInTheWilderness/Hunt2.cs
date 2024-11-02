using Common;
using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
    public class Hunt2 : MapEbase
    {
        public override MapENames mapEName => MapENames.Hunt2;

        CharNames charJoined;
        Currency money2Lose;
        public override void MapEContinuePressed()
        {
            EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
            EncounterService.Instance.mapEController.ShowMapE(MapENames.Hunt3);
        }

        public override void OnChoiceASelect()
        {
            mapEResult = true; // to be corrected
            if (20f.GetChance())
            {
                // combat with Hyena pack

                resultStr = "You encountered a Hyena pack, get ready to fight!";
                strFX = "";
            }
            else if (50f.GetChance())
            {
                // Combat with Lion pack 
                resultStr = "You encountered a Lion pack, get ready to be killed!";
                strFX = "";
            }
            else
            {
                // combat with lioness pack
                resultStr = "You encountered a Lioness pack, get ready to be killed!";
                strFX = "";
            }
        }

        public override void OnChoiceBSelect()
        {
            mapEResult = false;
            MapService.Instance.pathController.pathQView.Move2TownFail();
            EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
        }
    }
}