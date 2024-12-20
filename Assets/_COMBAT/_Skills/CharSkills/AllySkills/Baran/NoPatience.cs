﻿using Common;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Combat
{
    public class NoPatience : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.NoPatience;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => "No patience";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        float  StackAmt = 0; 
        bool isAPRewardGained = false;    
        public override void PopulateTargetPos()
        {          
            SelfTarget(); 
        }
        public override void BaseApply()
        {
            base.BaseApply();
            SkillService.Instance.OnSkillUsed -= HeadTossRegainAP;
            SkillService.Instance.OnSkillUsed += HeadTossRegainAP;
            CombatEventService.Instance.OnEOR1 -= ResetReward;
            CombatEventService.Instance.OnEOR1 += ResetReward;

        }
        void ResetReward(int rd)
        {
            isAPRewardGained = false;
        }

        void HeadTossRegainAP(SkillEventData skilleventData)
        {
            if (70f.GetChance())
            {
                if (skilleventData.skillName == SkillNames.HeadToss && !isAPRewardGained)
                {
                    charController.combatController.IncrementAP();
                    isAPRewardGained = true;
                    SkillService.Instance.OnSkillUsed -= HeadTossRegainAP;
                }
            }
        }

    
        public override void ApplyFX1()
        {         
            charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatName.fortitude, +5, false);
        }

        public override void ApplyFX2()
        {
            DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(CharMode.Ally, 1); 
            if(dyna != null)
            {
                GridService.Instance.gridController.SwapPos(myDyna, dyna); 

            }else
            {
                GridService.Instance.gridController.Move2Pos(myDyna, 1);
            }
        }

        public override void ApplyFX3()
        {
            if (StackAmt <= 3)
            {
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                , AttribName.willpower, +2, TimeFrame.EndOfCombat, skillModel.castTime, true);
                StackAmt++;
            }
            else if(StackAmt > 3)
            {
                StackAmt= 0;
            }
        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.MeleeStrike(PerkType.None, skillModel);

        }

        public override void PopulateAITarget()
        {
        }

        public override void DisplayFX1()
        {
            str0 = $"+2 Wp until eoc, stacks up to 6";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $"+5 <style=Fortitude>Fortitude</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);           
        }

        public override void DisplayFX3()
        {
            str2 = $"<style=Move>Move </style>to pos 1";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX4()
        {   
            str3 = $"On use Head Toss this turn: 70% Regain AP";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }

        public override void ApplyMoveFx()
        {
        }
    }



}
