using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Tough : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Tough;
        //Always triggers Max Armor
        public override void OnApply()
        {
            int buffID =
                charController.buffController.SetDmgORArmor2Max(CauseType.TempTrait, (int)tempTraitName
                , charID, AttribName.armorMax, TimeFrame.Infinity, 1);
            allBuffIds.Add(buffID);
        }
    }
}
