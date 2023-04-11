using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Derring : PermaTraitBase
    {
        public override PermaTraitName permaTraitName => PermaTraitName.Derring;
        public override void ApplyTrait(CharController charController)
        {
            base.ApplyTrait(charController);
            GameEventService.Instance.OnGameModeChg += ApplyOnGameModeChg; 
        }
        void ApplyOnGameModeChg(GameMode gameMode)
        {
            if (gameMode != GameMode.Taunt)
                return;
            int buffID= 
            charController.buffController.ApplyBuff(CauseType.PermanentTrait, (int)permaTraitName,
               charID, AttribName.morale, 2, TimeFrame.Infinity, 100, true); 
               
            allbuffID.Add(buffID);  
        }

        public override void EndTrait()
        {
            base.EndTrait();          
            GameEventService.Instance.OnGameModeChg += ApplyOnGameModeChg;
        }

    }
}