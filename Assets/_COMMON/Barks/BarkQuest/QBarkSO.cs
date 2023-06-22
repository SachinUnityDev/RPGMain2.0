using Quest;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(fileName = "QBarkSO", menuName = "Common/BarkService/QBarkSO")]
    public class QBarkSO : ScriptableObject
    {
        public List<BarkData> allBarkData = new List<BarkData>();

        public List<BarkData> GetBarkData(List<QBarkNames> qbarkNames)
        {
            List<BarkData> barkDatas= new List<BarkData>();
            foreach (QBarkNames name in qbarkNames)
            {
                int index = allBarkData.FindIndex(t => t.qBarkName == name); 
                if (index != -1)
                {
                    barkDatas.Add(allBarkData[index]); 
                }
            }
            return barkDatas;
        }
    }

    [System.Serializable]
    public class BarkData
    {
        public QBarkNames qBarkName;
        public List<QuestMode> InValidQMode = new List<QuestMode>();
        public List<BarkCharData> charData = new List<BarkCharData>();
    }
    [System.Serializable]
    public class BarkCharData
    {
        public CharNames charName;
        [TextArea(2,5)]
        public string str;
        public AudioSource audioSource;      
    }

  




}