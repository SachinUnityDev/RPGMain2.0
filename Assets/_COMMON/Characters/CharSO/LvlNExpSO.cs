using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Interactables;

namespace Common
{
    [System.Serializable]
    public class LvlExpData
    {
        public int charLvl;
        public int deltaExpPts;
        public int totalExpPts;
        public Currency clearMindCostAtThisLvl; 
    }

    [CreateAssetMenu(fileName = "LvlNExpSO", menuName = "Character Service/LvlNExpSO")]
    public class LvlNExpSO : ScriptableObject
    {
        public List<LvlExpData> allLvlExpData = new List<LvlExpData>();

        public int GetTotalExpPts4Lvl(int charlvl)
        {
            return allLvlExpData.Find(t => t.charLvl == charlvl).totalExpPts;
        }
        public int GetdeltaExpPts4Lvl(int charlvl)
        {
            return allLvlExpData.Find(t => t.charLvl == charlvl).deltaExpPts;
        }

        public Currency ClearMindMoneyNeeded(int charlvl)
        {
            int index = allLvlExpData.FindIndex(t => t.charLvl == charlvl); 
            if(index != -1)
            {
                return allLvlExpData[index].clearMindCostAtThisLvl; 
            }
            else
            {
                Debug.Log("charlvl value not found" + charlvl);
                return null; 
            }
        }
       
    }

}


