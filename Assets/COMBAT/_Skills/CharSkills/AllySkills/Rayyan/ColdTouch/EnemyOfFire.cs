using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class EnemyOfFire : PerkBase
    {
        public override PerkNames perkName => PerkNames.EnemyOfFire;
        public override PerkType perkType => PerkType.B2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.FringesOfIce };
        public override string desc => "(PR : B1) /n + 30 Fire Res Enemies who cast Fire Skills on ally /n, receive 6-12 Water dmg";
        public override CharNames charName => CharNames.Rayyan;
        public override SkillNames skillName => SkillNames.ColdTouch;
        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillFXRemove += skillController.allPerkBases
                       .Find(t => t.skillName == skillName && t.skillLvl == SkillLvl.Level1).WipeFX1;
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allPerkBases
                        .Find(t => t.skillName == skillName && t.skillLvl == SkillLvl.Level1).RemoveFX1;
        }


        public override void ApplyFX1()
        {
            if (targetController != null  && IsTargetAlly())
            {
                targetController.strikeController.AddThornsBuff(DamageType.Water, 5, 8
                                               , skillModel.timeFrame, skillModel.castTime);

                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.fireRes, 20f, skillModel.timeFrame, skillModel.castTime, true); 
            }
        }
  
        public override void ApplyFX2()
        {
            if (targetController != null && IsTargetAlly())
                targetController.charStateController.ClearDOT(CharStateName.BurnLowDOT); 
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
            str1 = "+20<style=Fire> Fire Res</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Clear <style=Fire>Burn</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
            str3 = "Apply Thorns 5-8<style=Water> Water</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }
        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "+20<style=Fire> Fire Res</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Clear <style=Fire>Burn</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Thorns -> 5-8<style=Water> Water</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }
}

