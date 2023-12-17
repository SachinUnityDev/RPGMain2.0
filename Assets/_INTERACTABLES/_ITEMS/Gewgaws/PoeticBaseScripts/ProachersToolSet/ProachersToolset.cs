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
        int index = -1; 
        public override void BonusFx()
        {
            QuestChange(QuestMissionService.Instance.currQuestMode);
            GameEventService.Instance.OnQuestModeChg += QuestChange;
            CombatEventService.Instance.OnSOC += SOCLuckFX;
            CombatEventService.Instance.OnSOC += OnSOC;
        }
        void OnSOC()
        {
            //Enemy Beastmen and Felines: -3 Luck
            foreach (CharController charCtrl in CharService.Instance.allCharInCombat)
            {
                if(charCtrl.charModel.raceType == RaceType.Beastmen
                    || charCtrl.charModel.cultType == CultureType.Feline
                    || charCtrl.charModel.cultType == CultureType.Canine)
                {
                    charCtrl.buffController.ApplyBuff(CauseType.PoeticSetGewgaw, (int)poeticSetName
                     , charController.charModel.charID, AttribName.luck, -3, TimeFrame.EndOfCombat, 1, false);
                }
            }
        }
        


        void SOCLuckFX()
        {
            int index =
                 charController.buffController.ApplyBuff(CauseType.PoeticSetGewgaw, (int)poeticSetName
                 , charController.charModel.charID, AttribName.luck, 3, TimeFrame.EndOfRound, 3, true);
            allBuffIds.Add(index);
        } 
        

        void QuestChange(QuestMode questMode)
        {
            if(questMode == QuestMode.Stealth)
            {
                index =  charController.skillController.ApplySkillDmgModBuff(CauseType.PoeticSetGewgaw, (int)poeticSetName
                                                     , SkillInclination.Physical, 16f, TimeFrame.Infinity, 1);
                allSkillDmgModBuffIndex.Add(index); 
            }
            else
            {
                if(index != -1)
                {
                    RemoveBonusFX(); 
                }
            }
        }
        public override void RemoveBonusFX()
        {
            base.RemoveBonusFX();
            GameEventService.Instance.OnQuestModeChg -= QuestChange;
            CombatEventService.Instance.OnSOC -= SOCLuckFX;
            CombatEventService.Instance.OnSOC -= OnSOC;
        }
    }
}