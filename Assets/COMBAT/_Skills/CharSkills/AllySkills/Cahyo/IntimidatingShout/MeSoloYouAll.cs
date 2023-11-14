using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class MeSoloYouAll : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.IntimidatingShout;
        public override SkillLvl skillLvl => SkillLvl.Level1;
        public override PerkSelectState state { get ;set ; }
        public override PerkNames perkName => PerkNames.MeSoloYouAll;
        public override PerkType perkType => PerkType.A1;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "Me solo you all ";        
        public override float chance { get; set; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.staminaReq = 12; 
        }

        public override void ApplyFX1()
        {
            int allyCount = GridService.Instance.GetAllByCharMode(CharMode.Ally).Count; 
            int enemyCount = GridService.Instance.GetAllByCharMode(CharMode.Enemy).Count;
            if ( allyCount == 1)
            {
                float attribDmgMin = charController.GetAttrib(AttribName.dmgMin).currValue;
                float attribDmgMax = charController.GetAttrib(AttribName.dmgMax).currValue;

                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                  , AttribName.dmgMax, attribDmgMax*0.3f, TimeFrame.EndOfCombat, skillModel.castTime, true);
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                  , AttribName.dmgMax, attribDmgMin*0.3f, TimeFrame.EndOfCombat, skillModel.castTime, true);


                float armorMin = charController.GetAttrib(AttribName.armorMin).currValue;
                float armorMax = charController.GetAttrib(AttribName.armorMax).currValue;

                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                  , AttribName.armorMax, armorMax*0.3f, TimeFrame.EndOfCombat, skillModel.castTime, true);
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                  , AttribName.armorMin, armorMin*0.3f, TimeFrame.EndOfCombat, skillModel.castTime, true);

            }
        }

        public override void ApplyFX2()
        {
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                , AttribName.morale, +2, skillModel.timeFrame, skillModel.castTime, true);
        }
        public override void ApplyFX3()
        {
            AttribData hasteAttrib = charController.GetAttrib(AttribName.haste);
            if (hasteAttrib.currValue == 12)
            {
                GridService.Instance.ShuffleCharMode(CharMode.Enemy); 
            }
        }

        public override void DisplayFX1()
        {
            str1 = $"<style=Performer>if solo, +20% Armor&Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);

            str2 = $"<style=Performer>(if Init 12, +40% Armor&Dmg)";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX2()
        {

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
    }

}

