using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using Common; 

namespace Combat
{
    public class SkillFactory : MonoBehaviour
    {

        Dictionary<SkillNames, Type> allSkills = new Dictionary<SkillNames, Type>();
        [SerializeField] int skillCount; 
        void Start()
        {
            // CombatEventService.Instance.OnSOC += SkillsInit; 
            CharService.Instance.OnCharAddedToParty += (CharNames charName) =>SkillsInit();
        }

        #region SKILL_INIT
        public void SkillsInit()
        {            
            if (allSkills.Count > 0) return;
            var getskills = Assembly.GetAssembly(typeof(SkillBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(SkillBase))); 
                         
                foreach (var getSkill in getskills)
                {
                    var t = Activator.CreateInstance(getSkill) as SkillBase;
                    if(t.skillLvl == SkillLvl.Level0)
                         allSkills.Add(t.skillName, getSkill);
                }
            skillCount = allSkills.Count;
            InitPerks();
        }
    
        public SkillBase GetSkill(SkillNames _skillName)
        {          
                foreach (var skill in allSkills)
                {
                    if (skill.Key == _skillName)
                    {
                        var t = Activator.CreateInstance(skill.Value) as SkillBase;
                        return t;
                    }
                }
                Debug.Log("SKILL NOT FOUND !");
                return null;
        }


    //public void AddSkill(SkillNames _combatSkillName, GameObject GO)
    //    {
    //        foreach (var a in SkillService.Instance.allSkills)
    //        {
    //            if (a.Key == _combatSkillName)
    //            {
    //                var t = Activator.CreateInstance(a.Value) as SkillBase;
    //                var temp = GO.AddComponent(t.GetType());
    //            }
    //        }
    //    }

        #endregion


        //public void InitLvl1Perks()
        //{
        //    var Perks = Assembly.GetAssembly(typeof(PerkBase)).GetTypes()
        //                       .Where(myType => myType.IsClass
        //                       && !myType.IsAbstract && myType.IsSubclassOf(typeof(PerkBase)));



        //}


        public void InitPerks()
        {
            var Perks = Assembly.GetAssembly(typeof(PerkBase)).GetTypes()
                                  .Where(myType => myType.IsClass
                                  && !myType.IsAbstract && myType.IsSubclassOf(typeof(PerkBase)));

            foreach (var perk in Perks)
            {
                var P1 = Activator.CreateInstance(perk) as PerkBase;

                PerkModelData skillPerkData = new PerkModelData(perk, P1.skillName, P1.perkName, P1.perkType
                     , P1.skillLvl, P1.preReqList, PerkSelectState.Clickable);
            
                SkillService.Instance.allSkillPerksData.Add(skillPerkData);
                
            }
        }

       
    }
}


//public SkillBase PopulateSkillBase(SkillNames skillName)
//{
//    foreach (var skillType in SkillService.Instance.allSkills)
//    {
//        if (skillType.Key == skillName)
//        {
//            SkillBase skill = Activator.CreateInstance(skillType.Value) as SkillBase;
//            return skill; 
//        }
//    }

//    return null; 

//}

//public void AddSkillDelegate(SkillDataSO skillSO, ref List<SkillDelegateData> allSkillDelegate)
//{
//    foreach (var skills in skillSO.allSkills)
//    {

//        foreach (var skillType in allSkills)
//        {
//            if (skillType.Key == skills.skillName)
//            {
//                var mySkillBase = Activator.CreateInstance(skillType.Value) as SkillBase;

//                SkillDelegateData skillDelegateData = new SkillDelegateData();
//                skillDelegateData.skillName = skills.skillName;
//                skillDelegateData.skillOnSelectDelegate += mySkillBase.On_SkillSelected;
//                allSkillDelegate.Add(skillDelegateData); 

//            }                     
//        }
//    }



//}


