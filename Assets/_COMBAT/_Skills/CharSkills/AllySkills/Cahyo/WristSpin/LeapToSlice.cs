using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;


namespace Combat
{
    public class LeapToSlice : PerkBase
    {
        public override PerkNames perkName => PerkNames.LeapToSlice;

        public override PerkType perkType => PerkType.B3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.DeepCut };

        public override string desc => "(PR: Deep Cut) /n * Can hit anyone from any spot. /n *now 7 stamina cost /n*+25% damage if target is at pos. 5,6,7. /n *+50% dmg if both self and target at 7.";

        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.WristSpin;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void AddTargetPos()
        {
            TargetAnyEnemy();
        }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.castPos.Clear();
            skillModel.castPos = new List<int> { 1,2,3,4,5,6,7};

        }

        public override void PerkSelected()
        {
            base.PerkSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1; 

        }

        public override void BaseApply()
        {
            base.BaseApply();

            skillModel.staminaReq = 7;

        }
        public override void ApplyFX1()
        {
            // if positions 5 67 
            // if position 4
            DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO); 

            if (targetDyna.currentPos == 5 || targetDyna.currentPos == 6 || targetDyna.currentPos == 7)
            {
                if(currDyna.currentPos ==7 && targetDyna.currentPos ==7)
                {
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Physical
                                               , skillModel.damageMod +50f, skillModel.skillInclination);
                }
                else
                {
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Physical
                                             , skillModel.damageMod + 25f, skillModel.skillInclination);
                }             
            }        
            else
            {
                targetController.damageController.ApplyDamage(charController,CauseType.CharSkill, (int)skillName, DamageType.Physical
                                               , skillModel.damageMod, skillModel.skillInclination);
            }
        }

        public override void ApplyFX2()
        {
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
            str0 = "Can hit anyone from any pos";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "+25% Dmg vs pos 5,6,7";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX3()
        {
            str2 = "+50% Dmg vs pos 7 if self at 7";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Can hit anyone from any pos";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "+25% Dmg vs pos 5,6,7";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "+50% Dmg vs pos 7 if self at 7";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "Stm cost: 5 -> 7";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }


}


