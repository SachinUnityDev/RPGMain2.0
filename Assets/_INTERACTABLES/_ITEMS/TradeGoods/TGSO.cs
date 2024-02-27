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
        public TGNames tgName;
        [Header("Desc")]
        public string desc = "";
        public int maxInvStackSize = 0;
        public Currency cost;
        public float fluctuation;
        public int maxWorldInstance;
        public List<NPCModData> allNPCModData = new List<NPCModData>();

        [Header("Sprites")]
        public Sprite iconSprite;
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();

        [Header("if Trophy / Pelt wall images")]
        public Sprite trophyOrPeltImg_Day;
        public Sprite trophyOrPeltImg_Night;
        public NPCModData GetNPCModData(NPCNames nPCName)
        {
                int index = 
                            allNPCModData.FindIndex(t=>t.npcName == nPCName);
            if(index != -1)
            {
                return allNPCModData[index];
            }
            else
            {
                Debug.Log(" npcName MOD Data not found" + nPCName);
                return null;
            }
        }

    }
}

