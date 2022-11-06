//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Common;
//using System.Linq; 

//namespace Combat
//{
//    public class QuickToReact : SkillBase
//    {
//        public override CharNames charName => CharNames.Cahyo;

//        public override SkillNames skillName => SkillNames.HoneBlades;

//        public override SkillLvl skillLvl => SkillLvl.Level1;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.QuickToReact;

//        public override PerkType perkType => PerkType.A1;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "Quick To react";

//        private float _chance = 0f;
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

//            SkillService.Instance.SkillHovered += DisplayFX1;
       
//        }

   

//        public override void SkillSelected()
//        {
//            DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);

//            if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
//                return;

//            SkillService.Instance.SkillApply += BaseApply;         

//        }
//        public override void BaseApply()
//        {
//            skillModel.cd = 2; 
//        }

//        public override void DisplayFX1()
//        {
//            str1 = $"<style=Allies> cd now {skillModel.cd} rd";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }


//        public override void ApplyFX1()
//        {
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
//    }
//}

