//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Combat
//{
//    public class FirstTarget : SkillBase
//    {
//        public override CharNames charName => CharNames.Abbas;

//        public override SkillNames skillName => SkillNames.RunguThrow;

//        public override SkillLvl skillLvl => SkillLvl.Level3;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.FirstTarget;

//        public override PerkType perkType => PerkType.A3;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.ConfuseThem, PerkNames.NoMercyForTheCripple };

//        public override string desc => "First Target";

//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }
//        public override void SkillHovered()
//        {
//        }

//        public override void SkillInit()
//        {
//        }

//        public override void SkillSelected()
//        {
//        }
//        public override void BaseApply()
//        {
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

