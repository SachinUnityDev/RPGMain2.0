using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Interactables
{
    [System.Serializable]
    public class ItemData  
    {
        public ItemType itemType;// should have a item ID for better control
        public int itemName;
        public GenGewgawQ genGewgawQ = GenGewgawQ.None;
        public ItemData(ItemType itemType, int itemName)
        {
            this.itemType = itemType;
            this.itemName = itemName;
        }
        public ItemData(ItemType itemType, int itemName, GenGewgawQ genGewgawQ)
        {
            this.itemType = itemType;
            this.itemName = itemName;
            this.genGewgawQ = genGewgawQ; 
        }
    }
}
