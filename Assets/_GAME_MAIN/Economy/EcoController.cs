using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quest;
using Interactables;
using System;
using Town;

namespace Common
{
    public class EcoController : MonoBehaviour
    {
        public EconoModel econoModel;
        void Start()
        {
            QuestEventService.Instance.OnEndOfQuest += ShareLoot2Companions;
        }

        public void InitEcoController(EconoModel econoModel)
        {
            this.econoModel= econoModel;// make it one EconoModel
        }
        public void ShareLoot2Companions()
        {
            int sharePercent = 0; 
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                sharePercent += (int)charCtrl.charModel.earningsShare; 
            }
            if(sharePercent >100)  // error check
                sharePercent= 100;

            if (sharePercent <= 100)
            {
                float bronzeCurr = econoModel.moneyGainedInQ.BronzifyCurrency() * (float)((100 - sharePercent) / 100);
                Currency curr = (new Currency(0,(int)bronzeCurr)).RationaliseCurrency();

                EcoService.Instance.AddMoney2PlayerInv(curr);
                econoModel.moneyGainedInQ = new Currency(0, 0);
            }

        }
        //public void ApplyTrophyNPelt(float mult)
        //{// find all pelt and trophy.. generate a list .... applyweekEvent
        //    Dictionary<TGNames, Type> allTG = ItemService.Instance.itemFactory.GetAllTradeGoodsLs();
        //    foreach (var tg in allTG)
        //    {
        //        ITrophyable itrophy = tg.Value as ITrophyable; 
        //        if(itrophy != null)
        //        {

        //        }
        //    }
        //}
       
        public void ApplyWeekEventCostMultiplier(List<EventCostMultData> allEventCostMultData)
        {
            econoModel.allWeekEventCostData.Clear();
            foreach (var eventCostMultData in allEventCostMultData)
            {
                econoModel.allWeekEventCostData.Add(eventCostMultData);
            }            
        }
        public float GetEventModifier(ItemType itemType)
        {
            //if (econoModel.allWeekEventCostData == null)
            //    return 1f;
            foreach (EventCostMultData multData in econoModel.allWeekEventCostData)
            {
                if (multData.itemType == itemType)
                    return multData.multiplier; 
            }
            return 1f;
        }

    }

    public class EventCostMultData
    {
        public ItemType itemType;
        public float multiplier;

        public EventCostMultData(ItemType itemType, float multiplier)
        {
            this.itemType = itemType;
            this.multiplier = multiplier;
        }
    }
}