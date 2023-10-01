using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

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
        public List<string> descLines = new List<string>();
        public SkillSelectState GetSkillState()
        {
            return skillSelState; 
        }
        public void SetSkillState(SkillSelectState _skillState)
        {
            // if passive  it cannot have anyother state than passive unclickable
            if (skillInclination == SkillInclination.Passive)
            {               
                skillSelState = SkillSelectState.Unclickable_passiveSkills;
              
            }
            //else if(attackType == AttackType.Remote)
            //{
            //     // to be written 
            //}
            else
            {
                skillSelState = _skillState; 
            }          
        }

        public SkillModel(SkillData _skillDataSO)
        {
          //  SkillData _skillData = _skillDataSO;
            skillName = _skillDataSO.skillName;
            charName = _skillDataSO.charName;
            skillID = (int)skillName;
            skillDesc = _skillDataSO.skillDesc;
            skillSprite = _skillDataSO.skillIconSprite;
            skillType = _skillDataSO.skillType;
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
        }

    }






}

