using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    [System.Serializable]
    public class FXSpriteData
    {
        public LocWRTChar loc; 
        public Sprite skillSprite;
    }

    [System.Serializable]

    public class PassiveSkillData
    {
        public PassiveSkillName passiveSkillName;
        public Sprite passiveSprite; 
    }


    [CreateAssetMenu(fileName = "SkillDataSO", menuName = "Skill Service/SkillDataSO")]
    public class SkillDataSO : ScriptableObject  // each char has one Skill SO and  allSkillInside this SO
    {
        public CharNames charName;
        public Sprite combatPose;
        public FXSpriteData combatFX; // TO BE CHECKED 
        public Sprite defendPose;
        public FXSpriteData defendFX; // TO BE CHECKED 

        [Header("Background Sprite")]
        public Sprite leftInvSkillPanelBG; 
        public Sprite rightInvSkillPanelBG;

        public List<PassiveSkillData> passiveSkills = new List<PassiveSkillData>();

        public List<SkillData> allSkills = new List<SkillData>();
        public void Awake()
        {
            if (allSkills.Count < 1) return; 
            foreach(SkillData skillData in allSkills)
            {
                skillData.charName = charName;
                if (skillData.skillType == SkillTypeCombat.Move || skillData.skillType == SkillTypeCombat.Patience)
                {
                    skillData.castPos.Clear();
                    skillData.castPos.AddRange(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
                }            
                if (skillData.castPos.Count < 1)
                {
                    skillData.castPos.AddRange(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
                }

                if (skillData.skillType == SkillTypeCombat.Move)
                {
                    
                    HexNames[] hexName = new HexNames[3] {HexNames.ally_anyoneNcast_anyone, HexNames.base_hex, HexNames.base_hex };
                    List<PerkType> perkChain = new List<PerkType>() { PerkType.None };
                    PerkHexData perkHexdata= new PerkHexData();
                    perkHexdata.hexNames = hexName;
                    perkHexdata.perkChain = perkChain;
                    skillData.allPerkHexes.Clear(); 
                    skillData.allPerkHexes.Add(perkHexdata); 

                }

                if(skillData.skillType == SkillTypeCombat.Patience)
                {
                    HexNames[] hexName = new HexNames[3] { HexNames.self_any, HexNames.base_hex, HexNames.base_hex };
                    List<PerkType> perkChain = new List<PerkType>() { PerkType.None };
                    PerkHexData perkHexdata = new PerkHexData();
                    perkHexdata.hexNames = hexName;
                    perkHexdata.perkChain = perkChain;
                    skillData.allPerkHexes.Clear();
                    skillData.allPerkHexes.Add(perkHexdata);
                }
            }
        }

        public SkillData GetSkillData(SkillNames skillName)
        {
            int index = allSkills.FindIndex(t=>t.skillName == skillName);
            if(index != -1)
            {
                return allSkills[index];
            }
            else
            {
                Debug.Log("Skilldata Not found" + skillName);
                return null; 
            }
        }

        public PerkHexData GetPerkHexData(List<PerkType> perkChain, SkillNames skillName)
        {
            perkChain = perkChain.OrderBy(t=>t).ToList(); 
            SkillData skillData = GetSkillData(skillName);
            if(perkChain.Count == 0)
            {
                PerkHexData perkHexData = 
                 skillData.allPerkHexes.Find(t => t.perkChain[0] == PerkType.None);
                if (perkHexData != null)
                    return perkHexData;
                else
                    return null; 
            }
            foreach (PerkHexData perk in skillData.allPerkHexes)
            {
            
                if (perk.perkChain.SequenceEqual(perkChain))
                {
                    return perk;  
                }
            }
            Debug.Log("Perk Hex data not found" + skillName + perkChain[0]);
            return null; 
        }

    }


   
    public enum LocWRTChar
    {
        None, 
        Top,
        Bottom, 
        Front, 
        Back, 
        Travel, 

    }

}



