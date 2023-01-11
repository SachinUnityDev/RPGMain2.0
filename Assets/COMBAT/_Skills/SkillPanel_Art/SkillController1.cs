using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class SkillController1 : MonoBehaviour
    {

        [SerializeField] CharMode charMode;
        CharController charController;
        CharNames charName; 
      
        public int currSkillID;
        [Header("All Skill and UnLocked Skill list")]
        public List<SkillNames> allSkillInChar = new List<SkillNames>();
        public List<SkillNames> unLockedSkills = new List<SkillNames>();

        [Header("Skill Model")]
        public List<SkillModel> allSkillModels = new List<SkillModel>();

        [Header("Skill and Perk Bases")]
        public List<SkillBase> allSkillBases = new List<SkillBase>();
        public List<PerkBase> allPerkBases = new List<PerkBase>();

        SkillDataSO skillDataSO;


        private void Start()
        {
            charController = gameObject.GetComponent<CharController>();
            charName = charController.charModel.charName;

            CharService.Instance.OnCharAddedToParty += PopulateSkillList;
        }
        public void PopulateSkillList(CharNames charName)
        {
            skillDataSO = SkillService.Instance.GetSkillSO(charName);
            foreach (SkillData skill in skillDataSO.allSkills)
            {
                allSkillInChar.Add(skill.skillName);

                if (skill.skillUnLockStatus == 1) // 1 = unlock, 0 locked, -1 NA
                {
                    unLockedSkills.Add(skill.skillName);
                }
            }
            foreach (var skillSO in skillDataSO.allSkills)
            {
                SkillBase skillbase = SkillService.Instance.skillFactory.GetSkill(skillSO.skillName);

                skillbase.charName = skillDataSO.charName;
                mySkillName = skillbase.skillName;// redundant stmt
                                                  // allSkillBases.ForEach(t => Debug.Log("SKILLBASES INIT" + t.skillName));
                allSkillBases.Add(skillbase);
                skillID++;  // could use random generation here 
                skillbase.SkillInit(this); // pass in all the params when all skills are coded

            }
        }

    }
}