using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class GreywargsHunger : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.GreyWargsHunger;        

        //+2 morale for each Kugharian in party(self included)
        //+30% Hunger	
        //+14- 20 Air Res	
        //+20% dmg until eoc when First Blood

        int valAir;

        public override void GewGawSagaicInit()
        {
            valAir = UnityEngine.Random.Range(14 , 21);            
        }

        void OnCharStateStart(CharStateData charStateData)
        {
            if(charStateData.charStateModel.charStateName == CharStateName.FirstBlood)
            {
                AttribData statData = charController.GetStat(AttribName.damage);
                int buffID = charController.buffController.ApplyBuffOnRange(CauseType.SagaicGewgaw, charController.charModel.charID,
               (int)sagaicGewgawName, AttribName.damage, statData.minRange*1.2f, statData.maxRange * 1.2f
               , TimeFrame.Infinity, -1, true);
                buffIndex.Add(buffID);
            }
        }
        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;
            foreach (CharController c in CharService.Instance.allCharsInParty)
            {
                if (c.charModel.cultType == CultureType.Kugharian)
                {
                    int buffID = c.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                       (int)sagaicGewgawName, AttribName.morale, 2, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);
                }
            }
            int buffID2 = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
               (int)sagaicGewgawName, AttribName.airRes, valAir, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID2);

            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
        }

        public override void UnEquipSagaic()
        {
            foreach (int i in buffIndex)
            {
                charController.buffController.RemoveBuff(i);
            }
        }
    }
}


