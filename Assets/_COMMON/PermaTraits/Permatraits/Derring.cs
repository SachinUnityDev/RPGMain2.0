using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quest;

namespace Common
{
    public class Derring : PermaTraitBase
    {
        public override PermaTraitName permaTraitName => PermaTraitName.Derring;
        public override void ApplyTrait()
        {
            GameEventService.Instance.OnQuestModeChg += ApplyOnGameModeChg; 
        }
        void ApplyOnGameModeChg(QuestMode questMode)
        {
            if (questMode != QuestMode.Taunt)
                return;
            int buffID = charController.buffController.ApplyBuff(CauseType.PermanentTrait, (int)permaTraitName,
                            charID, AttribName.morale, 2, TimeFrame.Infinity, 100, true); 
               
            allBuffIds.Add(buffID);  
        }

        public override void EndTrait()
        {
            base.EndTrait();          
            GameEventService.Instance.OnQuestModeChg -= ApplyOnGameModeChg;
        }

    }
}