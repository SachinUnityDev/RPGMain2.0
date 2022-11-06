using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


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
        public PassiveSkillNames passiveSkillName;
        public Sprite passiveSprite; 
    }


    [CreateAssetMenu(fileName = "SkillDataSO", menuName = "Skill Service/SkillDataSO")]
    public class SkillDataSO : ScriptableObject
    {
        public CharNames charName;
        public Sprite combatPose;
        public FXSpriteData combatFX; // TO BE CHECKED 
        public Sprite defendPose;
        public FXSpriteData defendFX; // TO BE CHECKED 

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
                    perkHexdata.hexName = hexName;
                    perkHexdata.perkChain = perkChain;
                    skillData.allPerkHexes.Clear(); 
                    skillData.allPerkHexes.Add(perkHexdata); 

                }

                if(skillData.skillType == SkillTypeCombat.Patience)
                {
                    HexNames[] hexName = new HexNames[3] { HexNames.self_any, HexNames.base_hex, HexNames.base_hex };
                    List<PerkType> perkChain = new List<PerkType>() { PerkType.None };
                    PerkHexData perkHexdata = new PerkHexData();
                    perkHexdata.hexName = hexName;
                    perkHexdata.perkChain = perkChain;
                    skillData.allPerkHexes.Clear();
                    skillData.allPerkHexes.Add(perkHexdata);
                }
            }
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



