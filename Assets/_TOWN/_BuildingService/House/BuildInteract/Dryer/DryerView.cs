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

        [Header(" Transfer to Inv")]
        [SerializeField] Dryer2InvTransferBtn dryerToInvTransferBtn;

        [Header(" Select INDEX")]
        [SerializeField] int index = -1;
       
      
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
            int  prevDay = day - 1;
            HouseModel houseModel = BuildingIntService.Instance.houseController.houseModel; 
            foreach (DryingData dryingData in houseModel.allDryingData)
            {
                if (dryingData.dayInGame != prevDay) continue;
                foreach (Iitems item in dryingData.allItems)
                {
                    if (item.itemType == ItemType.Foods)
                    {
                        if ((FoodNames)item.itemName == FoodNames.Venison)
                        {
                            Iitems itemNew =
                            ItemService.Instance.GetNewItem(new ItemData(ItemType.Foods, (int)FoodNames.DriedMeat));
                            houseModel.AddToDriedList(itemNew);
                        }
                        if ((FoodNames)item.itemName == FoodNames.Mutton)
                        {
                            Iitems itemNew =
                            ItemService.Instance.GetNewItem(new ItemData(ItemType.Foods, (int)FoodNames.DriedMeat));
                            houseModel.AddToDriedList(itemNew);
                        }
                        if ((FoodNames)item.itemName == FoodNames.Beef)
                        {
                            Iitems itemNew =
                            ItemService.Instance.GetNewItem(new ItemData(ItemType.Foods, (int)FoodNames.DriedMeat));
                            houseModel.AddToDriedList(itemNew);
                        }
                        if ((FoodNames)item.itemName == FoodNames.Fish)
                        {
                            Iitems itemNew =
                            ItemService.Instance.GetNewItem(new ItemData(ItemType.Foods, (int)FoodNames.DriedFish));
                            houseModel.AddToDriedList(itemNew);
                        }
                    }
                    if (item.itemType == ItemType.Fruits)
                    {
                        if ((FruitNames)item.itemName == FruitNames.Grape)
                        {
                            Iitems itemNew =
                            ItemService.Instance.GetNewItem(new ItemData(ItemType.Fruits, (int)FruitNames.DriedGrape));
                            houseModel.AddToDriedList(itemNew);
                        }
                    }
                }
            }
            houseModel.RemoveDayInDryingList(prevDay);             
        }
        public void OnTransfer2InvPressed()
        {
            HouseModel houseModel = BuildingIntService.Instance.houseController.houseModel; 
            foreach (Iitems item in houseModel.itemDried)
            {
                InvService.Instance.invMainModel.AddItem2CommORStash(item);
            }
            houseModel.ClearDriedList();
            slotTxt.text = $"{houseModel.slotSeq}/{MAX_SLOT_SIZE}";
          
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
            dryerToInvTransferBtn.Init(this);
        }

        public void DryerItemSelect(int index)
        {
            this.index = index;             
            dryerSlotView.GetComponent<DryerSlotView>().InitDryerSlotView(this, dryerBtnPtrEvents, index);
        }


        public void OnDryerPressed()
        {
            if(dryerSlotView.itemSelect != null)
            {
                if (!InvService.Instance.invMainModel.RemoveItemFrmCommInv(dryerSlotView.itemSelect))
                    InvService.Instance.invMainModel.RemoveItemFrmStashInv(dryerSlotView.itemSelect);

                int day = CalendarService.Instance.calendarModel.dayInGame; 
                BuildingIntService.Instance.houseController.houseModel.AddToDryingList(day,dryerSlotView.itemSelect);
                DryerItemSelect(index); // resets the slots 
            }
            slotTxt.text = $"{BuildingIntService.Instance.houseController.houseModel.slotSeq}/{MAX_SLOT_SIZE}";
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
