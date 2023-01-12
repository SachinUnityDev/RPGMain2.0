using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class CleverCleave : PerkBase
    {
        public override PerkNames perkName => PerkNames.CleverCleave;

        public override PerkType perkType => PerkType.A3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "If Edgy Axe is picked; 30% High Bleed --> 60% High Bleed /n If Pusher is picked; +1 Fortitude Org.until EOQ, per pushed enemy (Stacks up to 8)/n 6 --> 8 Stamina cost";

        public override CharNames charName => CharNames.Baran; 

        public override SkillNames skillName => SkillNames.Cleave;

        public override SkillLvl skillLvl => SkillLvl.Level3;


        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        int stack = 0; 
     

        public override void BaseApply()
        {
            base.BaseApply();

            foreach (SkillPerkData skillModelData in skillController.allSkillPerkData)
            {
                if (skillModelData.state == PerkSelectState.Clicked && skillModelData.perkName == PerkNames.EdgyAxe)
                {
                    skillController.allPerkBases.Find(t => t.skillName == skillName
                                                           && t.skillLvl == SkillLvl.Level1
                                                           && t.state == PerkSelectState.Clicked).chance = 60f;

                }
                if (skillModelData.state == PerkSelectState.Clicked && skillModelData.perkName == PerkNames.Pusher)
                {
                    //chance = skillController.allPerkBases.Find(t => t.skillName == skillName
                    //                                        && t.skillLvl == SkillLvl.Level1
                    //                                        && t.state == PerkSelectState.Clicked).chance;

                    // per enemy pushed you get +1. UNTILL EOQ 
                    if(stack <= 8)
                    {
                        charController.charModel.fortitudeOrg += 1;
                        stack++; 
                    }

                }
            }

            skillModel.staminaReq = 8; 
        }

        public override void ApplyFX1()
        {
       



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
            str0 = $"60% <style=Bleed>High Bleed</style>";
            SkillService.Instance.skillCardData.descLines.Add(str0);


            str1 = $"+1 <style=Fort>Fortitude Org.</style> until EOQ, per pushed enemy(Stacks up to 8)";
            SkillService.Instance.skillCardData.descLines.Add(str1);

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
//        public override SkillLvl skillLvl => SkillLvl.Level3;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.CleverCleave;

//        public override PerkType perkType => PerkType.A3;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "Clever Cleave";

//        private float _chance = 100f;
//        public override float chance { get => _chance; set => _chance = value; }
//        float baseFortOrg; 

//        List<DynamicPosData> targetDynas = new List<DynamicPosData>();
//        bool edgySelect = false;
//        bool pusherSelect = false; 

//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                            .Find(t => t.skillName == skillName);

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
//        }      

//        public override void SkillSelected()
//        {
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//             edgySelect = (skillController.allPerkBases.Any(t => t.skillName == skillName && t.skillLvl == SkillLvl.Level1
//               & t.perkName == PerkNames.EdgyAxe));
//             pusherSelect = (skillController.allPerkBases.Any(t => t.skillName == skillName && t.skillLvl == SkillLvl.Level1
//           & t.perkName == PerkNames.Pusher));
//            if (edgySelect)
//            {
//                skillController.allPerkBases.Find(t => t.skillName == skillName && t.skillLvl == SkillLvl.Level1
//                & t.perkName == PerkNames.EdgyAxe).RemoveFX1();

//            }           
//            foreach (var cell in skillModel.targetPos)
//            {
//                targetDynas.Add(GridService.Instance.gridView?.GetDynaFromPos(cell.pos, cell.charMode));
//            }

//        }
//        public override void PreApplyFX()
//        {

//        }
//        public override void BaseApply()
//        {
//            CombatEventService.Instance.OnEOR += Tick;

//        }

//        public override void ApplyFX1()
//        {
//            if (edgySelect)
//            {
//                if (_chance.GetChance())
//                    targetDynas.ForEach(t => CharStatesService.Instance
//                                .SetCharState(t.charGO, CharStateName.BleedHighDOT));
//            }
//        }

//        public override void ApplyFX2()
//        {
//            if (pusherSelect)
//            {
//                // tap if to fortitude value @ start of Combat

//            }


//        }

//        public override void ApplyFX3()
//        {

//        }

//        public override void ApplyFX4()
//        {

//        }
//        public override void Tick()
//        {

//        }
//        public override void SkillEnd()
//        {

//        }

//        public override void DisplayFX1()
//        {

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
