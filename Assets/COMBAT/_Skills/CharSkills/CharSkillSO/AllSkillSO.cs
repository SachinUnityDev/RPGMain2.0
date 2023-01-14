using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{
    [CreateAssetMenu(fileName = "AllSkillDataSO", menuName = "Skill Service/AllSkillDataSO")]

    public class AllSkillSO : ScriptableObject
    {


        public List<SkillDataSO> allSkillDataSO = new List<SkillDataSO>();

    }
}


