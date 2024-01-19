using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public class Ailurophobia : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Ailurophobia;

        public override void OnApply()
        {
              int dmgAltBuffID =  charController.strikeController.ApplyDmgAltBuff(-20f, CauseType.TempTrait, (int)tempTraitName
               , charController.charModel.charID, TimeFrame.Infinity, -1, false, AttackType.None, DamageType.None
               , CultureType.Feline);
                allBuffDmgAltIds.Add(dmgAltBuffID);

             int dmgrecAltBuffID =
                charController.damageController.ApplyDmgReceivedAltBuff(100f, CauseType.TempTrait, (int)tempTraitName
                , charController.charModel.charID, TimeFrame.Infinity, -1, false, AttackType.None, DamageType.None
                    , CultureType.Feline);
             allBuffDmgRecAltIds.Add((int)dmgrecAltBuffID);
        }


    }
}