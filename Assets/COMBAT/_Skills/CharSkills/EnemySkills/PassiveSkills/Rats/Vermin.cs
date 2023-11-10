using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Vermin : PassiveSkillBase
    {
        public override PassiveSkillName passiveSkillName => PassiveSkillName.Vermin;

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

        public override void ApplyFX()
        {
            charController.charStateController.ApplyDOTImmunityBuff(CauseType.PassiveSkillName, (int)passiveSkillName
                  , charController.charModel.charID, CharStateName.PoisonedLowDOT, TimeFrame.Infinity,1, true);

            //charController.strikeController.AddThornsBuff(DamageType.Earth, 1, 3
            //                                                    , TimeFrame.Infinity, 1);

        }
        protected override void DisplayFX1(PassiveSkillName passiveSkillName)
        {
            if (this.passiveSkillName != passiveSkillName) return;
            str0 = "Immune to Poison";
            PassiveSkillService.Instance.descLines.Add(str0);
            str1 = "Thorns: 1 - 3 Earth";
            PassiveSkillService.Instance.descLines.Add(str1);
            str2 = "+20% dmg vs Nausea";
            PassiveSkillService.Instance.descLines.Add(str2);
        }
    }

}
