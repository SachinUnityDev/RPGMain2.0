﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class HeadToss : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.HeadToss;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => "Head toss";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;

            skillModel.targetPos.Clear();

            CharMode oppCharMode = charController.charModel.charMode.FlipCharMode(); 
            for (int i = 1; i < 5; i++)
            {
                CellPosData cellPosData = new CellPosData(oppCharMode, i);
                DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                if (dyna != null)
                {
                    skillModel.targetPos.Add(cellPosData);
                }
            }
        }
   
        public override void ApplyFX1()
        {        
            if (targetController)
                targetController.damageController.ApplyDamage(charController,CauseType.CharSkill, (int)skillName
                                            , DamageType.Physical, skillModel.damageMod, skillModel.skillInclination, false);
        }

        public override void ApplyFX2()
        {         
            if (targetController)
            {
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.focus, -2, TimeFrame.EndOfRound, skillModel.castTime, false);

               // focuschg = true;
            }
        }

        public override void ApplyFX3()
        {
        }

        public override void DisplayFX1()
        {            
            str1 = $"{skillModel.damageMod}%<style=Physical> Physical </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"-2<style=Attributes> Focus </style>, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.MeleeStrike(PerkType.None, skillModel);
        }

        public override void PopulateAITarget()
        {
        }

        public override void ApplyMoveFx()
        {
        }
    }



}
