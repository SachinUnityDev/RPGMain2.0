using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using TMPro;
using Common;
using System.Linq;
using System;

namespace Town
{
    public static class TownExtnMethods 
    {
        public static GameObject FillCurrencyUI(this GameObject go, Currency val)
        {
            go.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text
                 = val.silver.ToString();
            go.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text
               = val.bronze.ToString();

            return go;
        }

        public static Currency RationaliseCurrency(this Currency currency)
        {
            Currency ratCurrency = currency;
            
            if (currency.bronze > 12)
            {                
                int silverAdded = (int)(currency.bronze / 12);
                ratCurrency.silver += silverAdded;
                ratCurrency.bronze -= silverAdded * 12;
            }

            return ratCurrency; 
        }

        public static int BronzifyCurrency(this Currency currency)
        {
            int bronzeCurr = currency.bronze + currency.silver * 12; 
            return bronzeCurr;
        }
        public static BronzifiedRange ApplyCurrencyFluctation(this Currency cost, float flucRate)
        {
            int bronzeVal = cost.BronzifyCurrency(); 
            int bronzeMax = (int)(bronzeVal * (100 + flucRate) / 100f);
            int bronzeMin = (int)(bronzeVal * (100 - flucRate) / 100f);

            BronzifiedRange bronzifiedRange = new BronzifiedRange(bronzeMin, bronzeMax);
            return bronzifiedRange;
        }
    

    }
    [Serializable]
    public class SlotData
    {
        public int Qty =1;
        public int itemName;
        public ItemType itemType; 
        public GenGewgawQ genGewgawQ = GenGewgawQ.None;
        public int maxInvStackSize; 

        public SlotData(int itemName, ItemType itemType, int maxInvStackSize, GenGewgawQ genGewgawQ = GenGewgawQ.None)
        {            
            this.itemName = itemName;
            this.itemType = itemType;
            this.maxInvStackSize = maxInvStackSize;
            this.genGewgawQ= genGewgawQ;
        }
    }

}

