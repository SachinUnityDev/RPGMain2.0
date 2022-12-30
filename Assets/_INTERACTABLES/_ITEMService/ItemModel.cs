using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Security.Policy;
using System.Linq;

namespace Interactables
{ 

    // item charge data , 
    // it
    public class ScrollReadData
    {
        public ScrollNames scrollName;
        public int activeDaysRemaining;
        public int activeDaysNet;

        public ScrollReadData(ScrollNames scrollName,int activeDaysNet)
        {
            this.scrollName = scrollName;            
            this.activeDaysNet = activeDaysNet;
            this.activeDaysRemaining = 0;
        }
    }

    public class GemChargeData
    {
        public GemNames gemName;
        public int chargeRemaining;
        public int chargeNet;

        public GemChargeData(GemNames gemName)
        {
            this.gemName = gemName;
            this.chargeRemaining = 0;
            this.chargeNet = 12;
        }
    }

    /// <summary>
    /// OCData logs and tracks overconsumption of the asset
    /// </summary>
    [System.Serializable]
    public class OCData // Over consumptions data 
    {
        public ItemType itemType;
        public int itemName;
        public float weight;

        public OCData(ItemType itemType, int itemName, float weight)
        {
            this.itemType = itemType;
            this.itemName = itemName;
            this.weight = weight;
        }
    }


    public class ItemModel  // all the items held by chars
    {
        // item charge saveable format 


        [Header("Gem Socketing")]
        public CharNames charName;
        public List<Iitems> divItemsSocketed = new List<Iitems> ();
        public Iitems supportItemSocketed = null;

        [Header("Potion Overconsumption")]
        public List<Iitems> potionsConsumed = new List<Iitems>();
        public List<OCData> allOCData = new List<OCData> ();

        [Header("Enchantment")]
        public List<Iitems> gemEnchanted = new List<Iitems>();
        public GemChargeData gemChargeData;
        #region  OverConsumption Data 

        public float AddOCData(OCData ocData)
        {
            int index = allOCData.FindIndex(t => t.itemName == ocData.itemName
                                        && t.itemType == ocData.itemType); 
            if (index == -1)
            {              
                allOCData.Add(ocData);
                index = allOCData.Count - 1; 
            }
            else
            {
                allOCData[index].weight += ocData.weight;
            }
            return allOCData[index].weight;
        }

        public void ClearAllOCData()
        {
            allOCData.Clear();  
        }

        public void ClearOCData(ItemData itemData)
        {
            int index = allOCData.FindIndex(t => t.itemName == itemData.ItemName
                                       && t.itemType == itemData.itemType);
            if(index != -1)
            {
                allOCData.RemoveAt(index);
            }
        }
        #endregion

        #region GEMS, ENCHANTMENT AND SCROLLS 
      
        public bool IsNotEnchanted()
        {
            if (gemEnchanted.Count != 0)
                return true; 
            else 
                return false;
        }
        public bool OnItemBeEnchanted(GemBase gemBase)
        {
            // add gemcharge data if new
            // recharge new only if charge is zero otherwise it return false; 
            // save as iitems to enchanted list... remove from Inv List 
            
            gemEnchanted.Add(gemBase as Iitems);

            return false;
        }
        # endregion




        
    }
}














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
