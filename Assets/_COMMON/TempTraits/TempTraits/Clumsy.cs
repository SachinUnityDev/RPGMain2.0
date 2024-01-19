using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Clumsy : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Clumsy;

        public override void OnApply()
        {   
           int buffID = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.acc, -2, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffID);
            CombatEventService.Instance.OnSOT += FriendlyFireChk;        
        }
        void FriendlyFireChk()
        {
            if (10f.GetChance())
                charController.strikeController.ApplyFriendlyFire(charID);
        }
        public override void EndTrait()
        {
            base.EndTrait();
            CombatEventService.Instance.OnSOT -= FriendlyFireChk;
        }
    }
}
