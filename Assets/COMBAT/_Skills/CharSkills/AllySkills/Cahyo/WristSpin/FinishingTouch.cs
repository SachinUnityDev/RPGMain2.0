using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class FinishingTouch : PerkBase
    {
        public override PerkNames perkName => PerkNames.FinishingTouch;

        public override PerkType perkType => PerkType.B2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "+40% dmg to bleeding enemies";

        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.WristSpin;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
    

        public override void ApplyFX1()
        {
            bool isTargetBleeding = targetController.charStateController.HasCharDOTState(CharStateName.BleedLowDOT);

            if (isTargetBleeding)
            {
                targetController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.luck, +4, false); 
            }
        }

        public override void SkillEnd()
        {
            base.SkillEnd();
           targetController.charStateController.RemoveCharState(CharStateName.BleedLowDOT);
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
            str1 = $"+4 <style=Attributes>Luck</style> to bleeding targets";
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




//}
//        public override CharNames charName => CharNames.Cahyo;

//        public override SkillNames skillName => SkillNames.WristSpin;

//        public override SkillLvl skillLvl => SkillLvl.Level2;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.FinishingTouch;

//        public override PerkType perkType => PerkType.B2;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "Finishing Touch";
//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }
//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                                  .Find(t => t.skillName == skillName);

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;

//            charGO = SkillService.Instance.GetGO4Skill(charName);


//        }
//        public override void SkillHovered()
//        {
//            SkillInit();          

//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;

//            SkillService.Instance.SkillHovered += DisplayFX1;
//        }
//        public override void SkillSelected()
//        {
//            DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);

//            if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
//                return;


//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;

//        }
//        public override void BaseApply()
//        {
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//           // CombatEventService.Instance.OnEOR += Tick;
//        }
//        public override void Tick()
//        {
//        }

//        public override void ApplyFX1()
//        {
//            if (CharStatesService.Instance.HasCharState(targetGO, CharStateName.BleedLowDOT)
//                || CharStatesService.Instance.HasCharState(targetGO, CharStateName.BleedMedDOT)
//                || CharStatesService.Instance.HasCharState(targetGO, CharStateName.BleedHighDOT))
//            {

//                skillModel.damageMod = 140f;
//            }



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



//        public override void RemoveFX1()
//        {
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



//        public override void DisplayFX1()
//        {
//            str1 = $"<style=Enemy> 40% Extra Dmg to <style=Bleed> Bleeding </style>";
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

//        public override void WipeFX1()
//        {
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

//        public override void PreApplyFX()
//        {
//        }

//        public override void PostApplyFX()
//        {
//        }