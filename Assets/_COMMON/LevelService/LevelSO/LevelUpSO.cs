using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Common
{
    [System.Serializable]
    public class LvlUpGrpData
    {
        public LvlUpGroup lvlUpGroup;
        public List<LvlUpData> allLvlsInGrp = new List<LvlUpData>();

    }

    [System.Serializable]
    public class LvlUpData
    {
        public int lvl;
        public List<AttribData> statChg = new List<AttribData>(); 
    }

    [System.Serializable]
    public class LvlGrpMap
    {
        public LvlUpGroup grp;
        public List<RaceType> allRaces = new List<RaceType>();
    }


    
    [CreateAssetMenu(fileName = "LevelUpSO", menuName = "Common/LvlUpSO")]

    public class LevelUpSO : ScriptableObject
    {
        public List<LvlUpGrpData> allGrpData = new List<LvlUpGrpData>(); // sensistive data 

        public List<LvlGrpMap> allLvlGrpMap = new List<LvlGrpMap>();

        public List<AttribData> GetLvlUpIncr4Stats(CharModel charModel, int finallvl)
        {
            RaceType raceType = charModel.raceType;
            LvlUpGroup grp = GetCharGroupNo(raceType);
            int targetLvl = (int)finallvl; 
            foreach (LvlUpGrpData grpData in allGrpData)
            {
                if(grpData.lvlUpGroup == grp)
                {
                    foreach (var statChges in grpData.allLvlsInGrp)
                    {
                        Debug.Log("stat changes Lvl list" + statChges.lvl);
                        if (statChges.lvl == (int)targetLvl)
                        {                            
                            List<AttribData> lst = new List<AttribData>();
                            lst = statChges.statChg; 
                            return lst;
                        }
                    }
                    //List<StatData> statChg =
                    //    grpData.allLvlsInGrp.Find(t => t.lvl == (Levels)targetLvl).statChg;
                    //if(statChg == null)
                    //{
                    //    Debug.Log("Target group " + targetLvl + "group " + grp);
                    //    return null;
                    //}                    
                    Debug.Log("Group data" + grpData);
                    return null;
                }
            }
            Debug.Log("Lvl Data not available");
            return null; 
        }

        LvlUpGroup GetCharGroupNo(RaceType raceType)
        {
            foreach (LvlGrpMap grp in allLvlGrpMap)
            {
                if(grp.allRaces.Any(t =>t == raceType))
                {
                    return grp.grp;
                }

            }
            Debug.Log("GROUP NOT FOUND");
            return 0; 
        }
    }
}

