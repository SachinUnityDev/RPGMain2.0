using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 


namespace Combat
{
    [System.Serializable]
    public class SkillData
    {
        public SkillNames skillName;
        public CharNames charName; 
        public int skillID;
        public Sprite skillIconSprite;
        public Sprite skillPose;
        public Sprite skillTransitionPose;
        public Sprite skillTransitionPose2;
        public SkillTypeCombat skillType;
        public StrikeNos strikeNos; 
        [Tooltip("-1 for NA , 0 for Locked and 1 for Unlocked.")]
        public int skillUnLockStatus;
        public string skillDesc;
        //public int NumberOfPerks;
        public int damageMod;
        public int skillLvl;
        //public SkillSelectState skillSelState; 
        public int cd;
        public AttackType attackType;
        public List<DamageType> dmgType = new List<DamageType>();
        public List<int> castPos = new List<int>();       
        public List<CellPosData> targetPos = new List<CellPosData>();
        public int staminareq;
        public int castTime;
        public TimeFrame timeFrame;
        public int maxUsagePerCombat;
        public SkillInclination skillIncli;
        public int baseWeight;
        public bool isActive; 
        public List<PerkHexData> allPerkHexes = new List<PerkHexData>();
        public List<SkillPerkFXData> allSkillFXs = new List<SkillPerkFXData>();
        [TextArea (5,10)]
        public List<string> allLines = new List<string>();


        public SkillData()
        {

        }

    }

    [System.Serializable]
    public class PerkHexData
    {
        public List<PerkType> perkChain = new List<PerkType>();
        public HexNames[] hexNames = new HexNames[3]; 
    }
    [System.Serializable]
    public class SkillPerkFXData
    {
        public CharMode charMode; 
        public PerkType perkType; // none means lvl 0 skill 
        public GameObject impactFX;
        public GameObject colImpactFX; 
        public GameObject mainSkillFX;
        public GameObject selfFX;
    }

}

