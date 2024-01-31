using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Combat
{
    public class DoubleSwing : PerkBase
    {
        public override PerkNames perkName => PerkNames.DoubleSwing;

        public override PerkType perkType => PerkType.A1;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "Double Swing";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.HatchetSwing;

        public override SkillLvl skillLvl => SkillLvl.Level1; 

        public override float chance { get;set; }

        public override void AddTargetPos()
        {
            TargetAnyEnemy();
        }
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.cd = 1; 
        }

        public override void ApplyFX1()
        {
            RegainAP();
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
            str1 = "Target -> anyone";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Regain AP";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
      
        public override void DisplayFX3()
        {
           
        }

        public override void DisplayFX4()
        {
           
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Target -> anyone";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Regain AP";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Cd: 0 -> 1";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }


}

//if (skillModel != null)
//{
//    skillModel.targetPos.Clear();
//    CombatService.Instance.mainTargetDynas.Clear();
//    for (int i = 1; i < 8; i++)
//    {
//        CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
//        DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
//        if (dyna != null)
//        {
//            CombatService.Instance.mainTargetDynas.Add(dyna);
//            skillModel.targetPos.Add(cellPosData);
//        }
//    }
//}

