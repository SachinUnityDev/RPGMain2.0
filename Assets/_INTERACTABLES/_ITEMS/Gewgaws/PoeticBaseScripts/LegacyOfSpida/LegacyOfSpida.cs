using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common;

namespace Interactables
{

    public class LegacyOfSpida : PoeticSetBase
    {
        public override PoeticSetName poeticSetName => PoeticSetName.LegacyOfTheSpida;

        public override void BonusFx()
        {
            charController = InvService.Instance.charSelectController;

            CombatEventService.Instance.OnSOC += SOCHaste2Rds;
            CombatEventService.Instance.OnDodge += OnDodgeChar;
            int buffID = 
            charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticSetName
                 , charController.charModel.charID, AttribName.fortOrg, -2, TimeFrame.Infinity, 1, false);
            allBuffIds.Add(buffID);
        }
        public override void RemoveBonusFX()
        {
            base.RemoveBonusFX();
            CombatEventService.Instance.OnSOC -= SOCHaste2Rds;
            CombatEventService.Instance.OnDodge -= OnDodgeChar;
        }
        void OnDodgeChar(DmgAppliedData dmgAppliedData)
        {
            if (dmgAppliedData.targetController.charModel.charID == charController.charModel.charID)
            {
                charController.ChangeStat(CauseType.PoeticGewgaw, (int)poeticSetName
                , charController.charModel.charID, StatName.stamina, 8);
            }
        }
        void SOCHaste2Rds()
        {
            foreach (CharController charCtrl in CharService.Instance.charsInPlayControllers)
            {
                if (charCtrl.charModel.charMode == CharMode.Enemy)
                {
                    int index =
                      charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticSetName
                            , charController.charModel.charID, AttribName.haste, -2, TimeFrame.EndOfRound, 2, false);
                    allBuffIds.Add(index);
                }
            }
        }
    }
}