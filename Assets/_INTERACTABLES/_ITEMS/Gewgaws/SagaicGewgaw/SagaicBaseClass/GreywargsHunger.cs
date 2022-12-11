using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class GreywargsHunger : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicgewgawName => SagaicGewgawNames.GreyWargsHunger; 
        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

        //+2 morale for each Kugharian in party(self included)
        //+30% Hunger	
        //+14- 20 Air Res	
        //+20% dmg until eoc when First Blood

        int valAir;

        public override void GewGawInit()
        {
            valAir = UnityEngine.Random.Range(14 , 21);            
        }

        public override void ApplyGewGawFX(CharController charController)
        {
            this.charController = charController;
            foreach (CharController c in CharService.Instance.allCharsInParty)
            {
                if(c.charModel.cultType == CultureType.Kugharian)
                {
                    int buffID = c.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                       (int)sagaicgewgawName, StatsName.morale, 2, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);
                }
            }
            int buffID2 = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
               (int)sagaicgewgawName, StatsName.airRes, valAir, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID2);

            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
        }

        void OnCharStateStart(CharStateData charStateData)
        {
            if(charStateData.charStateModel.charStateName == CharStateName.FirstBlood)
            {
                StatData statData = charController.GetStat(StatsName.damage);
                int buffID = charController.buffController.ApplyBuffOnRange(CauseType.SagaicGewgaw, charController.charModel.charID,
               (int)sagaicgewgawName, StatsName.damage, statData.minRange*1.2f, statData.maxRange * 1.2f
               , TimeFrame.Infinity, -1, true);
                buffIndex.Add(buffID);
            }
        }

        public override List<string> DisplayStrings()
        {
            return null; 
        }

        

        public override void EndFx()
        {
            foreach (int i in buffIndex)
            {
                charController.buffController.RemoveBuff(i);
            }
        }
    }
}


