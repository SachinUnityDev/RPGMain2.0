using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using TMPro;
using Common;

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
                ratCurrency.bronze = currency.bronze % 12;
                ratCurrency.silver += (int)(currency.bronze / 12); 
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



}

