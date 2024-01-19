using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class QuickDraw : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Quickdraw;
        // When First Blood: +20% Dmg  When First Blood: +3 Acc
        public override void OnApply()
        {
            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd += OnCharStateEnd;
        }

        void OnCharStateStart(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return;
            if (charStateModData.charStateName == CharStateName.FirstBlood)
            {
                int buffId = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                   charID, AttribName.acc, 3, TimeFrame.Infinity, -1, true);
                allBuffIds.Add(buffId);

                buffId = charController.buffController.ApplyDmgArmorByPercent(CauseType.TempTrait, (int)tempTraitName,
                                          charID, AttribName.dmgMax, 20f, TimeFrame.Infinity, -1, true);
                allBuffIds.Add(buffId);

                buffId = charController.buffController.ApplyDmgArmorByPercent(CauseType.TempTrait, (int)tempTraitName,
                                          charID, AttribName.dmgMin, 20f, TimeFrame.Infinity, -1, true);
                allBuffIds.Add(buffId);
            }
        }


        void OnCharStateEnd(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return;
            if (charStateModData.charStateName == CharStateName.FirstBlood)
            {
                foreach (int buffId in allBuffIds)
                {
                    charController.buffController.RemoveBuff(buffId);
                }
            }
                
        }
        public override void EndTrait()
        {
            base.EndTrait();
            CharStatesService.Instance.OnCharStateStart -= OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd -= OnCharStateEnd;
        }
    }
}
