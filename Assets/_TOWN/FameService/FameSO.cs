using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Common
{
    [System.Serializable]
    public class FameBehaviorMap
    {
        public FameBehavior fameBehavior;
        public List<FameType> allAntiFameTypes = new List<FameType>();
    }
    [System.Serializable]

    public class FameRangeData
    {
        public FameType FameType;
        public int rangeStart;
        public int rangeEnd; 
    }
    [System.Serializable]

    public class FameStrData
    {
        public FameType fameType;
        public string fameStr; 

    }
    [System.Serializable]
    public class fameBehaviorDescData
    {
        public FameBehavior famebehavior;
        public string desc;

    }
    [CreateAssetMenu(fileName = "FameSO", menuName = "Fame Service/FameSO")]
    public class FameSO : ScriptableObject
    {
        public List<FameBehaviorMap> allFameBehaviorMaps = new List<FameBehaviorMap>();
        public List<FameRangeData> allFameRangeData = new List<FameRangeData>();
        public List<FameStrData> allFameStrData = new List<FameStrData>();

        public List<fameBehaviorDescData> allfameBehaviorDesc = new List<fameBehaviorDescData>();

        public string GetFameBehaviorStr(FameBehavior fameBehavior)
        {
            string str = allfameBehaviorDesc.Find(t => t.famebehavior == fameBehavior).desc;
            return str;
        }

        public string GetFameTypeStr(FameType fameType)
        {
            string str = allFameStrData.Find(t => t.fameType == fameType).fameStr;
            return str;
        }

    }
}

