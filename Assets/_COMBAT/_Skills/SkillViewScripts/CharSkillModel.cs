using Combat;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    [Serializable]
    public class CharSkillModel
    {
        [Header("Skill Perk Data")]
        public List<PerkData> allSkillPerkData = new List<PerkData>();

        [Header("Skill Model")]
        public List<SkillModel> allSkillModels = new List<SkillModel>();

    }
}