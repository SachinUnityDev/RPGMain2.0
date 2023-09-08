using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{   
    [CreateAssetMenu(fileName = "SeqBarkSO", menuName = "Common/BarkService/SeqBarkSO")]
    public class SeqBarkSO : ScriptableObject
    { 
        public SeqBarkNames seqbarkName;

        public string barkTitle = "";
        [Header("Bark Owner")]
        public NPCNames npcName;
        public CharNames charName;
        public bool isLocked;
        public bool isRepeatable;

        public List<SeqBarkData> allBarkData = new List<SeqBarkData>();
      
    }
    [System.Serializable]
    public class SeqBarkData
    {
        public CharNames charName;
        public NPCNames npcName; 
        [TextArea(2, 5)]
        public string str;
        public AudioClip audioClip;

        public SeqBarkData(CharNames charName,NPCNames npcName, string str, AudioClip audioClip)
        {
            this.charName = charName;
            this.npcName = npcName; 
            this.str = str;
            this.audioClip = audioClip;
        }
    }


}
