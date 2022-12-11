using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DG.Tweening.DOTweenModuleUtils;


namespace Interactables
{
    public class SharpShootersVest : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicgewgawName => SagaicGewgawNames.SharpshootersVest;

        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }
        //  +14% dmg mod on Ranged Physical skills
        // 	+2-3 Dodge	
        // 	-30% Thirst	
        // 	+2-3 Willpower

        int valDodge =0 , valWp = 0; 
        public override void GewGawInit()
        {
             valDodge = UnityEngine.Random.Range(2, 4);
             valWp = UnityEngine.Random.Range(2, 4);
        }

        public override void ApplyGewGawFX(CharController charController)
        {
            this.charController = charController;
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                   (int)sagaicgewgawName, StatsName.dodge, valDodge, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

          
            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                   (int)sagaicgewgawName, StatsName.willpower, valWp, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
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

