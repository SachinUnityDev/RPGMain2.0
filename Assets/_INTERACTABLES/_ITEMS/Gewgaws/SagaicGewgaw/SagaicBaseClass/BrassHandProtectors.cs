using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class BrassHandProtectors : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.BrassHandProtectors;

        //-6-10 Air Res +8-12 Earth Res
        //	+1-2 armor
       
        //	Returned Regards perk doesnt cost stamina.And applicable to all alies, not just adjacent.
        //	Hone Blades has 2 rds cast time

        int valAir, valEarth; 
        public override void GewGawSagaicInit()
        {
            valAir = UnityEngine.Random.Range(6, 11);
            valEarth = UnityEngine.Random.Range(8, 13);
            // 
        }
        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                   (int)sagaicGewgawName, StatsName.airRes, valAir, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicGewgawName, StatsName.earthRes, valEarth, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = charController.buffController.ApplyBuffOnRange(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicGewgawName, StatsName.armor, 1, 2, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
        }

        public override void UnEquipSagaic()
        {
            
        }
    }
}

