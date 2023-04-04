using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DG.Tweening.DOTweenModuleUtils;


namespace Interactables
{
    public class SharpShootersVest : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.SharpshootersVest;

        //  +14% dmg mod on Ranged Physical skills
        // 	+2-3 Dodge	
        // 	-30% Thirst	mod
        // 	+2-3 Willpower

        int valDodge =0 , valWp = 0; 
        public override void GewGawSagaicInit()
        {
             valDodge = UnityEngine.Random.Range(2, 4);
             valWp = UnityEngine.Random.Range(2, 4);
        }

 
 
        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;

            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                   (int)sagaicGewgawName, AttribName.dodge, valDodge, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);


            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                   (int)sagaicGewgawName, AttribName.willpower, valWp, TimeFrame.Infinity, -1, true);
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

