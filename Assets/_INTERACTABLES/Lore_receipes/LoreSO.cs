using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Interactables
{
    [System.Serializable]
    public class LoreSubData
    {
        public SubLores subLoreNames;
        [TextArea(10,10)]
        public string Loretxt;
    }

    [System.Serializable]

    public class LoreData
    {
        public LoreNames loreName;
        public bool isLocked;
        public List<LoreSubData> allSubData = new List<LoreSubData>();
    }
    [System.Serializable]
    public class LoreStrData
    {
        public LoreNames loreName; 
        public string loreNameStr;

        public LoreStrData(LoreNames loreName, string loreNameStr)
        {
            this.loreName = loreName;
            this.loreNameStr = loreNameStr;
        }
    }

    [CreateAssetMenu(fileName = "LoreSO", menuName = "Inventory Service/LoreSO")]

    public class LoreSO : ScriptableObject
    {
        public List<LoreData> allLoreData = new List<LoreData>();
        public List<LoreStrData> allLoreStrData = new List<LoreStrData>();

        public Sprite loreLockImg;
        public Sprite loreUnLockedN;
        public Sprite loreUnLockedHL; 

        void Awake()
        {
            if (allLoreStrData.Count < 1)
            {
                for (int i = 1; i < Enum.GetNames(typeof(LoreNames)).Length; i++)
                {
                    LoreStrData loreStrData = new LoreStrData((LoreNames)i, "");
                    allLoreStrData.Add(loreStrData);
                }
            }
          
        }

    }




}

