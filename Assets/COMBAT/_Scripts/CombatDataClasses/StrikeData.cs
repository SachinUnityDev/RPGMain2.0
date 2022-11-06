using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Combat
{
    [System.Serializable]
    public class StrikeData
    {

        public CharController striker;
        public SkillNames skillName; 
        public DamageType dmgType;
        public AttackType attackType;
        public SkillInclination skillInclination; 
        public StrikeType strikeType;
        public List<CharController> targets;

        public StrikeData(CharController _striker, SkillNames _skillName, DamageType _dmgType, AttackType _attackType
            , SkillInclination _skillIncli, StrikeType _strikeType)
        {
            striker = _striker;
            skillName = _skillName;
            dmgType = _dmgType;
            attackType = _attackType;
            strikeType = _strikeType;
            skillInclination = _skillIncli; 
        }

    }
}


