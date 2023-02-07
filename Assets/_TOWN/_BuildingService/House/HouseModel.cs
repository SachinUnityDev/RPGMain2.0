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
        public List<HousePurchaseOptsData> purchaseOpts = new List<HousePurchaseOptsData>();

    }
}