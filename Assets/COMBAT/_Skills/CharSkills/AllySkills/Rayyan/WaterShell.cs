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
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "This Is water Shell ";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }


        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();
            int pos = GridService.Instance.GetDyna4GO(charGO).currentPos;
            skillModel.targetPos.Add(new CellPosData(myDyna.charMode, pos));
        }
 
        public override void ApplyFX1()
        {
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.staminaRegen
                , 3f, skillModel.timeFrame, skillModel.castTime, true);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.haste
                , -3f, skillModel.timeFrame, skillModel.castTime, false);

            // healing as percentage of MAX HP
            charController.damageController.HealingAsPercentOfMaxHP(charController, CauseType.CharSkill, (int)skillName, 12f); 

        }

        public override void ApplyFX2()
        {
            //  +% 60 Armor, 
            float statDataMin = charController.GetAttrib(AttribName.armorMin).currValue;
            float statDataMax = charController.GetAttrib(AttribName.armorMax).currValue;
         
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                , AttribName.armorMin, statDataMin*0.6f, skillModel.timeFrame, skillModel.castTime, true);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                , AttribName.armorMin, statDataMax * 0.6f, skillModel.timeFrame, skillModel.castTime, true);
        }

        public override void ApplyFX3()
        {
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.fireRes, +14,
                skillModel.timeFrame, skillModel.castTime, true);          
        }

        public override void ApplyMoveFx()
        {


        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.SingleTargetRangeStrike(PerkType.None);


        }

        public override void PopulateAITarget()
        {
        }

        public override void DisplayFX1()
        {
            str0 = $"2 -3 <style=Attributes>Stamina regen</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $"-3 <style=Attributes>Haste</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX3()
        {
            str2 = $"4-10 <style=Attributes>Heal</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
            str3 = $"+14 <style=Attributes>Elemental Res</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
            str0 = $"Ignore <style=Attributes>Stamina Dmg</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
        }

  
    }
}



