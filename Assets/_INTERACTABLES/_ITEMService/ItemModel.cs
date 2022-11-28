using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Security.Policy;

namespace Interactables
{ 

    // item charge data , 
    // it


    public class ItemModel  // all the items held by chars
    {
        // item charge saveable format 

        public CharNames charName;
        public List<Iitems> itemsSocketed = new List<Iitems> ();
        public List<Iitems> itemsEnchanted = new List<Iitems>(); 
        
        public List<Iitems> potionsConsumed = new List<Iitems>();

        #region 

        public void CanItemBeSocketed()
        {
        

        }
        public void OnItemBeEnchanted()
        {


        }
        


        #endregion 


        //[Header("POTION RELATED RECORD")]
        //public List<PotionModel> potionModel = new List<PotionModel> ();
        //public List<GemModel> allGemModel = new List<GemModel> ();
        //public List<GenGewgawModel> allGenGewgawModel = new List<GenGewgawModel> ();


        //public float potionAddict = 0f;

        //[Header("INV RELATED RECORD")]       
        //public SlotType potionLoc = SlotType.None; // store in item model 





        //public void AddItem2Ls(Iitems Iitems)
        //{
        //    ItemData itemData = new ItemData(Iitems.itemType, Iitems.itemName); 
        //    allItems.Add(itemData);
        //}
        //public void RemoveItemFrmLs(Iitems Iitems)
        //{
        //    ItemData itemData = 
        //        allItems.Find(t=>t.itemType==Iitems.itemType
        //                    && t.ItemName == Iitems.itemName);
        //    if(itemData != null)
        //        allItems.Remove(itemData);

        //}

    }
}

