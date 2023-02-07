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


    }
}