using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using Interactables;
using System.Security.Policy;

namespace Quest
{ 
    [Serializable]
    public class CurioExpData
    {
        public QuestMode gameMode;
        public int chanceVal;

    }

    [CreateAssetMenu(fileName = "CurioSO", menuName = "Quest/CurioSO")]
    public class CurioSO : ScriptableObject
    {
        public CurioNames curioName;
        public ToolNames toolName;
        public ToolNames toolName2; 
        [TextArea(2,5)]
        public string openDesc = "";


        public Currency lootMoneyMin = new Currency(0, 0);
        public Currency lootMoneyMax = new Currency(0, 0);

        public Sprite curioN;
        public Sprite curioHL;
        public Sprite curioOpn;

        [Header("Curio Bark SO")]
        [TextArea(2, 10)]
        public string curioBark;
        [Header("Audio UI")]
        public AudioClip audioClipUI;
        [Header("Audio VO")]
        public AudioClip audioClipVO; 
    }
}


