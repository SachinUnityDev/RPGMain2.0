using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Security.Policy;
using System.Linq;
using System;

namespace Interactables
{
    [Serializable]
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
    [Serializable]
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

    [Serializable]
    public class ItemModel  // all the items held by chars
    {

        [Header("Gem Socketing")]
        public CharNames charName;
        public Iitems[] divItemsSocketed = new Iitems[2];
        public Iitems supportItemSocketed = null;

        [Header("Potion Overconsumption")]
        public List<Iitems> potionsConsumed = new List<Iitems>();
        public List<OCData> allOCData = new List<OCData> ();

        [Header("Enchantment")]
        public Iitems gemEnchanted; 
        public GemChargeData gemChargeData;

        [SerializeField] int gemSocketedDiv = 0;
        [SerializeField] int gemSocketedSupport = 0; 

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
            int index = allOCData.FindIndex(t => t.itemName == itemData.itemName
                                       && t.itemType == itemData.itemType);
            if(index != -1)
            {
                allOCData.RemoveAt(index);
            }
        }
        #endregion

        #region GEMS, ENCHANTMENT AND SCROLLS 
      
        public bool IsAlreadyEnchanted()
        {
            if (gemEnchanted == null)
                return false; 
            else 
                return true;
        }
        public bool OnItemBeEnchanted(GemBase gemBase)
        {
            // add gemcharge data if new
            // recharge new only if charge is zero otherwise it return false; 
            // save as iitems to enchanted list... remove from Inv List 
            gemEnchanted = gemBase as Iitems;
            return false;
        }
        #endregion

        #region SOCKET AND UNSOCKET
        public bool CanSocketSupportGem(Iitems item)
        {
            if(supportItemSocketed == null)
                return true;
            return false;
        }
        public bool CanSocketDivGem(Iitems item)
        {
            if (divItemsSocketed[0] == null || divItemsSocketed[1] == null)
                return true;
            return false;
        }

        public void SocketItem2Armor(Iitems item, GemType gemType)
        {
            if (gemType == GemType.Divine) 
            {
                gemSocketedDiv++;
                if (divItemsSocketed[0] == null)
                {
                    divItemsSocketed[0] = item; 
                    IDivGem div = item as IDivGem;
                    div.OnSocketed();
                    return ; 
                }                   
                else if (divItemsSocketed[1] == null)
                {
                    divItemsSocketed[1] = item;
                    IDivGem div = item as IDivGem;
                    div.OnSocketed();
                    return;
                }
            }

            if (gemType == GemType.Support)
            {
                gemSocketedDiv++;
                supportItemSocketed = item;
                ISupportGem support = item as ISupportGem;
                support.OnSocketed();
                return;
            }
        }

        public bool IsUnSocketable()
        {
            if(supportItemSocketed == null && divItemsSocketed[0] == null
                                           && divItemsSocketed[1] == null)
                return true;
            else
                return false;
        }

        #endregion

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
