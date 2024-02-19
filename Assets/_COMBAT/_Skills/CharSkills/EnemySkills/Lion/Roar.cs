using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{
    public class Roar : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.Roar;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override string desc => "sniff blood";
 
        public override void PopulateTargetPos()
        {
          SelfTarget(); 
        }
   
        public override void ApplyFX1()
        {
        

        }

        public override void ApplyFX2()
        {
            //if (IsTargetMyAlly())
            //{   
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.morale, 2, skillModel.timeFrame, skillModel.castTime, true);

           //Enemy party: -3-6 Fortitude
//Ally party: +2 Morale"
        }

        public override void ApplyFX3()
        {
        }

 
        public override void DisplayFX1()
        {
            str1 = "Ally party: +2 Morale";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
           
        }

        public override void DisplayFX2()
        {
            str2 = $"Enemy party: -3-6 <style=Fortitude>Fortitude</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }


  
        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.RangedStrike(PerkType.None, skillModel);
        }

        public override void PopulateAITarget()
        {

            base.PopulateAITarget();
            if (SkillService.Instance.currentTargetDyna != null) return; 
            foreach (CellPosData cell in skillModel.targetPos)
            {
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
                if (dyna != null)
                {
                    CharController targetCharCtrl = dyna.charGO.GetComponent<CharController>();

                    StatData hp = targetCharCtrl.GetStat(StatName.health);
                    if (!targetCharCtrl.charStateController.HasCharState(CharStateName.Guarded))
                    {
                        SkillService.Instance.currentTargetDyna = dyna; break;
                    }
                    else if (hp.currValue / hp.maxLimit < 0.4f)
                    {
                        SkillService.Instance.currentTargetDyna = dyna; break;
                    }
                    else
                    {
                        randomDyna = dyna;
                    }
                }
                      
            }
            if(SkillService.Instance.currentTargetDyna == null)
            {
                SkillService.Instance.currentTargetDyna = randomDyna;
            }
        }

        public override void ApplyMoveFx()
        {
        }
    }
}
