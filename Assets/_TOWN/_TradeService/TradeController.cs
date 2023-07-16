using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    public class TradeController : MonoBehaviour
    {       
        
        public List<TradeModel> allTradeModel = new List<TradeModel>();
        public AllNPCSO allNPCSO;

        [SerializeField] int weekSeq; 
        void Start()
        {

        }

        public void InitController(AllNPCSO allNPCSO)
        {
            this.allNPCSO= allNPCSO;
            foreach (NPCSO npcSO in allNPCSO.allNPCSO)
            {
                TradeModel tradeModel = new TradeModel(npcSO, 1);// init for week 1
                allTradeModel.Add(tradeModel);
            }
        }

        public void UpdateWeekStock(int weekSeq)
        {
            this.weekSeq= weekSeq;
            foreach (NPCSO npcSO in allNPCSO.allNPCSO)
            {
                TradeModel tradeModel = new TradeModel(npcSO, weekSeq);
                allTradeModel.Add(tradeModel);
            }
        }

        public TradeModel GetTradeModel(NPCNames npcNames) 
        {
            int index = allTradeModel.FindIndex(t=>t.npcName== npcNames);
            if (index != 0)
                return allTradeModel[index]; 
            else
                Debug.Log("trade model not found" + npcNames.ToString());
            return null;
        }
    }
}