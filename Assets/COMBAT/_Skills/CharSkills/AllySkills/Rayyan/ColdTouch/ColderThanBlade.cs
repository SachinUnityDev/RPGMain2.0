using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class ColderThanBlade : PerkBase
    {
        public override PerkNames perkName => PerkNames.ColderThanBlade;
        public override PerkType perkType => PerkType.A2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.InspiringTouch };

        public override string desc => "+20% Physical skills";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.ColdTouch;
        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
  

        public override void BaseApply()
        {
            base.BaseApply();
            CombatEventService.Instance.OnStrikeFired += ExtraPhysicalDmg;
        }

        public void ExtraPhysicalDmg(StrikeData strikeData)
        {
            if (targetController == strikeData.striker)
            {
                if (strikeData.skillInclination == SkillInclination.Physical)
                {
                    strikeData.targets.ForEach(t => t.damageController.ApplyDamage(strikeData.striker, CauseType.CharSkill
                                                                       , (int)skillName, DamageType.Physical, +20f, false));
                }
            }
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
            str1 = $"+20% Physical Skills, {skillModel.castTime} rds";

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

//public override SkillLvl skillLvl => SkillLvl.Level2;

//private PerkSelectState _state = PerkSelectState.Clickable;
//public override PerkSelectState skillState { get => _state; set => _state = value; }
//public override PerkNames perkName => PerkNames.ColderThanBlade;

//public override PerkType perkType => PerkType.A2;

//public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.InspiringTouch };

//public override string desc => "Colder than Blade";

//private float _chance = 0f;
//public override float chance { get => _chance; set => _chance = value; }
//List<GameObject> targetGOs = new List<GameObject>();
//List<CharController> targetControllers = new List<CharController>();

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
//}



//public override void SkillSelected()
//{
//    DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);

//    if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
//        return;

//    skillController.allPerkBases.Find(t => t.skillName == skillName).RemoveFX2();
//    SkillService.Instance.SkillApply += BaseApply;
//    SkillService.Instance.SkillApply += ApplyFX1;
//}
//public override void BaseApply()
//{
//    List<DynamicPosData> allDyna = GridService.Instance.GetAllOccupiedbyCharMode(charController.charModel.charMode);
//    allDyna.ForEach(t => targetGOs.Add(t.charGO));
//    targetGOs.ForEach(t => targetControllers.Add(t.GetComponent<CharController>()));
//    CombatEventService.Instance.OnEOR += Tick;

//    CombatEventService.Instance.OnStrikeFired += ExtraAllyPhysicalDmg;


//}

//public override void DisplayFX1()
//{
//    str1 = "+20% dmg to allies' <style=Physical>Phyical</style> skills, 2 rd";
//    SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//}

//void ExtraAllyPhysicalDmg(StrikeData strikeData)
//{
//    foreach (CharController allyCtrl in targetControllers)
//    {
//        if (allyCtrl == strikeData.striker)
//        {
//            SkillModel usedSM = SkillService.Instance.allSkillModels
//                                            .Find(t => t.skillName == strikeData.skillName);

//            // To be discussed SkillTYPE 
//            //if (strikeData.skillType == SkillType.Physical)
//            //    strikeData.targets.ForEach(t => t.dmgController.ApplyDamage(strikeData.striker
//            //        ,usedSM.dmgType , UnityEngine.Random.Range(3, 6), false));
//        }
//    }

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
