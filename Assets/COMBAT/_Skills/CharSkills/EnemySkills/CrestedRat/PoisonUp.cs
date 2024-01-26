using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Combat
{
    public class PoisonUp : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.PoisonUp;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => "this is  poison up...";
    
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> buffsAdded = null; 

        public override void PopulateTargetPos()
        {
                skillModel.targetPos.Clear();         
                foreach (DynamicPosData dyna in GridService.Instance.allCurrPosOccupiedByDyna)
                {
                    if(dyna.charGO.GetComponent<CharController>().charModel.charName == CharNames.DireRat)
                    {
                        CellPosData cell = new CellPosData(dyna.charMode, dyna.currentPos); 
                        skillModel.targetPos.Add(cell);                     
                    }
                }          
        }

        public override void BaseApply()
        {
            base.BaseApply();
            SkillService.Instance.OnSkillUsed += PoisonBuff; 
        }
        void PoisonBuff(SkillEventData skillEventData)
         {
            if (skillEventData.strikerController.charModel.charName == CharNames.DireRat)
            {
                if (skillEventData.skillModel.skillName == SkillNames.RatBite)
                {
                    float percent = 70f;
                    if (percent.GetChance())
                    {

                        skillEventData.targetController.damageController.ApplyHighPoison(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID);
                    }
                }
            }
            return; 
        }

        public override void ApplyFX1()
        {
            if (targetController)
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                , AttribName.morale, 2f, skillModel.timeFrame, skillModel.castTime, true); 
        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }

        public override void SkillEnd()
        {
            base.SkillEnd();
            SkillService.Instance.OnSkillUsed -= PoisonBuff;
        }

        public override void DisplayFX1()
        {
            str1 = $"Adds 70%<style=Poison> High Poison </style>chance to ally Rat Bite skill, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
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
            SkillService.Instance.skillFXMoveController.MultiTargetRangeFX(PerkType.None, skillModel);
        }

        public override void PopulateAITarget()
        {
            base.PopulateAITarget();
            DynamicPosData randomDyna = null; 
            if (SkillService.Instance.currentTargetDyna == null)
            {
                CharController maxValChar = CharService.Instance.HasHighestStat(StatName.health, CharMode.Enemy);
                foreach (CellPosData cell in skillModel.targetPos)
                {
                    DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
                    if (dyna != null)
                    {
                        randomDyna= dyna;
                        CharController myCharCtrl = dyna.charGO.GetComponent<CharController>();
                        if (maxValChar.charModel.charID == myCharCtrl.charModel.charID)
                        {
                            SkillService.Instance.currentTargetDyna = dyna;
                            return;
                        }
                    }
                }
                if (SkillService.Instance.currentTargetDyna == null)
                    SkillService.Instance.currentTargetDyna = randomDyna;

            }
        }

        public override void ApplyMoveFx()
        {
        }
    }


}


