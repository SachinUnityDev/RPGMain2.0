using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class BleedForSure : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.HoneBlades;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override PerkNames perkName => PerkNames.BleedForSure;

        public override PerkType perkType => PerkType.B1;

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "Bleed for Sure";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        float prevChanceWristSpin, prevChanceDeepCut;
        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                     .Find(t => t.skillName == skillName);

        //    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
        //    skillController = SkillService.Instance.currSkillMgr;
        //    charGO = SkillService.Instance.GetGO4Skill(charName);

        //}
        //public override void SkillHovered()
        //{
        //    SkillInit();

        //    SkillService.Instance.skillCardData.skillModel = skillModel;
        //    SkillService.Instance.SkillHovered += DisplayFX1;

        //}

        //public override void SkillSelected()
        //{

        //    SkillService.Instance.SkillApply += BaseApply;
        //    SkillService.Instance.SkillApply += ApplyFX1;

        //}
        public override void BaseApply()
        {
            targetGO = SkillService.Instance.currentTargetDyna.charGO;
            targetController = targetGO.GetComponent<CharController>();
           // CombatEventService.Instance.OnEOR1 += Tick;

        }
        public override void DisplayFX1()
        {
            str1 = "<style=Allies>Wrist Spin has 100% <style=Bleed>Bleed</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }
        public override void ApplyFX1()
        {
            // wrist spin has 100 % bleed chance..
            prevChanceWristSpin = skillController.allSkillBases.Find(t => t.skillName
                == SkillNames.WristSpin && t.skillLvl == SkillLvl.Level0).chance;

            skillController.allSkillBases.Find(t => t.skillName
                == SkillNames.WristSpin && t.skillLvl == SkillLvl.Level0).chance = 100f;

            prevChanceDeepCut = skillController.allPerkBases.Find(t => t.skillName
             == SkillNames.WristSpin && t.perkName == PerkNames.DeepCut && t.state == PerkSelectState.Clicked).chance;

            skillController.allPerkBases.Find(t => t.skillName
             == SkillNames.WristSpin && t.perkName == PerkNames.DeepCut
                                    && t.state == PerkSelectState.Clicked).chance = 100f;
        }

        public override void Tick()
        {
            //if (roundEnd >= skillModel.castTime)
            //    SkillEnd();
            //roundEnd++;
        }
        public override void SkillEnd()
        {
            //CombatEventService.Instance.OnEOR -= Tick;
            //skillController.allSkillBases.Find(t => t.skillName
            //    == SkillNames.WristSpin && t.skillLvl == SkillLvl.Level0).chance = prevChanceWristSpin;
            //skillController.allPerkBases.Find(t => t.skillName
            //                == SkillNames.WristSpin && t.perkName == PerkNames.DeepCut
            //                && t.state == PerkSelectState.Clicked).chance = prevChanceDeepCut;
            //roundEnd = 0;
        }
        public override void ApplyFX2()
        {

        }

        public override void ApplyFX3()
        {

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

        public override void ApplyVFx()
        {

        }
        
        public override void ApplyMoveFX()
        {
          
        }
    }


}

