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
        public override SagaicGewgawNames sagaicgewgawName => SagaicGewgawNames.CowryShellBelt;

        public override CharController charController {  get; set; }
        public override List<int> buffIndex { get; set; }
        public override List<string> displayStrs { get; set; }
        public override void GewGawInit()
        {

        }
        public override void ApplyGewGawFX(CharController charController)
        {
            this.charController = charController;
            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd += OnCharStateEnd;
        }
        void OnCharStateStart(CharStateData charStateData)
        {
            if (charStateData.charStateModel.charStateName != CharStateName.Feebleminded
                || charStateData.charStateModel.charStateName != CharStateName.Confused
                || charStateData.charStateModel.charStateName != CharStateName.Despaired
                || charStateData.charStateModel.charStateName != CharStateName.Rooted
                ) return;
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw,
                            (int)sagaicgewgawName, charStateData.causeCharID, StatsName.vigor,
                            +4, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
        }

        void OnCharStateEnd(CharStateData charStateData)
        {
            charController.buffController.RemoveBuff(buffIndex[0]);// vigor buff  
        }

        public override List<string> DisplayStrings()
        {
            return null;
        }

    

        public override void RemoveFX()
        {
            
        }
    }
}
