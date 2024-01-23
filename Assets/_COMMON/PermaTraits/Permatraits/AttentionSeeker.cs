using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class AttentionSeeker : PermaTraitBase
    {
        public override PermaTraitName permaTraitName => PermaTraitName.AttentionSeeker;
        public override void ApplyTrait()
        {
            List<int> allPos = new List<int>() { 1, 7 };

            charController.buffController.ApplyPosBuff(allPos, CauseType.PermanentTrait, (int)permaTraitName
                                     , charID, AttribName.morale, 1f, TimeFrame.Infinity, 1, true); // removal controlled by buff controller
            
            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd += OnCharStateEnd;
        }
        void OnCharStateStart(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return;
            if (charStateModData.charStateName == CharStateName.Vanguard)
            {
                int buffId =
                    charController.buffController.ApplyBuff(CauseType.PermanentTrait, (int)permaTraitName,
                                           charID, AttribName.acc, +1,  TimeFrame.Infinity, -1, true);
                allBuffIds.Add(buffId);
            }
        }
        void OnCharStateEnd(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return;
            if (charStateModData.charStateName == CharStateName.Vanguard)
            {
                foreach (int buffId in allBuffIds)
                {
                    charController.buffController.RemoveBuff(buffId);
                }
            }
        }
    }
}