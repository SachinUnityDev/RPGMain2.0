using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Stout : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Stout;

        public override void OnApply()
        {
            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd += OnCharStateEnd;
        }

        void OnCharStateStart(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return; 
            if(charStateModData.charStateName == CharStateName.Faithful)
            {
                int buffId = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                   charID, AttribName.vigor, 4, TimeFrame.Infinity, -1, true);
                allBuffIds.Add(buffId);

                buffId = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                   charID, AttribName.hpRegen, 2, TimeFrame.Infinity, -1, true);
                allBuffIds.Add(buffId);
            }

        }


        void OnCharStateEnd(CharStateModData charStateModData)
        {
            if (charController.charModel.charID != charStateModData.effectedCharID) return;
            if (charStateModData.charStateName == CharStateName.Faithful)
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
        public override void OnEndConvert()
        {
            base.OnEndConvert();
            if (50f.GetChance())
            {
                charController.tempTraitController
                .ApplyTempTrait(CauseType.TempTrait, (int)tempTraitName, charID, TempTraitName.Tough);
            }
        }
    }
}
