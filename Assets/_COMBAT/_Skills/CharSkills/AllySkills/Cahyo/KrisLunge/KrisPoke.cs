using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class KrisPoke : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.KrisLunge;

        public override SkillLvl skillLvl => SkillLvl.Level1;
        
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.KrisPoke;

        public override PerkType perkType => PerkType.A1;

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "This Is KrisPoke ";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX3;
            skillModel.castPos.Clear();
            skillModel.castPos = new List<int> { 1,2,3,4,5,6,7 };
        }
        public override void PerkSelected()
        {
            base.PerkSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX3;
        }

        public override void ApplyFX1()
        {
            if(targetController)
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                 , AttribName.haste, -2, skillModel.timeFrame, skillModel.castTime, false);
        }
       
        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {

        }
        public override void DisplayFX1()
        {
            str0 = "Cast from any pos";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "-2 Haste";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }
        public override void ApplyVFx()
        {           
        }
        public override void ApplyMoveFX()
        {
          
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Cast from any pos";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "-2 Haste";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "Move forward subtracted";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }

}

