using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    [Serializable]
    public class HousePurchaseOptsData
    {
        public HousePurchaseOpts houseOpts;
        public Currency currency;
        public bool isPurchased;
        public bool isPurchaseAbleInDemo;

        public HousePurchaseOptsData(HousePurchaseOpts houseOpts, Currency currency, bool isPurchased, bool isPurchaseAbleInDemo)
        {
            this.houseOpts = houseOpts;
            this.currency = currency;
            this.isPurchased = isPurchased;
            this.isPurchaseAbleInDemo = isPurchaseAbleInDemo;
        }
    }

    [Serializable]
    public class HouseModel
    {
        [Header("Interact: Buy Furniture")]
        public List<HousePurchaseOptsData> purchaseOpts = new List<HousePurchaseOptsData> ();

        [Header("Interact: Rest")]
        public bool isBedUpgraded =false;

        [Header("Interact: Provision")]
        public Iitems item;

        [Header("Interact: Stash")]
        public List<Iitems> allItemsInStash = new List<Iitems>();
        public bool isStashUpgraded = false; 

        [Header("Interact:Fermentor")]
        public bool isFermentorPurchased = false;

        [Header("Interact: Dryer")]
        public bool isDryerPurchased = false;

        [Header("Interact: Cora")]
        public bool isCoraPurchased = false;

        [Header("Interact: Drums")]
        public bool isDrumsPurchased = false;

        public List<CharInteractData> charInteract = new List<CharInteractData>();

        public List<NPCInteractData> npcData = new List<NPCInteractData>();   

        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();


        public HouseModel(BuildingSO houseSO)
        {
            buildIntTypes = houseSO.buildingData.buildIntTypes.DeepClone();
            npcData = houseSO.buildingData.npcInteractData.DeepClone();
            charInteract = houseSO.buildingData.charInteractData.DeepClone();

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
