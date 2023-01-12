using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class HeadOrButt : PerkBase
    {
        public override PerkNames perkName => PerkNames.HeadOrButt;
        public override PerkType perkType => PerkType.A2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "-2 Focus --> Confuse, 2 rds";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.HeadToss;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
    
        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX2;
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX2;

        }


        public override void ApplyFX1()
        {
          if(targetController)
            CharStatesService.Instance
                    .ApplyCharState(targetGO, CharStateName.Confused
                                     , charController, CauseType.CharSkill, (int)skillName);
        }

        public override void ApplyFX2()
        {
        }
        public override void SkillEnd()
        {
            base.SkillEnd();
            if (targetController)
                CharStatesService.Instance.RemoveCharState(targetGO, CharStateName.Confused);
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
            str0 = $"<style=States> Confused </style>, {skillModel.castTime} rds ";
            SkillService.Instance.skillCardData.descLines.Add(str0);
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




//        public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.HeadToss;

//        public override SkillLvl skillLvl => SkillLvl.Level2;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.HeadOrButt;

//        public override PerkType perkType => PerkType.A2;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "Head or Butt";

//        private float _chance = 60f;
//        public override float chance { get => _chance; set => _chance = value; }

//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                               .Find(t => t.skillName == skillName);
//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//        }

//        public override void SkillHovered()
//        {
//            SkillInit();
//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//            Debug.Log("skill hovered" + perkName);
//            SkillService.Instance.SkillHovered += DisplayFX1;
//            SkillService.Instance.SkillHovered += DisplayFX2;
//            SkillService.Instance.SkillHovered += DisplayFX3;

//        }



//        public override void SkillSelected()
//        {
//            SkillController skillController = SkillService.Instance.currSkillMgr;

//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;

//        }

//        public override void BaseApply()
//        {
//            CombatEventService.Instance.OnEOR += Tick;
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//        }

//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

//                CharStatesService.Instance.SetCharState(targetGO, CharStateName.Confused);
//        }
//        public override void SkillEnd()
//        {
//            CombatEventService.Instance.OnEOR -= Tick;
//            CharStatesService.Instance.RemoveCharState(targetGO, CharStateName.Confused);
//            roundEnd = 0;
//        }



//        public override void Tick()
//        {
//            if (roundEnd >= skillModel.castTime)
//                SkillEnd();
//            roundEnd++;
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
//            str1 = $"<style=States> Confuse </style>, {skillModel.castTime} rds";
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


//        public override void WipeFX1()
//        {
//            SkillService.Instance.SkillHovered -= DisplayFX1;

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

