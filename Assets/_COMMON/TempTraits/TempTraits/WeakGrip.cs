using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DG.Tweening.DOTweenModuleUtils;


namespace Common
{
    public class WeakGrip : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.WeakGrip;
        //Always triggers min Melee Physical Dmg
        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }
        public override void OnEnd()
        {
            
        }
    }
}