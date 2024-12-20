﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class FeelOfImpact : PerkBase
    {
        public override PerkNames perkName => PerkNames.FeelOfImpact;
        public override PerkType perkType => PerkType.B2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.CrazyWaves };

        public override string desc => "this is feel the impact";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.TidalWaves;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void AddTargetPos()
        {
            base.AddTargetPos();
            if (skillModel == null) return;
            skillModel.targetPos.Clear();
            CombatService.Instance.mainTargetDynas.Clear();
            CombatService.Instance.mainTargetDynas.AddRange(GridService.Instance.GetFirstDiamond(CharMode.Enemy));

            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                CellPosData cell = new CellPosData(dyna.charMode, dyna.currentPos);
                skillModel.targetPos.Add(cell);
            }
        }
        public override void PerkSelected()
        {
            base.PerkSelected();
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;
        }
        public override void ApplyFX1()
        {
            if (targetController != null)
            {
                foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
                {
                    if(dyna.currentPos == 4)
                    {
                        dyna.charGO.GetComponent<CharController>().damageController.ApplyDamage(charController
                                            , CauseType.CharSkill, (int)skillName
                                            , DamageType.Water, skillModel.damageMod+50f, skillModel.skillInclination);
                    }
                    else
                    {

                        dyna.charGO.GetComponent<CharController>().damageController.ApplyDamage(charController
                                            , CauseType.CharSkill, (int)skillName
                                            , DamageType.Water, skillModel.damageMod, skillModel.skillInclination);
                    }
                }
            }
        }

        public override void ApplyFX2()
        {
            
        }

        public override void ApplyFX3()
        {
           
        }
        public override void ApplyVFx()
        {           
        }

        public override void DisplayFX1()
        {
            str0 = "Target -> First Diamond";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "+50% Dmg vs pos 4";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX3()
        {          
        }

        public override void DisplayFX4()
        {           
        }
        public override void ApplyMoveFX()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Target -> First Diamond";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "+50% Dmg vs pos 4";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }


}

