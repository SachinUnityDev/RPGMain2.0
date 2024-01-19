using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Anemic : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Anemic;
        public override void OnApply()
        {
            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd += OnCharStateEnd;
        }

        void OnCharStateStart(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return;
            if (charStateModData.charStateName == CharStateName.Bleeding)
            {
                allBuffIds.AddRange(charController.buffController.BuffAllRes(CauseType.TempTrait
                                , (int)tempTraitName, charID, -20f, TimeFrame.Infinity, -1, false)); 
                
                int buffId = charController.buffController.ApplyDmgArmorByPercent(CauseType.TempTrait, (int)tempTraitName,
                                                   charID, AttribName.armorMax, -80, TimeFrame.Infinity, -1, false);
                allBuffIds.Add(buffId);

                buffId = charController.buffController.ApplyDmgArmorByPercent(CauseType.TempTrait, (int)tempTraitName,
                                                   charID, AttribName.armorMin, -80, TimeFrame.Infinity, -1, false);
                allBuffIds.Add(buffId);
            }

        }


        void OnCharStateEnd(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return;
            if (charStateModData.charStateName == CharStateName.Bleeding)
                foreach (int buffId in allBuffIds)
                {
                    charController.buffController.RemoveBuff(buffId);
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

