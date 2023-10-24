using Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;


namespace Combat
{


    public class PassiveSkillFactory : MonoBehaviour
    {


        Dictionary<PassiveSkillNames, Type> allSkills = new Dictionary<PassiveSkillNames, Type>();
        [SerializeField] int skillCount;
        public void PassiveSkillsInit()
        {
            if (allSkills.Count > 0) return;
            var getskills = Assembly.GetAssembly(typeof(PassiveSkillBase)).GetTypes()
                                   .Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(PassiveSkillBase)));

            foreach (var getSkill in getskills)
            {
                var t = Activator.CreateInstance(getSkill) as PassiveSkillBase;                
                allSkills.Add(t.passiveSkillName, getSkill);
            }
            skillCount = allSkills.Count;
        }

        public PassiveSkillBase GetPassiveSkills(PassiveSkillNames pSkillName)
        {
            foreach (var skill in allSkills)
            {
                if (skill.Key == pSkillName)
                {
                    var t = Activator.CreateInstance(skill.Value) as PassiveSkillBase;
                    return t;
                }
            }
            Debug.Log("PASSIVE SKILL NOT FOUND !" + pSkillName);
            return null;
        }
    }
}