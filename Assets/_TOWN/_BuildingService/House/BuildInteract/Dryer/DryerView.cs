using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Town;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{


    public class DryerView : MonoBehaviour, IPanel
    {
        public  int MAX_SLOT_SIZE = 3; 

        [SerializeField] DryerOptsBtnView dryerOptsBtnView;
        [SerializeField] DryerSlotView dryerSlotView;
        [SerializeField] Button exitBtn;
        [SerializeField] DryerBtnPtrEvents dryerBtnPtrEvents;
        
        [Header(" Slot txt TBR")]
        [SerializeField] TextMeshProUGUI slotTxt;

        [Header(" Select INDEX")]
        [SerializeField] int index = -1;
       
        [SerializeField] List<Iitems> itemDrying = new List<Iitems>();
        
        private void Awake()
        {
            exitBtn.onClick.AddListener(OnExitBtnPressed);

        }
        private void Start()
        {
            CalendarService.Instance.OnStartOfCalDay -= ChurnOutDriedItems;
            CalendarService.Instance.OnStartOfCalDay += ChurnOutDriedItems; 
        }

        void ChurnOutDriedItems(int day)
        {

            foreach (Iitems item in itemDrying)
            {
                if(item.itemType == ItemType.Foods)
                {
                    if((FoodNames)item.itemName == FoodNames.Venison)
                    {
                        Iitems itemNew = 
                        ItemService.Instance.GetNewItem(new ItemData(ItemType.Foods, (int)FoodNames.DriedMeat));
                        InvService.Instance.invMainModel.AddItem2CommORStash(itemNew);
                    }
                    if ((FoodNames)item.itemName == FoodNames.Mutton)
                    {
                        Iitems itemNew =
                        ItemService.Instance.GetNewItem(new ItemData(ItemType.Foods, (int)FoodNames.DriedMeat));
                        InvService.Instance.invMainModel.AddItem2CommORStash(itemNew);
                    }
                    if ((FoodNames)item.itemName == FoodNames.Beef)
                    {
                        Iitems itemNew =
                        ItemService.Instance.GetNewItem(new ItemData(ItemType.Foods, (int)FoodNames.DriedMeat));
                        InvService.Instance.invMainModel.AddItem2CommORStash(itemNew);
                    }
                    if ((FoodNames)item.itemName == FoodNames.Fish)
                    {
                        Iitems itemNew =
                        ItemService.Instance.GetNewItem(new ItemData(ItemType.Foods, (int)FoodNames.DriedFish));
                        InvService.Instance.invMainModel.AddItem2CommORStash(itemNew);
                    }
                }
                if (item.itemType == ItemType.Fruits)
                {
                    if ((FruitNames)item.itemName == FruitNames.Grape)
                    {
                        Iitems itemNew =
                        ItemService.Instance.GetNewItem(new ItemData(ItemType.Fruits, (int)FruitNames.DriedGrape));
                        InvService.Instance.invMainModel.AddItem2CommORStash(itemNew);
                    }
                }
            }
            itemDrying.Clear();
        }

        
        void OnExitBtnPressed()
        {
            UnLoad();
        }

        void InitDryerView()
        {
            dryerOptsBtnView.InitDryerPtrEvents(this);
            dryerOptsBtnView.OnDriedMeatBtnPressed();// default
            int slotSeq = BuildingIntService.Instance.houseController.houseModel.slotSeq;   
            slotTxt.text = $"{slotSeq}/{MAX_SLOT_SIZE}";
        }

        public void DryerItemSelect(int index)
        {
            this.index = index;             
            dryerSlotView.GetComponent<DryerSlotView>().InitDryerSlotView(this, dryerBtnPtrEvents, index);
        }


        public void OnDryerPressed()
        {
            BuildingIntService.Instance.houseController.houseModel.slotSeq++;
            slotTxt.text = $"{BuildingIntService.Instance.houseController.houseModel.slotSeq}/{MAX_SLOT_SIZE}"; 
            if(dryerSlotView.itemSelect != null)
            {
                if (!InvService.Instance.invMainModel.RemoveItemFrmCommInv(dryerSlotView.itemSelect))
                    InvService.Instance.invMainModel.RemoveItemFrmStashInv(dryerSlotView.itemSelect);
                
                itemDrying.Add(dryerSlotView.itemSelect);
                DryerItemSelect(index); // resets the slots 
            }
        }
     
        public void Init()
        {

        }

        public void Load()
        {
            InitDryerView();
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}
