using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class FringesOfIce : PerkBase
    {
        public override PerkNames perkName => PerkNames.FringesOfIce;
        public override PerkType perkType => PerkType.B1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "Melee attackers against targeted ally takes 4-6 water dmg(cast time 2 rds)(Retaliation)";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.ColdTouch;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }



   

        public override void ApplyFX1()
        {
           targetController.strikeController.AddThornsBuff(DamageType.Water, 4, 6
                                                    , skillModel.timeFrame, skillModel.castTime);

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
            str1 = $"Melee attackers recieve 40%<style=States> Water </style>back, {skillModel.castTime} rds";
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
//public override CharNames charName => CharNames.Rayyan;

//public override SkillNames skillName => SkillNames.ColdTouch;

//public override SkillLvl skillLvl => SkillLvl.Level1;

//private PerkSelectState _state = PerkSelectState.Clickable;
//public override PerkSelectState skillState { get => _state; set => _state = value; }
//public override PerkNames perkName => PerkNames.FringesOfIce;

//public override PerkType perkType => PerkType.B1;

//public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//public override string desc => "FringesOfIce";
//private float _chance = 0f;
//public override float chance { get => _chance; set => _chance = value; }
//public override void SkillInit()
//{
//}

//public override void SkillSelected()
//{
//}

//public override void ApplyFX1()
//{

//}

//public override void ApplyFX2()
//{

//}

//public override void ApplyFX3()
//{

//}

//public override void ApplyFX4()
//{

//}

//public override void BaseApply()
//{

//}

//public override void RemoveFX1()
//{

//}

//public override void RemoveFX2()
//{

//}

//public override void RemoveFX3()
//{
//}

//public override void RemoveFX4()
//{

//}

//public override void SkillEnd()
//{

//}

//public override void Tick()
//{

//}

//public override void SkillHovered()
//{
//    throw new System.NotImplementedException();
//}

//public override void DisplayFX1()
//{
//    throw new System.NotImplementedException();
//}

//public override void DisplayFX2()
//{
//    throw new System.NotImplementedException();
//}

//public override void DisplayFX3()
//{
//    throw new System.NotImplementedException();
//}

//public override void DisplayFX4()
//{
//    throw new System.NotImplementedException();
//}

//public override void WipeFX1()
//{
//    throw new System.NotImplementedException();
//}

//public override void WipeFX2()
//{
//    throw new System.NotImplementedException();
//}

//public override void WipeFX3()
//{
//    throw new System.NotImplementedException();
//}

//public override void WipeFX4()
//{
//    throw new System.NotImplementedException();
//}

//public override void PreApplyFX()
//{
//    throw new System.NotImplementedException();
//}

//public override void PostApplyFX()
//{
//    throw new System.NotImplementedException();
//}
