using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class BuffaloStampede : MapEbase
    {
        public override MapENames mapEName => MapENames.BuffaloStampede; 

        public override void MapEContinuePressed()
        {

        }

        public override void OnChoiceASelect()
        {
            float chance = 0f;
            if (chance.GetChance())
            {
                resultStr = "You managed to taked couple of them down and the rest is gravy...";
                strFX = "Loot gained";
            }
            else
            {
                foreach (CharController c in CharService.Instance.allyInPlayControllers)
                {
                    c.buffController.ApplyBuff(CauseType.MapEncounter, (int)mapEName, -1, AttribName.vigor
                                , -1, TimeFrame.EndOfQuest, 1, false);
                }


                resultStr = "Perhaps it was not a good idea to have this dangerous ride afterall...";
                strFX = "Party debuff: -1 Vigor until eoq";
            }
        }

        public override void OnChoiceBSelect()
        {
            float chance = 60f;
            if (chance.GetChance())
            {
                foreach (CharController c in CharService.Instance.allyInPlayControllers)
                {
                    c.buffController.ApplyBuff(CauseType.MapEncounter, (int)mapEName, -1, AttribName.willpower
                                , 2, TimeFrame.EndOfQuest, 1, true);
                }
                resultStr = "The feel of adventure seems to have raised the spirit of your party";
                strFX = "Party buff: +2 Wp until eoq";
            }
            else
            {
                foreach (CharController c in CharService.Instance.allyInPlayControllers)
                {
                    c.buffController.ApplyBuff(CauseType.MapEncounter, (int)mapEName, -1, AttribName.haste
                                , -1, TimeFrame.EndOfQuest, 1, false);
                }
                resultStr = "You just wasted time, nothing else.";
                strFX = "Party debuff: -1 Haste until eoq";
            }
        }
    }
}