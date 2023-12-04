using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class ConcentratedFist : PerkBase
    {
        public override PerkNames perkName => PerkNames.ConcentratedFist;
        public override PerkType perkType => PerkType.B1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None};
        public override string desc => "Target single any enemy,/n 180% --> 240% Water Soaked, 6 rds /n (If Focus 12, 280% Water)";
        public override CharNames charName => CharNames.Rayyan;
        public override SkillNames skillName => SkillNames.FistOfWater;
        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void AddTargetPos()
        {
            if (skillModel != null)
            {
                skillModel.targetPos.Clear();
                CombatService.Instance.mainTargetDynas.Clear();
                for (int i = 1; i < 8; i++)
                {
                    CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
                    DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                    if (dyna != null)
                    {
                        CombatService.Instance.mainTargetDynas.Add(dyna);
                        skillModel.targetPos.Add(cellPosData);
                    }
                }
            }
        }

        public override void SkillHovered()
        {
            skillModel.cd = 4;
            skillModel.maxUsagePerCombat = -1; 
            AttribData statData = charController.GetAttrib(AttribName.focus);
            if (statData.currValue == 12f)            
                skillModel.damageMod = 300f;            
            else            
                skillModel.damageMod = 240f;
        }
        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX2;
        }

        public override void ApplyFX1()
        {
            if(targetController)
            targetController.damageController
                                       .ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Water
                                                        , skillModel.damageMod, skillModel.skillInclination, true);
        }

        public override void ApplyFX2()
        {
            if (targetController)
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                   , charController.charModel.charID, CharStateName.Soaked, skillModel.timeFrame, skillModel.castTime);
        }

        public override void ApplyFX3()
        {
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }

        public override void DisplayFX1()
        {
            str0 = "Target -> single enemy";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "If Focus 12: 300% <style=Water>Water</style> Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "<style=Water>Water</style> Dmg: 160% -> 240%";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Target -> single enemy";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "If Focus 12: 300% <style=Water>Water</style> Dmg";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }
}

