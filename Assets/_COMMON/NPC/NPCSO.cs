using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Common;
using System;

namespace Town
{
  

    [CreateAssetMenu(fileName = "NPCSO", menuName = "Char Service/NPCSO")]

    public class NPCSO : ScriptableObject
    {
        public int npcID;
        public NPCNames npcName;
        public GameObject npcPrefab;  // change to prefab 
        public Sprite npcSprite;
        public Sprite npcHexPortrait;
        public Sprite dialoguePortraitClicked;
        public Sprite dialoguePortraitUnClicked;

        [Header("Interaction related")]
        public string InteractHeading; 
        public List<string> InteractionOptions = new List<string>();

        [Header("Items purchaseAble by NPC")]
        public List<ItemType> itemTypesAccepted = new List<ItemType>();

       
    }

    [Serializable]
    public class NPCWeeklyStockData
    {
        public int WeekInYear;
        public List<ItemDataWithQty> allItemData = new List<ItemDataWithQty>();

        public NPCWeeklyStockData(int weekInYear, List<ItemDataWithQty> allItemData)
        {
            WeekInYear = weekInYear;
            this.allItemData = allItemData;
        }
    }


}

