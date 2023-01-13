using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{

    public class SkillPerkData : MonoBehaviour
    {
        
        public SkillNames skillName;
        public PerkNames perkName;
        public PerkSelectState state;
        public PerkType perkType;
        public SkillLvl perkLvl;
        public List<PerkNames> preReqList = new List<PerkNames>();        

        public SkillPerkData(SkillNames skillName, PerkNames perkName
                                    , PerkSelectState state, PerkType perkType, SkillLvl perkLvl
                                    , List<PerkNames> preReqList)
        {
            this.skillName = skillName;
            this.perkName = perkName;
            this.state = state;
        }
    }
}