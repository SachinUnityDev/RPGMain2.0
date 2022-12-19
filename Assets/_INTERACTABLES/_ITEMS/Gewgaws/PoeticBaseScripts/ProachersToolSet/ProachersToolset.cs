using Combat;
using Common;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    public class ProachersToolset : PoeticSetBase
    {
        public override PoeticSetName poeticSetName => PoeticSetName.PoachersToolset;
       //  On Stealth Mode: Gain +16% Dmg
       //  Enemy Beastmen and Felines: -3 Luck	
       //  +3 luck vs Rooted targets
        public override void BonusFx()
        {
            QuestChange(QuestService.Instance.questMode);
            QuestEventService.Instance.OnQuestModeChange += QuestChange; 
        }


        void SOCLuckFX()
        {
          // get your enemy party - luck

        } 
        

        void QuestChange(QuestMode questMode)
        {
            int index =
                  charController.strikeController.ApplyDmgAltBuff(16, CauseType.PoeticSetGewgaw, (int)poeticSetName
                , charController.charModel.charID, TimeFrame.Infinity, -1, true);
            dmgAltBuffIndex.Add(index);
        }
        public override void RemoveBonusFX()
        {
            dmgAltBuffIndex.ForEach(t => charController.strikeController.RemoveDmgBuff(t));
            dmgAltBuffIndex.Clear();
        }
    }
}