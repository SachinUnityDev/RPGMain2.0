using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DG.Tweening.DOTweenModuleUtils;


namespace Common
{
    public class WeakGrip : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.WeakGrip;
        //Always triggers min dmg
        public override void OnApply()
        {
            int buffID =
                charController.buffController.SetDmgORArmor2Min(CauseType.TempTrait, (int)tempTraitName
                , charID, AttribName.dmgMin, TimeFrame.Infinity, 1);
            allBuffIds.Add(buffID);
        }

    }
}