using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

namespace Common
{
    [Serializable]
    public class BarkLineData
    {
        [TextArea(2,10)]
        public string barkline;
        public AudioClip barkAudio; 
    }


    [CreateAssetMenu(fileName = "QuestPrepAbbasSO", menuName = "Common/BarkService/QuestPrepSO")]
    public class QuestPrepBarkSO : ScriptableObject
    {
        
        public CharNames CharName; 
        public List<BarkLineData> barkLines = new List<BarkLineData>(); 

    }
}