using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class WetAndHappy : PermaTraitBase
    {
        //Neglects negative specs of soaked status	
        //+3 morale when soaked
        public override PermaTraitName permaTraitName => PermaTraitName.WetAndHappy;
        public override void ApplyTrait()
        {
            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd += OnCharStateEnd;

        }
        void OnCharStateStart(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return;
            if (charStateModData.charStateName == CharStateName.Soaked)
            {
                int buffId =
                    charController.buffController.ApplyBuff(CauseType.PermanentTrait, (int)permaTraitName,
                                           charID, AttribName.morale, +4, TimeFrame.Infinity, -1, true);
                allBuffIds.Add(buffId);
                buffId =
                 charController.buffController.ApplyBuff(CauseType.PermanentTrait, (int)permaTraitName,
                                        charID, AttribName.focus, +2, TimeFrame.Infinity, -1, true);
                allBuffIds.Add(buffId);
            }
        }
        void OnCharStateEnd(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return;
            if (charStateModData.charStateName == CharStateName.Soaked)
            {
                foreach (int buffId in allBuffIds)
                {
                    charController.buffController.RemoveBuff(buffId);
                }
            }
        }
    }
}

