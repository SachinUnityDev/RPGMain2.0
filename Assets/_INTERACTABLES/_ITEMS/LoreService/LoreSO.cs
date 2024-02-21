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
        public bool isLocked = true;
        public List<Sprite> pics = new List<Sprite>();

        public LoreSubData(SubLores subLoreNames, bool isLocked)
        {
            this.subLoreNames = subLoreNames;
            this.isLocked = isLocked;
        }
    }

    [System.Serializable]
    public class LoreData
    {
        public LoreBookNames loreName;
        public bool isLocked = true;     
        public List<LoreSubData> allSubLore = new List<LoreSubData>();

        public LoreData(LoreBookNames loreName, bool isLocked)
        {
            this.loreName = loreName;
            this.isLocked = isLocked;            
        }
    }
    [System.Serializable]
    public class LoreStrData
    {
        public LoreBookNames loreName; 
        public string loreNameStr;

        public LoreStrData(LoreBookNames loreName, string loreNameStr)
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
                for (int i = 1; i < Enum.GetNames(typeof(LoreBookNames)).Length; i++)
                {
                    LoreStrData loreStrData = new LoreStrData((LoreBookNames)i, "");
                    allLoreStrData.Add(loreStrData);
                }
            }
          
        }

    }




}

