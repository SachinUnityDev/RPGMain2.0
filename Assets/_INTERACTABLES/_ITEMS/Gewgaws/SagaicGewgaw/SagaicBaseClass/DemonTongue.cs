using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class DemonTongue : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicgewgawName => SagaicGewgawNames.DemonTongue;

        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

        bool onceEnraged = false;
        //12-17 Fire Res	
        //On Flanks: +2 Acc and Dodge (pos 2,3,5,6)	
        // When Burning: Enraged, 3 rds (once per combat)
        //	+3-5 Fort Origin
        int valFire;
        int valFortOrg;
        public override void GewGawInit()  // item init 
        {
            valFire = UnityEngine.Random.Range(12, 17);
            valFortOrg = UnityEngine.Random.Range(3, 5);
            onceEnraged = false;
        }
        public override void ApplyGewGawFX(CharController charController)
        {
            this.charController = charController;
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                  (int)sagaicgewgawName, StatsName.fireRes, valFire, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
               (int)sagaicgewgawName, StatsName.fortOrg, valFortOrg, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            CharStatesService.Instance.OnCharStateStart += OnCharStateChg; 
        }

        void OnCharStateChg(CharStateData charStateData)
        {
            if (onceEnraged) return;
            if(charStateData.charStateModel.charStateName != CharStateName.BurnHighDOT 
                || charStateData.charStateModel.charStateName != CharStateName.BurnMedDOT
                || charStateData.charStateModel.charStateName != CharStateName.BurnLowDOT)
            {

                CharStatesService.Instance.ApplyCharState(charController.gameObject, CharStateName.Enraged,
                    charController, CauseType.SagaicGewgaw, (int)sagaicgewgawName, TimeFrame.EndOfRound, 3); 
                onceEnraged = true;
            }
        }
        public override List<string> DisplayStrings()
        {
            return null;
        }
        public override void RemoveFX()
        {
            CharStatesService.Instance.OnCharStateStart -= OnCharStateChg;
            foreach (int i in buffIndex)
            {
                charController.buffController.RemoveBuff(i);
            }
        }
    }
}

