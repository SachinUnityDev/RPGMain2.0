using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    public class Bleeding : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Bleeding;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public CharController strikerController;
        public override float chance { get; set; }
        bool fxApplied = false; 
        public override void StateApplyFX()
        {
            int strikerLvl = 0;          
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                strikerController = CombatService.Instance.currCharOnTurn;
                strikerLvl = strikerController.charModel.charLvl;
            }
            else
            {
                strikerLvl = 0; 
            } 
             dmgPerRound = 2 + (strikerLvl / 4);

            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.dodge, -2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);
            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.staminaRegen, -1, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            CombatEventService.Instance.OnEOT += ApplyFX;
        
        }
        void ApplyFX()
        {
            if (CombatService.Instance.currCharOnTurn.charModel.charID != charID) return;
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatName.fortitude, -2);

            AttribData statData = charController.GetAttrib(AttribName.armorMin);

            if (statData.currValue > 4)   // apply damage here
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatName.health, Mathf.RoundToInt(-dmgPerRound * 0.50f));
            else
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatName.health, -dmgPerRound);

        }
    
 
        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str1 = $"Lose<style=Bleed> Health </style>per rd";
            allStateFxStrs.Add(str1);
            str2 = "-2<style=Fortitude> Fortitude </style>per rd";
            allStateFxStrs.Add(str2);
            str3 = "-1<style=Stamina> Stamina Regen</style>";
            allStateFxStrs.Add(str3);   
            str4 = "-2<style=Attributes> Dodge </style>";
            allStateFxStrs.Add(str4);
        }

        public override void EndState()
        {
            base.EndState();         
            CombatEventService.Instance.OnEOT -= ApplyFX;
        }
       
    }
}

