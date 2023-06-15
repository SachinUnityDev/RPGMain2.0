using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;



namespace Quest
{
    [Serializable]
    public class CurioModel
    {
        public CurioNames curioName;
        public ToolNames toolName;
        public ToolNames toolName2;
        [TextArea(2, 5)]
        public string openDesc = "";
        [TextArea(2, 5)]
        public string interactDesc = "";

        public Currency lootMoneyMin = new Currency(0, 0);
        public Currency lootMoneyMax = new Currency(0, 0);

        [TextArea(2, 10)]
        public string curioBark;

        public CurioModel(CurioSO curioSO)
        {
            this.curioName = curioSO.curioName;
            this.toolName = curioSO.toolName;
            this.toolName2 = curioSO.toolName2;
            this.openDesc = curioSO.openDesc;
            this.lootMoneyMin = curioSO.lootMoneyMin;
            this.lootMoneyMax = curioSO.lootMoneyMax;
            this.curioBark = curioSO.curioBark;
        }

        public Currency GetLootMoney()
        {
            int bronzifyCurrMin = lootMoneyMin.BronzifyCurrency(); 
            int bronzifyCurrMax = lootMoneyMax.BronzifyCurrency();  
            int BronzeCurr = UnityEngine.Random.Range(bronzifyCurrMax, bronzifyCurrMin);
            Currency curr = (new Currency(0,BronzeCurr)).RationaliseCurrency();
            return curr; 
        }

    }


}
