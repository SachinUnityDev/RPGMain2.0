using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common; 


namespace Combat
{
    public class Pusher : PerkBase
    {
        public override PerkNames perkName => PerkNames.Pusher;

        public override PerkType perkType => PerkType.A1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "Push targets /n -2 Haste,/n 2 rds";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.Cleave;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void AddTargetPos()
        {

            if (skillModel == null) return;
            CombatService.Instance.mainTargetDynas.Clear();
            foreach (var cell in skillModel.targetPos)
            {
                CombatService.Instance.mainTargetDynas
                    .Add(GridService.Instance.gridView?.GetDynaFromPos(cell.pos, cell.charMode));
            }
        }
        public override void ApplyFX1()
        {
           
        }

        public override void ApplyFX2()
        {
            targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName
                , charID, AttribName.haste, -2, TimeFrame.EndOfRound, skillModel.castTime,false);
        }

        public override void ApplyFX3()
        {

        }

        public override void ApplyMoveFX()
        {
            foreach (var dyna in CombatService.Instance.mainTargetDynas)
            {
                if (dyna != null)
                {
                    GridService.Instance.gridMovement.MovebyRow(dyna, MoveDir.Backward, 1);
                   // chance++;
                }
            }
        }

        public override void ApplyVFx()
        {

        }
        public override void DisplayFX1()
        {
            str0 = $"<style=Move>Push</style> 1";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = $"-2 Haste";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }


        public override void DisplayFX3()
        {

        }

        public override void DisplayFX4()
        {

        }
        public override void InvPerkDesc()
        {
            perkDesc = "Push";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "-2 Haste";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}


