using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class ICheatDeath : PerkBase
    {
        public override PerkNames perkName => PerkNames.ICheatDeath;

        public override PerkType perkType => PerkType.A1;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is I cheat Death";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.FeignDeath;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        public override float chance { get; set; }

        public override void BaseApply()
        {
            base.BaseApply();     
            CombatEventService.Instance.OnEOC += OnEOC; 
        }
        void OnEOC()
        {
            CombatEventService.Instance.OnEOC -= OnEOC;
        }
     
        public override void ApplyFX1()
        {
            charController.damageController.MAX_ALLOWED_CHEATDEATH++; 

        }

        public override void ApplyFX2()
        {

            AttribData hasteState = charController.GetAttrib(AttribName.haste);
            if (hasteState.currValue == 12)
            {
                charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                                , charController.charModel.charID, CharStateName.Sneaky);
            }
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