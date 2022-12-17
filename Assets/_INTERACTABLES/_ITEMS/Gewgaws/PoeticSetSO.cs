using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using UnityEngine;


namespace Interactables
{
    [System.Serializable]
    public class VerseData
    {
        public GewgawMidNames gewgawMidNames;
        public string verseStr; 

    }

    [CreateAssetMenu(fileName = "PoeticSetSO", menuName = "Item Service/PoeticSetSO")]
    public class PoeticSetSO : ScriptableObject
    {
        public PoeticSetName poeticSetName;

        public List<VerseData> itemVerses = new List<VerseData>();

        [TextArea(5,10)]
        public List<string> bonusBuffStr = new List<string>();  

    }
}
