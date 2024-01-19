using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Weakness : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Weakness;
        // Always triggers min Armor and min Dmg Clamp Dodge to 0,


        public override void OnApply()
        {
            int buffID =
                charController.buffController.SetDmgORArmor2Min(CauseType.TempTrait, (int)tempTraitName
                , charID, AttribName.dmgMin, TimeFrame.Infinity, 1);
            allBuffIds.Add(buffID);
            buffID =
                charController.buffController.SetDmgORArmor2Min(CauseType.TempTrait, (int)tempTraitName
              , charID, AttribName.armorMin, TimeFrame.Infinity, 1);
            allBuffIds.Add(buffID);

            buffID =
                charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                       charID, AttribName.dodge, -10, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffID);

            allBuffIds.AddRange(charController.buffController.BuffAllRes(CauseType.TempTrait, (int)tempTraitName
               , charID, -25, TimeFrame.Infinity, 1, false));
        }
    }
}