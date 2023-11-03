using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;


namespace Common
{
    //2 rds	2 dmg per round
    public class LowPoisonDOT : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.PoisonedLowDOT;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public CharController strikerController;
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            int strikerLvl = 0;
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                strikerController = CombatService.Instance.currCharOnTurn;
                strikerLvl = strikerController.charModel.charLvl;
            }
            dmgPerRound = 2 + (strikerLvl / 4);

            bool isBleeding = charController.charStateController.HasCharDOTState(CharStateName.BleedLowDOT);
            bool isPoisoned = charController.charStateController.HasCharDOTState(CharStateName.PoisonedLowDOT);
            bool isBurning = charController.charStateController.HasCharDOTState(CharStateName.BurnLowDOT);

            if (isPoisoned)
            {
                // clear old poison 
                charController.charStateController.ClearDOT(CharStateName.PoisonedLowDOT);
                // APPLY DAMAGE CONTROller 5-6 fortitude damage
                charController.damageController.ApplyDamage(charController, CauseType.CharState, (int)charStateName
                    , DamageType.FortitudeDmg, UnityEngine.Random.Range(5, 7));
            }
            
            ApplyRoundFX();
            CombatEventService.Instance.OnSOT += ApplyRoundFX;
            ApplyPoison();
            if (isBleeding)
            {
                // 4-5 stamina Damage
                charController.damageController.ApplyDamage(charController, CauseType.CharState, (int)charStateName
                    , DamageType.StaminaDmg, UnityEngine.Random.Range(4, 6));

            }
            if (isBurning)
            {
                //  6-9 earth damage 
                charController.damageController.ApplyDamage(charController, CauseType.CharState, (int)charStateName
                 , DamageType.Earth, UnityEngine.Random.Range(6, 10));
            }
        }

        void ApplyPoison()
        {
            int buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                        , charID, AttribName.haste, -2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);
            buffID =
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, AttribName.fireRes, -30, timeFrame, castTime, true);
            allBuffIds.Add(buffID); 

            buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, AttribName.airRes, +20, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName, charID
                  , CharStateName.Shocked, TimeFrame.Infinity, 1);
        }

        void ApplyRoundFX()
        {
            if (CombatService.Instance.currCharOnTurn.charModel.charID != charID) return;
            AttribData statData = charController.GetAttrib(AttribName.earthRes);

            if (statData.currValue > 60f)   // apply damage here
                charController.ChangeStat(CauseType.CharState, (int)charStateName
                    , charID, StatName.health, Mathf.RoundToInt(-dmgPerRound*0.50f));
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
            str0 = "<style=Poison> Poisoned </style>";
            allStateFxStrs.Add(str0);
            str1 = $"-2<style=Poison> Health </style>per rd";
            allStateFxStrs.Add(str1);
            str2 = "-2<style=Attributes> Haste </style>";
            allStateFxStrs.Add(str2);
            str3 = "-30<style=Fire> Fire Res </style>and +20<style=Air> Air Res </style>";
            allStateFxStrs.Add(str3);
            str4 = "Immune to<style=Air> Shocked </style>";
            allStateFxStrs.Add(str4);
        }

        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnSOT -= ApplyRoundFX;          
        }       
    }
}

