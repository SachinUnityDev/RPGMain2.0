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
            charController.ChangeStat(CauseType.PoeticGewgaw, (int)poeticSetName
                 , charController.charModel.charID, AttribName.fortOrg, -2);
        }
        public override void RemoveBonusFX()
        {
            CombatEventService.Instance.OnSOC += SOCHaste2Rds;
            CombatEventService.Instance.OnDodge += OnDodgeChar;
            charController.ChangeStat(CauseType.PoeticGewgaw, (int)poeticSetName
                 , charController.charModel.charID, AttribName.fortOrg, 2);
        }
        void OnDodgeChar(CharController _charController)
        {
            if (charController.charModel.charID == _charController.charModel.charID)
            {
                charController.ChangeStat(CauseType.PoeticGewgaw, (int)poeticSetName
                , charController.charModel.charID, AttribName.stamina, 8);
            }
        }
        void SOCHaste2Rds()
        {
            charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticSetName
               , charController.charModel.charID, AttribName.haste, -2, TimeFrame.EndOfRound, 2, false);
        }
    }
}