using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
    public class MigratoryBirds : MapEbase
    {
        public override MapENames mapEName => MapENames.MigratoryBirds;

        public override void MapEContinuePressed()
        {
            EncounterService.Instance.mapEController.On_MapEComplete(mapEName, mapEResult);
            MapService.Instance.pathController.pathQView.Move2NextNode(mapEResult);
        }

        public override void OnChoiceASelect()
        {
            float chance = 0f;
            if (chance.GetChance())
            {
                

                resultStr = "Time to pick up the loot from little birds.";
                strFX = "Loot gained"; 
            }
            else
            {
                foreach (CharController c in CharService.Instance.allyInPlayControllers)
                {
                    c.buffController.ApplyBuff(CauseType.MapEncounter, (int)mapEName, -1, AttribName.willpower
                                , -1, TimeFrame.EndOfQuest, 1, false); 
                }
                
                resultStr = "You missed all your shoots and now you feel disappointed.";
                strFX = "Party debuff: -1 Wp until eoq"; 
            }
        }

        public override void OnChoiceBSelect()
        {
            float chance = 60f;
            if (chance.GetChance())
            {
                foreach (CharController c in CharService.Instance.allyInPlayControllers)
                {
                    c.buffController.ApplyBuff(CauseType.MapEncounter, (int)mapEName, -1, AttribName.haste
                                , 1, TimeFrame.EndOfQuest, 1, true);
                }
                resultStr = "You feel swift and free as the birds...";
                strFX = "Party buff: +1 Haste until eoq";
            }
            else
            {
                foreach (CharController c in CharService.Instance.allyInPlayControllers)
                {
                    c.buffController.ApplyBuff(CauseType.MapEncounter, (int)mapEName, -1, AttribName.focus
                                , -1, TimeFrame.EndOfQuest, 1, false);
                }
                resultStr = "You look pretty confused as you try to observe their flying patterns.";
                strFX = "Party debuff: -1 Focus until eoq";
            }
        }
    }
}