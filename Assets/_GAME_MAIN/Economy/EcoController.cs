using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quest;
using Interactables;
using Town;

namespace Common
{
    public class EcoController : MonoBehaviour
    {
        public EconoModel econoModel;
        void Start()
        {
            QuestEventService.Instance.OnEOQ += ShareLoot2Companions;
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

                EcoServices.Instance.AddMoney2PlayerInv(curr);
                econoModel.moneyGainedInQ = new Currency(0, 0);
            }

        }

        
    }
}