using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Resilient : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Resilient; 
        
        public override void OnApply()
        {
            charController.OnStatChg += OnStatChg;
            BuffApply(); 
        }
        
        void OnStatChg(StatModData statModData)
        {
            if (statModData.statModified != StatName.health) return;
            BuffApply();     
        }

        void BuffApply()
        {
            if (allBuffIds.Count != 0) return; 
            StatData statData = charController.GetStat(StatName.health);
            if (statData != null)
            {
                float hpPercent = statData.currValue / statData.maxLimit;
                if (hpPercent < 0.3f)
                {
                    int buffID = charController.buffController.ApplyDmgArmorByPercent(CauseType.TempTrait, (int)tempTraitName
                        , charID, AttribName.armorMax, 50f, TimeFrame.Infinity, 1, true);
                    allBuffIds.Add(buffID);

                    allBuffIds.AddRange(charController.buffController.BuffAllRes(CauseType.TempTrait, (int)tempTraitName
                   , charID, 15, TimeFrame.Infinity, 1, true));
                }
            }
        }

        public override void EndTrait()
        {
            base.EndTrait();
            charController.OnStatChg -= OnStatChg;
        }
    }

}

