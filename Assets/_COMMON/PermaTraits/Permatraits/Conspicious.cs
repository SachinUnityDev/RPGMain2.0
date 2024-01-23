using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Conspicuous : PermaTraitBase
    {   
        public override PermaTraitName permaTraitName => PermaTraitName.Conspicuous;
        public override void ApplyTrait()
        {   
             charController.charStateController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                         , charID, CharStateName.Sneaky, TimeFrame.Infinity, 1);

            List<int> allPos = new List<int>() { 2,3,4,5,6 };

            charController.buffController.ApplyPosBuff(allPos, CauseType.PermanentTrait, (int)permaTraitName
                                     , charID, AttribName.morale, -3f, TimeFrame.Infinity, 1, false); // removal controlled by buff controller
        }
    }
}

