using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using Quest;

namespace Interactables
{ 
    [Serializable]
    public class CurioExpData
    {
        public QuestMode gameMode;
        public int chanceVal;

    }

    [CreateAssetMenu(fileName = "CurioSO", menuName = "Interactable/CurioSO")]
    public class CurioSO : ScriptableObject
    {
        public CurioNames curioNames;
        public ToolNames toolName;
        public ToolNames toolName2; 
        [TextArea(2,5)]
        public string openDesc = "";
        [TextArea(2, 5)]
        public string interactDesc = "";

        public List<Currency> lootMoneyRange = new List<Currency>();

        public Sprite curioN;
        public Sprite curioHL;
        public Sprite curioOpn;

        [TextArea(2, 10)]
        public string curioBark; 

        
    }
}


