using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 


namespace Combat
{
    public class WaterShell : SkillBase   // replacement for patience
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.WaterShell;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => "This Is water Shell ";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }


        public override void PopulateTargetPos()
        {  
                SelfTarget(); 
        }
 
        public override void ApplyFX1()
        {
            int stmRegen = UnityEngine.Random.Range(3, 6);
            if (targetController == null) return;
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                , AttribName.staminaRegen, stmRegen, skillModel.timeFrame, skillModel.castTime, true);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.haste
                , -3f, skillModel.timeFrame, skillModel.castTime, false);

            // healing as percentage of MAX HP
            charController.HealingAsPercentOfMaxHP( CauseType.CharSkill, (int)skillName, 12f); 

        }

        public override void ApplyFX2()
        {
            //  +% 60 Armor, 
            if (targetController == null) return;
            float statDataMin = charController.GetAttrib(AttribName.armorMin).currValue;
            float statDataMax = charController.GetAttrib(AttribName.armorMax).currValue;
         
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                , AttribName.armorMin, statDataMin * 0.6f, skillModel.timeFrame, skillModel.castTime, true);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                , AttribName.armorMax, statDataMax * 0.6f, skillModel.timeFrame, skillModel.castTime, true);
        }

        public override void ApplyFX3()
        {
            if (targetController == null) return;
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.fireRes, +14,
                skillModel.timeFrame, skillModel.castTime, true);          
        }

        public override void ApplyMoveFx()
        {
        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.RangedStrike(PerkType.None, skillModel);
        }

        public override void PopulateAITarget()
        {
        }

        public override void DisplayFX1()
        {
            str0 = "+3-5 Stm Regen and -3 Haste";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "<style=Heal>Heal</style> 12% of max Hp";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX3()
        {
            str2 = "+60% Max Armor and +14 <style=Burn>Fire Res</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX4()
        {          
        }

  
    }
}



