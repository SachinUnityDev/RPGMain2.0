using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    [System.Serializable]
    public class PerkData 
    {
        
        public SkillNames skillName;
        public PerkNames perkName;
        public PerkSelectState state;
        public PerkType perkType;
        public SkillLvl perkLvl;
        public List<PerkNames> preReqList = new List<PerkNames>();
        public int[] pipeRel = new int[2];
        public PerkData(SkillNames skillName, PerkNames perkName, PerkSelectState state
            , PerkType perkType, SkillLvl perkLvl, List<PerkNames> preReqList)
        {
            this.skillName = skillName;
            this.perkName = perkName;
            this.state = state;
            this.perkType = perkType;
            this.perkLvl = perkLvl;
            this.preReqList = preReqList;
            pipeRel[0] = 0; 
            pipeRel[1] = 0;
        }
        public PerkData()
        {

        }
    }
}