using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Interactables
{
    public class ItemModel
    {
        public CharNames charName;
        List<ItemData> allItems = new List<ItemData> ();


        public void AddItem2Ls(Iitems Iitems)
        {
            ItemData itemData = new ItemData(Iitems.itemType, Iitems.itemName); 
            allItems.Add(itemData);

        }
        public void RemoveItemFrmLs(Iitems Iitems)
        {
            ItemData itemData = 
                allItems.Find(t=>t.itemType==Iitems.itemType
                            && t.ItemName == Iitems.itemName);
            if(itemData != null)
                allItems.Remove(itemData);
                    
        }

    }
}

