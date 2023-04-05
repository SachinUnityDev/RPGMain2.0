using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class ConcentratedFirst : PerkBase
    {
        public override PerkNames perkName => PerkNames.ConcentratedFist;
        public override PerkType perkType => PerkType.B1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None};

        public override string desc => "Target single any enemy,/n 180% --> 240% Water Soaked, 6 rds /n (If Focus 12, 280% Water)";


        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.FistOfWater;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
   

        public override void BaseApply()
        {
            base.BaseApply();
            CombatEventService.Instance.OnEOC += ApplySoakEOC;
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            AttribData statData = charController.GetAttrib(AttribName.focus); 
            if(statData.currValue == 12f)
            {
                skillModel.damageMod = 280f;

            }else
            {
                skillModel.damageMod = 240f;

            }

            // wipe soak skill in the base
            

        }

        public override void SkillEnd()
        {
            base.SkillEnd();
            CombatEventService.Instance.OnEOC -= ApplySoakEOC;
        }

        void ApplySoakEOC()
        {
            if (targetController)
            {
                CharStatesService.Instance.RemoveCharState(targetGO, CharStateName.Soaked);
            }
        }
        public override void ApplyFX1()
        {
            if (targetController)
            {
                CharStatesService.Instance
                     .ApplyCharState(targetGO, CharStateName.Soaked
                                     , charController, CauseType.CharSkill, (int)skillName);
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
            str0 = $" 240% <style=Water>Water</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);

            str1 = $"If <style=Attribute>Focus</style> 12, 280% <style=Water>Water</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"<style=States>Soaked</style>,{skillModel.castTime}";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);

            //str3 = $"If <style=Attribute>Focus</style> 12, <style=States>Soaked</style> until EOC";
            //SkillServiceView.Instance.skillCardData.descLines.Add(str3);
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

//public override SkillNames skillName => SkillNames.FistOfWater;

//public override SkillLvl skillLvl => SkillLvl.Level1;

//private PerkSelectState _state = PerkSelectState.Clickable;
//public override PerkSelectState skillState { get => _state; set => _state = value; }
//public override PerkNames perkName => PerkNames.ConcentratedFist;

//public override PerkType perkType => PerkType.B1;

//public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//public override string desc => "Concentration first";
//private float _chance = 0f;
//public override float chance { get => _chance; set => _chance = value; }
//public override void ApplyFX1()
//{
//    throw new System.NotImplementedException();
//}

//public override void ApplyFX2()
//{
//    throw new System.NotImplementedException();
//}

//public override void ApplyFX3()
//{
//    throw new System.NotImplementedException();
//}

//public override void ApplyFX4()
//{
//    throw new System.NotImplementedException();
//}

//public override void BaseApply()
//{
//    throw new System.NotImplementedException();
//}

//public override void RemoveFX1()
//{
//    throw new System.NotImplementedException();
//}

//public override void RemoveFX2()
//{
//    throw new System.NotImplementedException();
//}

//public override void RemoveFX3()
//{
//    throw new System.NotImplementedException();
//}

//public override void RemoveFX4()
//{
//    throw new System.NotImplementedException();
//}

//public override void SkillEnd()
//{
//    throw new System.NotImplementedException();
//}

//public override void SkillInit()
//{
//    throw new System.NotImplementedException();
//}

//public override void SkillSelected()
//{
//    throw new System.NotImplementedException();
//}

//public override void Tick()
//{
//    throw new System.NotImplementedException();
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

