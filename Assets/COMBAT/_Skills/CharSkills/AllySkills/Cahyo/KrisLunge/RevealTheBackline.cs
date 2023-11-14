using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class RevealTheBackline : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.KrisLunge;

        public override SkillLvl skillLvl => SkillLvl.Level1;
        public override PerkSelectState state { get;set; }
        public override PerkNames perkName => PerkNames.RevealTheBackline;
        public override PerkType perkType => PerkType.B1;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "reveal the backline";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void AddTargetPos()
        {  
            if (skillModel == null) return;
            skillModel.targetPos.Clear();
            CombatService.Instance.mainTargetDynas.Clear();
            for (int i = 1; i < 8; i++)
            {
                CellPosData cell = new CellPosData(CharMode.Enemy, i);
                DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cell.pos, cell.charMode);

                if (dyna != null)
                {
                    skillModel.targetPos.Add(cell);
                    CombatService.Instance.mainTargetDynas.Add(dyna);
                }
            }


        }
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.damageMod = 105f; 
        }


        public override void ApplyFX1()
        {
            DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO);

            if (targetDyna.currentPos == 5 || targetDyna.currentPos == 6 || targetDyna.currentPos == 7)
            {
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                   , AttribName.focus, -3, skillModel.timeFrame, skillModel.castTime, false);

                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                   , AttribName.luck, -3, skillModel.timeFrame, skillModel.castTime, false);
            }
        }
        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }
        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy>Target all Dmg {skillModel.damageMod}%";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str1 = $"<style=Enemy> -3 <style=Focus>Focus </style> & <style=Luck>Luck </style>, 2 rds";
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
    }


}

