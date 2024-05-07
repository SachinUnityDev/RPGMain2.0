using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    //4 rds	7 dmg per round

    public class Burning : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Burning;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public CharController strikerController;
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            int strikerLvl = 0;
            if (GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
            {
                strikerController = CombatService.Instance.currCharOnTurn;
                strikerLvl = strikerController.charModel.charLvl;
            }
            else
            {
                strikerLvl = 0; 
            }
            dmgPerRound = 4 + (strikerLvl / 3);
            CombatEventService.Instance.OnEOT += ApplyRoundFX;
            ApplyBurn();

        }

        void ApplyBurn()
        {
            int buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                        , charID, AttribName.dodge, +2, TimeFrame.Infinity, 1, true);
            allBuffIds.Add(buffID);

            buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, AttribName.waterRes, +24, TimeFrame.Infinity, 1, true);
            allBuffIds.Add(buffID);

            int immuneBuffID = 
            charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName, charID
                    , CharStateName.Soaked, TimeFrame.Infinity, 1);
            allImmunityBuffs.Add(immuneBuffID);
        }

        void ApplyRoundFX()
        {
            if (CombatService.Instance.currCharOnTurn.charModel.charID != charID) return;
            AttribData statData = charController.GetAttrib(AttribName.fireRes);

            if (statData.currValue > 60f)   // apply damage here
                charController.ChangeStat(CauseType.CharState, (int)charStateName
                    , charID, StatName.health, Mathf.RoundToInt(-dmgPerRound * 0.50f));
            else
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                    , StatName.health, -dmgPerRound);

            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                                , StatName.fortitude, -5);

        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {   
            str0 = $"Lose<style=Burn> Health </style>per rd";
            allStateFxStrs.Add(str0);
            str1 = "+2 Dodge";
            allStateFxStrs.Add(str1);
            str2 = "+24<style=Water> Water Res</style>";
            allStateFxStrs.Add(str2);
            str3 = "-6<style=Fortitude> Fortitude </style>per rd";
            allStateFxStrs.Add(str3);
            str4 = "Immune to<style=Water> Soaked</style>";
            allStateFxStrs.Add(str4);
        }

        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnEOT -= ApplyRoundFX;
        }
    }
}

