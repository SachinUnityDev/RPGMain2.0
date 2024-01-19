using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Strong : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Strong;
        // Always triggers max  Dmg
        public override void OnApply()
        {
            int buffID =
                charController.buffController.SetDmgORArmor2Max(CauseType.TempTrait, (int)tempTraitName
                , charID, AttribName.dmgMax, TimeFrame.Infinity, 1);
            allBuffIds.Add(buffID);
        }
    }
}