using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    public class TradeController : MonoBehaviour
    {       
        
        public List<TradeModel> allTradeModel = new List<TradeModel>();

        void Start()
        {

        }

        public void InitController(AllNPCSO allNPCSO)
        {
            foreach (NPCSO npcSO in allNPCSO.allNPCSO)
            {
                TradeModel tradeModel = new TradeModel(npcSO, 1);// init for week 1
                allTradeModel.Add(tradeModel);
            }
        }

    }
}