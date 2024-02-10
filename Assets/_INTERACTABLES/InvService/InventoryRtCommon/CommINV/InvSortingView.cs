using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Interactables.InvSortingView;

namespace Interactables
{
    public class InvSortingView : MonoBehaviour
    {
        public List<ItemGrp> allItemGrps = new List<ItemGrp>();
        [Header(" Item Group")]
        [SerializeField] ItemGrp selectItemGrp;


        InvRightViewController invRightViewController;
        Transform slotContainer;

        private void Awake()
        {
            FillItemGrps();
        }
        public void InvSortingGrpInit(InvRightViewController invRightViewController)
        {
            this.invRightViewController = invRightViewController;
            slotContainer = invRightViewController.invContainer.transform; 
            FillItemGrps();
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<InvSortBtnPtrEvents>().InitSortBtns(this, allItemGrps[i]);
            }
            ShowAll(); 
        }

        void FillItemGrps()
        {
            allItemGrps.Clear();
            List<ItemType> itemTypes= new List<ItemType>() { ItemType.GenGewgaws, ItemType.SagaicGewgaws, ItemType.PoeticGewgaws };           
            allItemGrps.Add(new ItemGrp(itemTypes));

            itemTypes = new List<ItemType>() { ItemType.Gems };             
            allItemGrps.Add(new ItemGrp(itemTypes));

            itemTypes = new List<ItemType>() { ItemType.Potions };
            allItemGrps.Add(new ItemGrp(itemTypes));

            itemTypes = new List<ItemType>() { ItemType.Herbs, ItemType.Ingredients };
            allItemGrps.Add(new ItemGrp(itemTypes));

            itemTypes = new List<ItemType>() { ItemType.Foods, ItemType.Fruits };
            allItemGrps.Add(new ItemGrp(itemTypes));

            itemTypes = new List<ItemType>() { ItemType.Scrolls, ItemType.LoreBooks };
            allItemGrps.Add(new ItemGrp(itemTypes));

            itemTypes = new List<ItemType>() { ItemType.Tools, ItemType.Pouches };
            allItemGrps.Add(new ItemGrp(itemTypes));

            itemTypes = new List<ItemType>() { ItemType.TradeGoods };
            allItemGrps.Add(new ItemGrp(itemTypes));

            itemTypes = new List<ItemType>() { ItemType.None };
            allItemGrps.Add(new ItemGrp(itemTypes));
        }

        public void OnItemGrpSelected(ItemGrp itemGrp, bool isClicked)
        {
            UnClickAll();
            if (isClicked)
            {
                selectItemGrp = itemGrp;
                ShowItemGrp(itemGrp); 
            }
        }
        void UnClickAll()
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<InvSortBtnPtrEvents>().UnClick();
            }
        }

        public void ShowAll()
        {
            foreach (Transform child in slotContainer)
            {
                child.gameObject.SetActive(true);
            }
        }
        void ShowItemGrp(ItemGrp itemGrp)
        {
            if (itemGrp.itemTypes[0] == ItemType.None) // case for last ItemGrp ie. show all
            {
                ShowAll(); return;
            }

            foreach (Transform child in slotContainer)
            {
                child.gameObject.SetActive(false);
                foreach (ItemType itemType in itemGrp.itemTypes)
                {
                    ItemSlotController itemSlotController = child.GetComponent<ItemSlotController>();
                    if (itemSlotController.ItemsInSlot.Count > 0)
                    {
                        Iitems item = itemSlotController.ItemsInSlot[0];
                        if (item.itemType == itemType)
                        {
                            child.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
            foreach (Transform child in slotContainer)
            {
                ItemSlotController itemSlotController = child.GetComponent<ItemSlotController>();
                if(itemSlotController.ItemsInSlot.Count != 0)
                {
                    child.SetAsFirstSibling();
                }
            }
        }

    

    }


    public class ItemGrp
    {
        public List<ItemType> itemTypes = new List<ItemType>();
        public ItemGrp(List<ItemType> itemTypes)
        {
            this.itemTypes = itemTypes;
        }
    }
}
