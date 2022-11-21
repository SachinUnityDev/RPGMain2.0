using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Interactables
{
    public class WorldInstanceData
    {
        public int itemName;
        public int currInstance = 0;
        public int maxWorldInstace = 0; // Set for all items on init 
    }

    public class ItemModel  // all the items held by chars
    {

        // add remove items here .. depending upon type add to respective list 
        // do this locally here rather than spreading the process around inv , stash , excess 
        // oeprations
        public CharNames charName;
        List<ItemData> allItems = new List<ItemData> ();

        [Header("POTION RELATED RECORD")]
        public List<PotionModel> potionModel = new List<PotionModel> ();
        public List<GemModel> allGemModel = new List<GemModel> ();
        public List<GenGewgawModel> allGenGewgawModel = new List<GenGewgawModel> ();


        public float potionAddict = 0f;

        [Header("INV RELATED RECORD")]       
        public SlotType potionLoc = SlotType.None; // store in item model 


    


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

