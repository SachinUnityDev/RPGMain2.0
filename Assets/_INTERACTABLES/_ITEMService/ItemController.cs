using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat;
using System.Linq;
using UnityEditor;
using Town;

namespace Interactables
{
    public class ItemController : MonoBehaviour
    {

        public CharController charController; 
        public ItemModel itemModel;

        public List<Iitems> itemsInPotionActiveInv = new List<Iitems>();
        public List<Iitems> itemInGewgawActiveInv = new List<Iitems>();

        public List<ScrollConsumedData> allScrollConsumed= new List<ScrollConsumedData>();


        public void Init()
        {
           itemModel = new ItemModel();
        }

        void Start()
        {
            CalendarService.Instance.OnStartOfDay += (DayName dayName)=>OnDayTick(); 
        }

        public void OnScrollConsumed(ScrollName scrollName)
        {
            ScrollSO scrollSO = ItemService.Instance.GetScrollSO(scrollName);
            ScrollConsumedData scrollConsumedData = new ScrollConsumedData(scrollName
                                                    , scrollSO.castTime); 
            allScrollConsumed.Add(scrollConsumedData);  
        }

        public bool EnchantTheWeaponThruScroll(GemName gemName)
        {         
            if(ItemService.Instance.CanEnchantGemThruScroll(charController, gemName))
            {
                itemModel.gemChargeData = new GemChargeData(gemName);
                return true;
            }
            return false;
        }

        public bool EnchantInTemple()
        {
            // to be linked to the town scene 
            return false; 
        }
        void OnDayTick()
        {
            foreach (ScrollConsumedData scrollData in allScrollConsumed.ToList())
            {                
                if (scrollData.activeDaysRemaining >= scrollData.activeDaysNet)
                {
                    allScrollConsumed.Remove(scrollData); 
                }
                scrollData.activeDaysRemaining++; 
            }
        }

        //public void AddItemToActiveInvLs(Iitems Iitem)
        //{
        //    if(Iitem.itemType == ItemType.Potions)
        //    {
        //        itemsInPotionActiveInv.Add(Iitem);
                
        //    }else
        //    {
        //        itemInGewgawActiveInv.Add(Iitem);   
        //    }
          
        //}
    }
}

