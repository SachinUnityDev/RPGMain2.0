﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Quest; 
namespace Common
{
    public class SwornRivals : PermaTraitBase
    {   
        public override PermaTraitName permaTraitName => PermaTraitName.SwornRivals;
        public override void ApplyTrait()
        {
            CharService.Instance.OnPartyLocked += ApplyHasteBuff;
            CharService.Instance.OnCharDeath += DeathNFleeChk;
            CharService.Instance.OnCharFleeQuest += DeathNFleeChk;
        }

        void DeathNFleeChk(CharController charController)
        {
            if (charController.charModel.cultType == CultureType.Kugharian
                && charController.charModel.charID != charID)
                ApplyHasteBuff();
        }

        void ApplyHasteBuff()
        {
            int count = 0;
            ClearBuffs();
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {

                if (charController.charModel.cultType == CultureType.Kugharian
                    && charController.charModel.charID != charID)
                    if (charCtrl.charModel.stateOfChar == StateOfChar.UnLocked)
                        count++;
            }
            for (int i = 0; i < count; i++)
            {
                int buffID = charController.buffController.ApplyBuff(CauseType.PermanentTrait, (int)permaTraitName
                                    , charID, AttribName.morale, -3, TimeFrame.Infinity, 1, false);
                allBuffIds.Add(buffID);
            }
        }
    }
}
