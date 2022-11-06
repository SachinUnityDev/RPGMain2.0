using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 


namespace Common
{
    [System.Serializable]
    public class LvlExpData
    {
        public int charLvl;
        public int deltaExpPts;
        public int totalExpPts; 
    }

    [CreateAssetMenu(fileName = "LvlNExpSO", menuName = "Character Service/LvlNExpSO")]
    public class LvlNExpSO : ScriptableObject
    {
        public List<LvlExpData> allLvlExpData = new List<LvlExpData>();

        public int GetTotalExpPts4Lvl(int charlvl)
        {
           return  allLvlExpData.Find(t => t.charLvl == charlvl).totalExpPts; 
        }
        public int GetdeltaExpPts4Lvl(int charlvl)
        {
            return allLvlExpData.Find(t => t.charLvl == charlvl).deltaExpPts;
        }


        private void Awake()
        {
            int accExp = 0; 
            for (int i = 0; i < allLvlExpData.Count; i++)
            {
                accExp += allLvlExpData[i].deltaExpPts;
                allLvlExpData[i].totalExpPts = accExp;
            }
         
        }
    }

}


