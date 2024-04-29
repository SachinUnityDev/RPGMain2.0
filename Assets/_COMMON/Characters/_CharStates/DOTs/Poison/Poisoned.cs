using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    public class Poisoned : CharStatesBase
    {

        public override CharStateName charStateName => CharStateName.Poisoned;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public CharController strikerController;
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            int strikerLvl = 0;
            if (GameService.Instance.currGameModel.gameState == GameState.InCombat)
            {
                strikerController = CombatService.Instance.currCharOnTurn;
                strikerLvl = strikerController.charModel.charLvl;
            }
            else
            {
                strikerLvl = 0; 
            }
            dmgPerRound = 3 + (strikerLvl / 3);

            ApplyRoundFX();
            CombatEventService.Instance.OnSOT += ApplyRoundFX;
            ApplyPoison();     

        }

        void ApplyPoison()
        {
            int buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                        , charID, AttribName.haste, -2, TimeFrame.Infinity, 1, true);
            allBuffIds.Add(buffID);

            buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, AttribName.fireRes, -30, TimeFrame.Infinity, 1, false);
            allBuffIds.Add(buffID);

            buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, AttribName.airRes, +20, TimeFrame.Infinity, 1, true);
            allBuffIds.Add(buffID);

            int immuneBuff = 
            charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName, charID
                    , CharStateName.Shocked, TimeFrame.Infinity, 1);
         
            allImmunityBuffs.Add(immuneBuff); 
        }

        void ApplyRoundFX()
        {
            if (CombatService.Instance.currCharOnTurn.charModel.charID != charID) return;
            AttribData statData = charController.GetAttrib(AttribName.earthRes);

            if (statData.currValue > 60f)   // apply damage here
                charController.ChangeStat(CauseType.CharState, (int)charStateName
                    , charID, StatName.health, Mathf.RoundToInt(-dmgPerRound * 0.50f));
            else
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                    , StatName.health, -dmgPerRound);
        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            int dmg = Mathf.RoundToInt(dmgPerRound);      
            str0 = $"Lose<style=Bleed> Health </style>per rd";
            allStateFxStrs.Add(str0);
            str1 = "-2<style=Attributes> Haste</style>";
            allStateFxStrs.Add(str1);
            str2= "-30<style=Fire> Fire Res </style>and +20<style=Air> Air Res</style>";
            allStateFxStrs.Add(str2);
            str3 = "Immune to<style=Air> Shocked</style>";
            allStateFxStrs.Add(str3);
        }

        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnSOT -= ApplyRoundFX;
        }

    }
}