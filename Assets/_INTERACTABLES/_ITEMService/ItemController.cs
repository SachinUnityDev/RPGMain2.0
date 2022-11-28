using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Interactables
{
    public class ItemController : MonoBehaviour
    {

        public CharController charController; 
        public ItemModel itemModel;

        public List<Iitems> itemsInPotionActiveInv = new List<Iitems>();
        public List<Iitems> itemInGewgawActiveInv = new List<Iitems>();

        public void Init()
        {
           itemModel = new ItemModel();
        }

        void Start()
        {

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

