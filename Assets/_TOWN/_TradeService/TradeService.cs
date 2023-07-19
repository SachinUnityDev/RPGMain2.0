using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{


    public class TradeService : MonoSingletonGeneric<TradeService>
    {
        public event Action OnTradeStart;
        public event Action OnTradeEnds;

        public AllNPCSO allNPCSO;
        public TradeController tradeController;
        public void InitTradeService()
        {
            tradeController = GetComponent<TradeController>();
            tradeController.InitController(allNPCSO);
        }


        public void On_TradeStart()
        {
            OnTradeStart?.Invoke();
        }
        public void On_TradeEnds()
        {
            OnTradeEnds?.Invoke();
        }
    }
        
}