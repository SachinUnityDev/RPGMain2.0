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

        public Currency GetNPCSalePriceWithoutFluctuation(NPCNames nPCName)
        {
            NPCModData nPCModData = 
                            allNPCModData.Find(t=>t.npcName == nPCName);
            if(nPCModData == null) return null;
            
            Currency SPCurrency = new Currency((int)nPCModData.modVal * cost.silver, (int)nPCModData.modVal * cost.bronze);

            return SPCurrency;
            // fluctuation to be a number between say
            //  30 +20%*30, -20%*30

        }

    }
}

