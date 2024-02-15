using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Insane : TempTraitBase
    {
        
        //Physical damage: +20% Dmg Mod
        //	-50 All Res
        public override TempTraitName tempTraitName => TempTraitName.Insane;


        public override void OnApply()
        {
            //int dmgaltBuffID =
            //charController.strikeController.ApplyDmgAltBuff(20f, CauseType.TempTrait, (int)tempTraitName, charID,
            //TimeFrame.Infinity, 1, true, AttackType.None, DamageType.Physical, CultureType.None, RaceType.None);
            //allBuffDmgAltIds.Add(dmgaltBuffID);

            allBuffIds.AddRange(charController.buffController.BuffAllRes(CauseType.TempTrait, (int)tempTraitName
                , charID, -50, TimeFrame.Infinity, 1, false));

            int buffId = charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                   charID, AttribName.acc,-5, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffId);

            CombatEventService.Instance.OnSOT += FriendlyFireChk; 

        }
        void FriendlyFireChk()
        {
            if (50f.GetChance())
                charController.strikeController.ApplyFriendlyFire(charID); 

        }
        public override void EndTrait()
        {
            base.EndTrait();
            CombatEventService.Instance.OnSOT -= FriendlyFireChk;
        }

    }
}