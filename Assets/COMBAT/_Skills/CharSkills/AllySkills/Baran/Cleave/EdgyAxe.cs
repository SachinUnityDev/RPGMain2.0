using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class EdgyAxe : PerkBase
    {
        public override PerkNames perkName => PerkNames.EdgyAxe;

        public override PerkType perkType => PerkType.B1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "30% Low Bleed --> 30% High Bleed";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.Cleave;

        public override SkillLvl skillLvl => SkillLvl.Level1;


        private float _chance = 30f;
        public override float chance { get => _chance; set => _chance = value; }
     
        public override void SkillHovered()
        {
            base.SkillHovered();            
            SkillService.Instance.SkillWipe += skillController.allSkillBases
                                                .Find(t => t.skillName == skillName).WipeFX2;

        }
        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillWipe += skillController.allSkillBases
                                              .Find(t => t.skillName == skillName).RemoveFX2;
        }
        public override void ApplyFX1()
        {
            if (chance.GetChance())             
                CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
                .charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                , charController.charModel.charID, CharStateName.BleedHighDOT, TimeFrame.Infinity, -1));

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
            str1 = $"{chance}% <style=Bleed>High Bleed </style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
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

    }
}

//public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.Cleave;

//        public override SkillLvl skillLvl => SkillLvl.Level1;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.EdgyAxe;

//        public override PerkType perkType => PerkType.B1;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "Edgy Axe";

//        private float _chance = 50f;
//        public override float chance { get => _chance; set => _chance = value; }
//        List<DynamicPosData> targetDynas = new List<DynamicPosData>();


//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                             .Find(t => t.skillName == skillName);

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//        }

//        public override void SkillHovered()
//        {
//            SkillInit();
//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//            SkillService.Instance.SkillHovered += DisplayFX1;
//            SkillService.Instance.SkillHovered += DisplayFX2;
//            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName
//         && t.skillLvl == SkillLvl.Level0).WipeFX2;
//        }

//        public override void SkillSelected()
//        {

//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//            skillController.allSkillBases.Find(t => t.skillName == skillName && t.skillLvl == SkillLvl.Level0).RemoveFX2();
//        }
//        public override void BaseApply()
//        {
//            CombatEventService.Instance.OnEOR += Tick;

//            foreach (var cell in skillModel.targetPos)
//            {
//                targetDynas.Add(GridService.Instance.gridView?.GetDynaFromPos(cell.pos, cell.charMode));
//            }

//        }
//        public override void ApplyFX1()
//        {

//            if (chance.GetChance())
//                targetDynas.ForEach(t => CharStatesService.Instance.SetCharState(t.charGO, CharStateName.BleedMedDOT));


//        }

//        public override void ApplyFX2()
//        {

//        }

//        public override void ApplyFX3()
//        {
//        }

//        public override void ApplyFX4()
//        {
//        }



//        public override void DisplayFX1()
//        {
//            str1 = $"{chance}% <style=Bleed>Medium Bleed</style>";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }

//        public override void DisplayFX2()
//        {

//        }

//        public override void DisplayFX3()
//        {
//        }

//        public override void DisplayFX4()
//        {
//        }

//        public override void PostApplyFX()
//        {
//        }

//        public override void PreApplyFX()
//        {
//        }

//        public override void RemoveFX1()
//        {
//            SkillService.Instance.SkillApply -= ApplyFX1;
//        }

//        public override void RemoveFX2()
//        {
//        }

//        public override void RemoveFX3()
//        {
//        }

//        public override void RemoveFX4()
//        {
//        }

//        public override void SkillEnd()
//        {
//        }



//        public override void Tick()
//        {
//        }

//        public override void WipeFX1()
//        {
//            SkillService.Instance.SkillHovered += DisplayFX1;
//        }

//        public override void WipeFX2()
//        {
//        }

//        public override void WipeFX3()
//        {
//        }

//        public override void WipeFX4()
//        {
//        }