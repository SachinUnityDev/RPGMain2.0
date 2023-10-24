using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Vermin : PassiveSkillBase
    {
        public override PassiveSkillNames passiveSkillName => PassiveSkillNames.Vermin;

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

        private SkillNames _skillName;
        public override SkillNames skillName { get => _skillName; set => _skillName = value; }

    

        public override void ApplyFX()
        {
            charController.charStateController.ApplyDOTImmunityBuff(CauseType.CharSkill, (int)skillName
                  , charController.charModel.charID, CharStateName.PoisonedLowDOT, TimeFrame.Infinity,1, true);

            charController.strikeController.AddThornsBuff(DamageType.Earth, 1, 3
                                                                , TimeFrame.Infinity,1);

        }
     
    }

}
