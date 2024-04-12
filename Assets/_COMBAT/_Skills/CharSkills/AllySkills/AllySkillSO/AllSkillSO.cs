using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{
    [CreateAssetMenu(fileName = "AllSkillDataSO", menuName = "Skill Service/AllSkillDataSO")]

    public class AllSkillSO : ScriptableObject
    {
        public List<SkillDataSO> allSkillDataSO = new List<SkillDataSO>();
        SkillDataSO GetSkillDataSO(CharNames _charName)
        {
            SkillDataSO skillDataSO = allSkillDataSO.Find(x => x.charName == _charName);
            if (skillDataSO != null)
            {
                return skillDataSO;
            }
            else
            {
                Debug.Log("skill Data SO  Not FOUND" + _charName);
                return null;
            }
        }
    }
}


