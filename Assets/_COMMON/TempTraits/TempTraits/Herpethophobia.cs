using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Herpethophobia : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Herpethophobia;

        public override void OnApply()
        {
            int dmgAltBuffID = charController.strikeController.ApplyDmgAltBuff(-20f, CauseType.TempTrait, (int)tempTraitName
             , charController.charModel.charID, TimeFrame.Infinity, -1, false, AttackType.None, DamageType.None
             , CultureType.Reptile);
            allBuffDmgAltIds.Add(dmgAltBuffID);

            int dmgrecAltBuffID =
            charController.damageController.ApplyDmgReceivedAltBuff(100f, CauseType.TempTrait, (int)tempTraitName
               , charController.charModel.charID, TimeFrame.Infinity, -1, false, AttackType.None, DamageType.None
                   , CultureType.Reptile);
            allBuffDmgRecAltIds.Add((int)dmgrecAltBuffID);
        }

    }
}