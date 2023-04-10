using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class CowryShellBelt : SagaicGewgawBase
    {
        //Gain +5 Vigor when Feebleminded, Confused, Despaired, Rooted	
        //-10% Thirst and +10% Hunger	
        //+3 Morale when Starving, -2 Morale when Unslakable
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.CowryShellBelt;
        bool charStateFX1Applied; 
        public override void GewGawSagaicInit()
        {
            charStateFX1Applied = false; 
        }

        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;
            CharStatesService.Instance.OnCharStateStart += OnCharStateStartFX1;
            CharStatesService.Instance.OnCharStateEnd += OnCharStateEndFX1;

            CharStatesService.Instance.OnCharStateStart += OnCharStateStartFX2;
            CharStatesService.Instance.OnCharStateEnd += OnCharStateEndFX2;
        }

        public override void UnEquipSagaic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }

        void OnCharStateStartFX1(CharStateModData charStateModData)
        {
            if (charStateModData.charStateName != CharStateName.Feebleminded
                || charStateModData.charStateName != CharStateName.Confused
                || charStateModData.charStateName != CharStateName.Despaired
                || charStateModData.charStateName != CharStateName.Rooted
                ) return;
            if (charStateFX1Applied) return; 
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw,
                            (int)sagaicGewgawName, charStateModData.causeByCharID, AttribName.vigor,
                            +5, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
            charStateFX1Applied = true; 
        }

        void OnCharStateEndFX1(CharStateModData  stateModData)
        {
            if (stateModData.charStateName == CharStateName.Feebleminded
               ^ stateModData.charStateName == CharStateName.Confused
               ^ stateModData.charStateName == CharStateName.Despaired
               ^ stateModData.charStateName == CharStateName.Rooted
               ) return; 

                charController.buffController.RemoveBuff(buffIndex[0]);// vigor buff  
        }

        void OnCharStateStartFX2(CharStateModData charStateModData)
        {
            //+3 Morale when Starving, -2 Morale when Unslakable



        }
        void OnCharStateEndFX2(CharStateModData charStateModData)
        {

        }
    }
}
