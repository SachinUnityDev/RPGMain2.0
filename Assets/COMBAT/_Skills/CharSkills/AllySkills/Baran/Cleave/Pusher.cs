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
        //public override void SkillEnd()
        //{
        //    base.SkillEnd();
        //   // targetController.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.haste, 2);
        //}


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
            str0 = $"<style=Move>Push,</style> 1 row";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);    
        }

        public override void DisplayFX2()
        {
            str1 = $"-2 <style=Attributes>Haste</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX3()
        {

        }

        public override void DisplayFX4()
        {

        }

    }
}

//public override CharNames charName => CharNames.Baran;

//public override SkillNames skillName => SkillNames.Cleave;

//public override SkillLvl skillLvl => SkillLvl.Level1;

//private PerkSelectState _state = PerkSelectState.Clickable;
//public override PerkSelectState skillState { get => _state; set => _state = value; }
//public override PerkNames perkName => PerkNames.Pusher;

//public override PerkType perkType => PerkType.A1;

//public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//public override string desc => "Pusher";

//private float _chance = 0f;
//public override float chance { get => _chance; set => _chance = value; }

//List<DynamicPosData> targetDynas = new List<DynamicPosData>(); 
//public override void SkillInit()
//{
//    skillModel = SkillService.Instance.allSkillModels
//                                      .Find(t => t.skillName == skillName);

//    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//    skillController = SkillService.Instance.currSkillMgr;
//    charGO = SkillService.Instance.GetGO4Skill(charName);
//}

//public override void SkillHovered()
//{
//    SkillInit();
//    SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//    SkillService.Instance.SkillHovered += DisplayFX1;
//    SkillService.Instance.SkillHovered += DisplayFX2;
//}    

//public override void SkillSelected()
//{

//    SkillService.Instance.SkillApply += BaseApply;
//    SkillService.Instance.SkillApply += ApplyFX1;
//    SkillService.Instance.SkillApply += ApplyFX2;
//}


//public override void BaseApply()
//{
//    CombatEventService.Instance.OnEOR += Tick;
//    targetDynas.Clear();
//    foreach (var cell in skillModel.targetPos)
//    {
//        targetDynas.Add(GridService.Instance.gridView?.GetDynaFromPos(cell.pos, cell.charMode));
//    }

//}
//public override void ApplyFX1()
//{
//    if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;


//    foreach(var dyna in targetDynas)
//    {
//        if (dyna != null)
//        {
//            GridService.Instance.gridMovement.MovebyRow(dyna
//                , MoveDir.Backward, 1);
//            chance++; 
//        }
//    }
//}

//public override void ApplyFX2()
//{
//    if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

//    targetDynas.ForEach(t => t.charGO.GetComponent<CharController>().ChangeStat(StatsName.initiative, +2, 0, 0)); 

//}

//public override void ApplyFX3()
//{
//}

//public override void ApplyFX4()
//{
//}
//public override void Tick()
//{

//    if (roundEnd >= skillModel.castTime)
//        SkillEnd();
//    roundEnd++; 

//}




//public override void SkillEnd()
//{
//    CombatEventService.Instance.OnEOR -= Tick;

//    targetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
//                                .ChangeStat(StatsName.initiative, -2, 0, 0));
//    roundEnd = 0;

//}


//public override void DisplayFX1()
//{


//    str1 = $"<style=Move>Push </style>1 row";
//    SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//}

//public override void DisplayFX2()
//{

//    str2 = $"+2 Init {skillModel.castTime} rd";
//    SkillServiceView.Instance.skillCardData.descLines.Add(str2);
//}

//public override void DisplayFX3()
//{
//}

//public override void DisplayFX4()
//{
//}

//public override void PostApplyFX()
//{
//}

//public override void PreApplyFX()
//{
//}

//public override void RemoveFX1()
//{
//    SkillService.Instance.SkillApply -= ApplyFX1;

//}

//public override void RemoveFX2()
//{
//    SkillService.Instance.SkillApply -= ApplyFX2;

//}

//public override void RemoveFX3()
//{
//}

//public override void RemoveFX4()
//{
//}


//public override void WipeFX1()
//{
//}

//public override void WipeFX2()
//{
//}

//public override void WipeFX3()
//{
//}

//public override void WipeFX4()
//{
//}


