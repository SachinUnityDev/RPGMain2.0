using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class BrassHandProtectors : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicgewgawName => SagaicGewgawNames.BrassHandProtectors;

        public override CharController charController { get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }

        //-6-10 Air Res +8-12 Earth Res
        //	+1-2 armor
        //	Returned Regards perk doesnt cost stamina.And applicable to all alies, not just adjacent.
        //	Hone Blades has 2 rds cast time

        int valAir, valEarth; 
        public override void GewGawInit()
        {
            valAir = UnityEngine.Random.Range(6, 11);
            valEarth = UnityEngine.Random.Range(8, 13);
            // 
        }
        public override void ApplyGewGawFX(CharController charController)
        {
            this.charController = charController;
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                   (int)sagaicgewgawName, StatsName.airRes, valAir, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicgewgawName, StatsName.earthRes, valEarth, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = charController.buffController.ApplyBuffOnRange(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicgewgawName, StatsName.armor, 1,2, TimeFrame.Infinity, -1, true);
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

