using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    [System.Serializable]
    public class SkillModel
    {
        public SkillNames skillName;
        public CharNames charName; 
        public int skillID;
        public int charID;
        public string skillDesc;
        public Sprite skillSprite;
        public SkillTypeCombat skillType;
        [Tooltip("-1 for NA , 0 for Locked and 1 for Unlocked.")]
        public int skillUnLockStatus;
        public float damageMod;
        public float skillLvl;
        [SerializeField]
        SkillSelectState skillSelState; 
        public int cd;
        public AttackType attackType;
        public List<DamageType> dmgType;
        public List<int> castPos = new List<int>();
        public List<CellPosData> targetPos = new List<CellPosData>();
        public int staminaReq;
        public int castTime;
        public TimeFrame timeFrame;
        public int maxUsagePerCombat;
        public SkillInclination skillInclination;
        public int baseWeight;
        public float AIChance = 0; 
        public bool isActive;
        // // EXTRA Parameters
        public int lastUsedInRound =-5;
        public List<PerkHexData> allPerkHexes = new List<PerkHexData>();
        public List<PerkType> perkChain = new List<PerkType>();
        List<string> descLines = new List<string>();
        List<string> perkDescLines = new List<string>();

        [Header("Use Limit")]
        public int noOfTimesUsed;
        [Header(" cd remaining")]
        public int cdRemaining; 

        public SkillSelectState GetSkillState()
        {
            return skillSelState; 
        }
        public void SetSkillState(SkillSelectState _skillState)
        {   
            if (skillInclination == SkillInclination.Passive)
            {               
                skillSelState = SkillSelectState.Unclickable_passiveSkills;              
            }   
            else
            {
                skillSelState = _skillState; 
            }          
        }
        public void AddDescLines(string str)
        {
            if(!descLines.Any(t=>t == str))
                descLines.Add(str);
        }
        public List<string> GetDescLines()
        {
            List<string> descLines1 = new List<string>();
            descLines1 = descLines.DeepClone();
            return descLines1;
        }
        public void AddPerkDescLines(string str)
        {
            if (!perkDescLines.Any(t => t == str))
                descLines.Add(str);
        }
        public List<string> GetPerkDescLines()
        {
            List<string> perkDescLines1 = new List<string>();
            perkDescLines1 = perkDescLines.DeepClone();
            return perkDescLines1;
        }
        public SkillModel(SkillData _skillDataSO)
        {
          
            skillName = _skillDataSO.skillName;
            charName = _skillDataSO.charName;
            skillID = (int)skillName;
            skillDesc = _skillDataSO.skillDesc;
            skillSprite = _skillDataSO.skillIconSprite;
            skillType = _skillDataSO.skillType;
            skillUnLockStatus =_skillDataSO.skillUnLockStatus;
            damageMod = _skillDataSO.damageMod;
            skillLvl = _skillDataSO.skillLvl;
            skillSelState = _skillDataSO.skillSelState;
            cd = _skillDataSO.cd;
            attackType = _skillDataSO.attackType;
            dmgType = _skillDataSO.dmgType.DeepClone();
            castPos = _skillDataSO.castPos.DeepClone();
            //targetPos = _skillDataSO.targetPos;
            staminaReq = _skillDataSO.staminareq;
            castTime = _skillDataSO.castTime;
            timeFrame = _skillDataSO.timeFrame;
            maxUsagePerCombat = _skillDataSO.maxUsagePerCombat;
            skillInclination = _skillDataSO.skillIncli;
            baseWeight = _skillDataSO.baseWeight;
            allPerkHexes = _skillDataSO.allPerkHexes;
            isActive = _skillDataSO.isActive;
            //  Uses and cd remaining
            noOfTimesUsed = 0;
        }

    }






}

