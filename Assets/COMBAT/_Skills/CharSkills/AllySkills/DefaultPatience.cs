using Common;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{
    public class DefaultPatience : SkillBase
    {
        public override SkillModel skillModel { get; set; }
        public override SkillNames skillName => SkillNames.DefaultPatience;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override string desc => "this is defualt patience";

     
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }



        public override void SkillInit(SkillController1 _skillController)
        {
            base.SkillInit(_skillController);
            
            //charName = _skillController.charName;
            //SkillDataSO skillDataSO = SkillService.Instance.GetSkillSO(charName);
            //skillController = _skillController;
            //charController = skillController.gameObject.GetComponent<CharController>();
            //charID = charController.charModel.charID;
            //skillData = skillDataSO.allSkills.Find(t => t.skillName == skillName);

            //skillModel = new SkillModel(skillData);
            //skillModel.skillID = skillController.skillID;
            //skillModel.charID = skillController.charID;
            ////SkillService.Instance.allSkillModels.Add(skillModel);
            //skillController.allSkillModels.Add(skillModel);
            //charGO = SkillService.Instance.GetGO4SkillCtrller(charName);
            myDyna = GridService.Instance.GetDyna4GO(charGO);
            // Debug.Log("INSIDE SKILL INIT" + skillName);
            // Do a Skill Init at the start of the combat.. 

            PopulateTargetPos();
        }

        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear();
            
            int pos = GridService.Instance.GetDyna4GO(charGO).currentPos; 
            skillModel.targetPos.Add(new CellPosData(myDyna.charMode, pos));

            CombatService.Instance.mainTargetDynas.Add(myDyna);

        }
     
        public override void ApplyFX1()
        {            
            charController.charModel.staminaRegen.currValue += 2;
        }

        public override void ApplyFX2()
        {
            if (charController)
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , StatsName.haste, -2, TimeFrame.EndOfRound, skillModel.castTime,false);
        }

        public override void ApplyFX3()
        {
        }


        public override void SkillEnd()
        {

            charController.charModel.staminaRegen.currValue -= 2;
            //charController.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.haste, 2);

        }


        public override void DisplayFX1()
        {
            str1 = $"Wait 1 turn";
            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"+2<style=Stamina> Stamina Regen </style>, {skillModel.castTime} rds";
            SkillServiceView.Instance.skillCardData.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"-2<style=Attribute> Haste </style>, 2 rds";
            SkillServiceView.Instance.skillCardData.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.MultiTargetRangeFX(PerkType.None);
        }

      

        public override void PopulateAITarget()
        {
            // add self target .. 
            SkillService.Instance.currentTargetDyna = myDyna;

        }

        public override void ApplyMoveFx()
        {
        }
    }

}
