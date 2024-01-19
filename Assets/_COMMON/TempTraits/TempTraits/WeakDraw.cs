using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class WeakDraw : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Weakdraw;
        //-20 % Dmg Ranged n Physical
        public override void OnApply()
        {
            int dmgAltBuffID = charController.strikeController.ApplyDmgAltBuff(-20f, CauseType.TempTrait, (int)tempTraitName
           , charController.charModel.charID, TimeFrame.Infinity, -1, false, AttackType.Ranged, DamageType.Physical
           , CultureType.None);
            allBuffDmgAltIds.Add(dmgAltBuffID);
        }
    }
}