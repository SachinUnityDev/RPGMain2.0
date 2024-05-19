using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;


namespace Town
{
    [Serializable]
    public class HousePurchaseOptsData
    {
        public HousePurchaseOpts houseOpts;
        public Currency currency;
        public bool isUpgraded;
        public bool isPurchaseAbleInDemo;

        public HousePurchaseOptsData(HousePurchaseOpts houseOpts, Currency currency, bool isUpgraded, bool isPurchaseAbleInDemo)
        {
            this.houseOpts = houseOpts;
            this.currency = currency;
            this.isUpgraded = isUpgraded;
            this.isPurchaseAbleInDemo = isPurchaseAbleInDemo;
        }
    }
    [Serializable]
    public class DryingData
    {
        public int dayInGame;
        public List<Iitems> allItems;

        public DryingData(int dayInGame, List<Iitems> allItems)
        {
            this.dayInGame = dayInGame;
            this.allItems = allItems;
        }

   
    }


    [Serializable]
    public class HouseModel : BuildingModel
    {
        [Header("Interact: Buy Furniture")]
        public List<HousePurchaseOptsData> purchaseOpts = new List<HousePurchaseOptsData> ();

        [Header("Interact: Rest")]
        public float restChance = 0f;
        public float restChanceOnUpgrade = 60f; 

        [Header("Interact: Provision")]
        public Iitems item;

        [Header("Interact: Dryer")]
        public int slotSeq =0;
        public List<DryingData> allDryingData = new List<DryingData>();   

        public List<Iitems> itemDried = new List<Iitems>();

        public void AddToDryingList(int day, Iitems item)
        {
            if(allDryingData.Any(t=>t.dayInGame == day))
            {
                int index = allDryingData.FindIndex(t=>t.dayInGame==day);
                DryingData dryingData = allDryingData[index];
                dryingData.allItems.Add(item);
                slotSeq++;
            }
            else
            {
                DryingData dryingData = new DryingData(day, new List<Iitems>() { item } );
                allDryingData.Add(dryingData);
                slotSeq++;
            }
        }
        public void RemoveDayInDryingList(int day)
        {
            int index = allDryingData.FindIndex(t => t.dayInGame == day);
            if(index != -1)
            {
                allDryingData.RemoveAt(index);
            }
             
        }

        public void AddToDriedList(Iitems item)
        {           
            itemDried.Add(item);
        }
    
        public void ClearDriedList()
        {
            slotSeq -= itemDried.Count;
            itemDried.Clear();     
        }
        public HousePurchaseOptsData GetHouseOptsInteractData(HousePurchaseOpts houseOpts)
        {
            int index = purchaseOpts.FindIndex(t=>t.houseOpts== houseOpts); 
            if(index != -1)
                return purchaseOpts[index];
            else
            {
                Debug.Log("house options not found" + houseOpts);
                return null; 
            }
        }
        public HouseModel(BuildingModel buildingModel)
        {
            buildingName = buildingModel.buildingName;
            buildState = buildingModel.buildState;
            buildIntTypes = buildingModel.buildIntTypes.DeepClone();
            npcInteractData = buildingModel.npcInteractData.DeepClone();
            charInteractData = buildingModel.charInteractData.DeepClone();

            HousePurchaseOptsData purchaseOpts1 = new HousePurchaseOptsData(HousePurchaseOpts.UpgradeBed, new Currency(18, 0), false, true);
            HousePurchaseOptsData purchaseOpts2 = new HousePurchaseOptsData(HousePurchaseOpts.UpgradeStash, new Currency(15, 0), false, true);
            HousePurchaseOptsData purchaseOpts3 = new HousePurchaseOptsData(HousePurchaseOpts.Fermentor, new Currency(9, 10), false, true);
            HousePurchaseOptsData purchaseOpts4 = new HousePurchaseOptsData(HousePurchaseOpts.Dryer, new Currency(6, 0), false, true);
            HousePurchaseOptsData purchaseOpts5 = new HousePurchaseOptsData(HousePurchaseOpts.Cora, new Currency(7, 4), false, true);
            HousePurchaseOptsData purchaseOpts6 = new HousePurchaseOptsData(HousePurchaseOpts.Drums, new Currency(3, 4), false, true);
            purchaseOpts.AddRange(new List<HousePurchaseOptsData>()
                            { purchaseOpts1, purchaseOpts2, purchaseOpts3, purchaseOpts4, purchaseOpts5, purchaseOpts6 });
        }

        public HouseModel(BuildingSO houseSO)
        {
            buildingName= houseSO.buildingName;

            buildState = houseSO.buildingState; 

            buildIntTypes = houseSO.buildIntTypes.DeepClone();
            npcInteractData = houseSO.npcInteractData.DeepClone();
            charInteractData = houseSO.charInteractData.DeepClone();

            HousePurchaseOptsData purchaseOpts1 = new HousePurchaseOptsData(HousePurchaseOpts.UpgradeBed, new Currency(18, 0), false, true);
            HousePurchaseOptsData purchaseOpts2 = new HousePurchaseOptsData(HousePurchaseOpts.UpgradeStash, new Currency(15, 0), false, true);
            HousePurchaseOptsData purchaseOpts3 = new HousePurchaseOptsData(HousePurchaseOpts.Fermentor, new Currency(9, 10), false, true);
            HousePurchaseOptsData purchaseOpts4 = new HousePurchaseOptsData(HousePurchaseOpts.Dryer, new Currency(6, 0), false, true);
            HousePurchaseOptsData purchaseOpts5 = new HousePurchaseOptsData(HousePurchaseOpts.Cora, new Currency(7, 4), false, true);
            HousePurchaseOptsData purchaseOpts6 = new HousePurchaseOptsData(HousePurchaseOpts.Drums, new Currency(3, 4), false, true);
            purchaseOpts.AddRange(new List<HousePurchaseOptsData>()
                            { purchaseOpts1, purchaseOpts2, purchaseOpts3, purchaseOpts4, purchaseOpts5, purchaseOpts6 }); 
        }
    }
}
