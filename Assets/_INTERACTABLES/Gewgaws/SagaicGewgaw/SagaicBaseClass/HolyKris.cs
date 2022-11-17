using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class HolyKris : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicgewgawName => SagaicGewgawNames.HolyKris;

        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }
        // "In the name of usmu" skill has no CD	
        // +2-4 willpower
        // when Stamina below 30%, gain +30% dmg

        int valWP;
        public override void GewGawInit()
        {
            valWP = UnityEngine.Random.Range(2, 5);
        }
        public override void ApplyGewGawFX(CharController charController)
        {
            this.charController = charController;
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                          (int)sagaicgewgawName, StatsName.willpower, valWP, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
        }

        public override List<string> DisplayStrings()
        {
            return null;
        }

     

        public override void RemoveFX()
        {
            foreach (int i in buffIndex)
            {
                charController.buffController.RemoveBuff(i);
            }
        }
    }
}
