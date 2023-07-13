using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{


    public class TradeService : MonoSingletonGeneric<TradeService>
    {

        public AllNPCSO allNPCSO;

        TradeController tradeController; 
        
        public void InitTradeService()
        {
            tradeController = GetComponent<TradeController>();
            tradeController.InitController(allNPCSO);
        }



    }
}