using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class BreathTaker : PerkBase
    {
        public override PerkNames perkName => PerkNames.Breathtaker; 

        public override PerkType perkType => PerkType.B2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.EdgyAxe };

        public override string desc => "(PR: B1) /n Hits first row regardless of cast pos /n 3-5 Stamina dmg"; 

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.Cleave;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void AddTargetPos()
        {

            if (skillModel == null) return; 
            skillModel.targetPos.Clear();
           // CombatService.Instance.mainTargetDynas.Clear();

            List<DynamicPosData> firstRowChar = GridService.Instance.GetFirstRowChar(CharMode.Enemy);
            Debug.Log("SkillModel is FINE " + skillModel.skillName);
            if (firstRowChar != null)
            {
                foreach (var dynaChar in firstRowChar)
                {
                    skillModel.targetPos.Add(new CellPosData(dynaChar.charMode, dynaChar.currentPos));
                    CombatService.Instance.mainTargetDynas.Add(dynaChar);
                }
            }
        }

        public override void ApplyFX1()
        {
            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                dyna.charGO.GetComponent<CharController>().damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                    , DamageType.StaminaDmg, UnityEngine.Random.Range(3, 6)); 
            }
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
            str1 = $"Hits first row";
            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"Drain <style=Attributes>Stamina </style> 3-5";
            SkillServiceView.Instance.skillCardData.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

        public override void PostApplyFX()
        {
        }
        public override void PreApplyFX()
        {
        }
    }

}

//public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.Cleave;

//        public override SkillLvl skillLvl => SkillLvl.Level2;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.Breathtaker;

//        public override PerkType perkType => PerkType.B2;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.EdgyAxe };

//        public override string desc => "BreathTaker";

//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }
//        List<DynamicPosData> targetDynas = new List<DynamicPosData>(); 

//        public override void SkillInit()
//        {
//            // new cast pos and new targets .. override the begining skills 
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
//            //SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName
//            //&& t.skillLvl == SkillLvl.Level0).WipeFX1;

//        }
//        public override void SkillSelected()
//        {
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;

//            //skillController.allSkillBases.Find(t => t.skillName == skillName
//            //                    && t.skillLvl == SkillLvl.Level0).RemoveFX1();
//            CharMode oppParty = charController.charModel.charMode.FlipCharMode();
//            targetDynas = GridService.Instance.GetFirstRowChar(oppParty);

//            skillModel.targetPos.Clear();
//            foreach (var dyna in targetDynas)
//            {
//                skillModel.targetPos.Add(new CellPosData(dyna.charMode, dyna.currentPos));
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
//            foreach (var dyna in targetDynas)
//            {
//                dyna.charGO.GetComponent<CharController>().ChangeStat(StatsName.stamina, -Random.Range(3, 6), 0, 0);
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
//        public override void Tick()
//        {


//        }
//        public override void SkillEnd()
//        {
//        } 

//        public override void DisplayFX1()
//        {
//            str1 = $" Decr Stamina 3-5";
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