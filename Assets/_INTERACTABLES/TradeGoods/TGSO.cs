using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace Interactables
{
    [Serializable]
    public class NPCModData
    {
        public NPCNames npcName;
        public float modVal = 0f;

        public NPCModData(NPCNames npcName, float modVal)
        {
            this.npcName = npcName;
            this.modVal = modVal;
        }
    }

    [CreateAssetMenu(fileName = "TradeGoodSO", menuName = "Interactable/TradeGoodSO")]
    public class TGSO : ScriptableObject
    {
        public TgNames tgName;
        public int maxInventoryStack = 0;
        public Currency cost;
        public float fluctuation;
        public int maxWorldInstance;
        public List<NPCModData> allNPCModData = new List<NPCModData>();

        [Header("Desc")]
        public string desc = "";

        [Header("Sprites")]
        public Sprite iconSprite;
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();
    }
}

