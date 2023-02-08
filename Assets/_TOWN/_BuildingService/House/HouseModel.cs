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
    }

    [Serializable]
    public class HouseModel
    {
        [Header("Interact: Buy Furniture")]
        public List<HousePurchaseOptsData> purchaseOpts = new List<HousePurchaseOptsData>();

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

        public List<NPCDataInBuild> npcData = new List<NPCDataInBuild>();   

        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();


        public HouseModel(BuildingSO houseSO)
        {
           buildIntTypes = houseSO.buildingData.buildIntTypes.DeepClone();
            npcData = houseSO.buildingData.npcData.DeepClone();
            charInteract = houseSO.buildingData.charInteractData.DeepClone();
            // buttons at the bottom panel

        }
    }
}
