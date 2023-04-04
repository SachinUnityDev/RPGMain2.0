using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class HolyKris : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.HolyKris;

        // "In the name of usmu" skill has no CD	
        // +2-4 willpower
        // when Stamina below 30%, gain +30% dmg

        int valWP;
        public override void GewGawSagaicInit()
        {
            valWP = UnityEngine.Random.Range(2, 5);
        }
        
        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                          (int)sagaicGewgawName, AttribName.willpower, valWP, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
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
