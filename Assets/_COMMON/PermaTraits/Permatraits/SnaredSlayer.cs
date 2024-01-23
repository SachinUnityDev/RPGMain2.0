using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class SnaredSlayer : PermaTraitBase
    {

        public override PermaTraitName permaTraitName => PermaTraitName.SnaredSlayer;      
        public override void ApplyTrait()
        {
            int index = charController.strikeController.ApplyCharStateDmgAltBuff(+20f, CauseType.PermanentTrait
                , (int)permaTraitName, charID, TimeFrame.Infinity, -1, true, CharStateName.Rooted);
            allCharStateDmgAltBuffIds.Add(index);

        }
    }




}

