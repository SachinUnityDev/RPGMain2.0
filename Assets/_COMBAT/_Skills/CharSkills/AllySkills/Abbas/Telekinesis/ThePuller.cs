using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{

    public class ThePuller : PerkBase
    {
        public override PerkNames perkName => PerkNames.ThePuller; 

        public override PerkType perkType => PerkType.B1;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is Puller";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.Telekinesis;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        public override float chance { get; set; }

        public override void AddTargetPos()
        {
            base.AddTargetPos();
            if (skillModel == null) return;

            skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear();

            List<DynamicPosData> sameLaneOccupiedPos = GridService.Instance.GetInSameLaneOppParty
                         (new CellPosData(charController.charModel.charMode, GridService.Instance.GetDyna4GO(charGO).currentPos));
            int last = sameLaneOccupiedPos.Count;
            if (last > 0)
            {
                CellPosData Pos = new CellPosData(sameLaneOccupiedPos[last-1].charMode, sameLaneOccupiedPos[last -1].currentPos);
                skillModel.targetPos.Add(Pos);
                CombatService.Instance.mainTargetDynas
                    .Add(GridService.Instance.GetDynaAtCellPos(sameLaneOccupiedPos[last -1].charMode, sameLaneOccupiedPos[last - 1].currentPos));
            }

        }

        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX2;
        }
        public override void PerkSelected()
        {
            base.PerkSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX2;
        }
        public override void ApplyFX1()
        {
            DynamicPosData dyna = GridService.Instance.GetDyna4GO(targetGO);
            if (targetController)
                GridService.Instance.gridMovement.MovebyRow(dyna, MoveDir.Forward, 1);

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
            str1 = "Target -> Last on same lane";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "<style=Move>Pull</style>";
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
            perkDesc = "Target -> Last on same lane";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "<style=Move>Push</style> -> <style=Move>Pull</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}