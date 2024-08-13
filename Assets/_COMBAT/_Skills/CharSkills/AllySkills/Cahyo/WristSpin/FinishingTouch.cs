using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class FinishingTouch : PerkBase
    {
        public override PerkNames perkName => PerkNames.FinishingTouch;
        public override PerkType perkType => PerkType.B2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.DeepCut };

        public override string desc => "+40% dmg to bleeding enemies";

        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.WristSpin;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void PerkSelected()
        {
            base.PerkSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;
        }

        public override void ApplyFX1()
        {
            StatData statData = targetController.GetStat(StatName.health);
            float hpPercent = statData.currValue / statData.maxLimit;
            if (targetController)
                if (hpPercent < 0.3f)
                {
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                     , DamageType.Physical, skillModel.damageMod
                                                     ,skillModel.skillInclination, false, true);
                }
                else
                {
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                     , DamageType.Physical, skillModel.damageMod, skillModel.skillInclination);
                }
        }

        public override void ApplyFX2()
        {
            bool isTargetBleeding = targetController.charStateController.HasCharState(CharStateName.Bleeding);

            if (isTargetBleeding)
            {
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                , DamageType.Physical, (skillModel.damageMod + 40), skillModel.skillInclination);
            }
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
            str0 = $"True strike vs enemy with < 30% Hp";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = $"+40 Dmg vs <style=Bleed>Bleeding</style>";
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
            perkDesc = "True strike vs enemy with < 30% Hp";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            
            perkDesc = "+40 Dmg vs <style=Bleed>Bleeding</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}


