using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    // 2 rds	1 dmg per round
    //Char with more than 60% FR suffers only half of the dmg per round.

    public class BurnLowDOT : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.BurnLowDOT;
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public CharController strikerController;

        public override void StateApplyFX()
        {
            int strikerLvl = 0;
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                strikerController = CombatService.Instance.currCharOnTurn;
                strikerLvl = strikerController.charModel.charLvl;
            }
            dmgPerRound = 4 + (strikerLvl / 4);

            bool isBleeding = charController.charStateController.HasCharDOTState(CharStateName.BleedLowDOT);
            bool isPoisoned = charController.charStateController.HasCharDOTState(CharStateName.PoisonedLowDOT);
            bool isBurning = charController.charStateController.HasCharDOTState(CharStateName.BurnLowDOT);

            if (isPoisoned && !isBurning)
            {
                castTime++;
            }
            if (isBleeding)
            {
                charController.charStateController.ClearDOT(CharStateName.BleedHighDOT);
            }
            if (isPoisoned && isBurning)
            {
                OverLapRuleBurning();
            }
            if (isBurning)
            {
                // deal 4 - 8 fire dmg(instant)
                // +deal 4 - 8 Fortitude dmg(instnat)
                charController.damageController.ApplyDamage(charController, CauseType.CharState, (int)charStateName
                             , DamageType.Fire, UnityEngine.Random.Range(4,9), false);

                charController.damageController.ApplyDamage(charController, CauseType.CharState, (int)charStateName
                             , DamageType.FortitudeDmg, UnityEngine.Random.Range(4, 9), false);

            }
            else
            {
                ApplyRoundFX();
                CombatEventService.Instance.OnSOT += ApplyRoundFX;
                ApplyBurn();
            }
        }

        void ApplyBurn()
        {
            int buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                        , charID, AttribName.dodge, +2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID= 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, AttribName.waterRes, +24, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            // change to buff 
            int immuneBuff = 
            charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName, charID
               , CharStateName.Soaked, TimeFrame.Infinity, 1);            
            allImmunityBuffs.Add(immuneBuff);   

            List<int> immuneDOTIDs =
            charController.charStateController.ApplyDOTImmunityBuff(CauseType.CharState, (int)charStateName, charID
               , CharStateName.BleedLowDOT, TimeFrame.Infinity, 1, true);
            allImmunityBuffs.AddRange(immuneDOTIDs);        
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
                                                                , StatName.fortitude, -6);

        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            int dmg = Mathf.RoundToInt(dmgPerRound);
            str0 = "<style=Burn> Burning </style>";
            charStateCardStrs.Add(str0);
            str1 = $"-{dmg}<style=Burn> Health </style>per rd";
            charStateCardStrs.Add(str1);
            str2 = "+2<style=Attributes> Dodge </style>";
            charStateCardStrs.Add(str2);
            str3 = "+24<style=Water> Water Res </style>";
            charStateCardStrs.Add(str3);
            str4 = "-6<style=Fortitude> Fortitude </style>per rd";
            charStateCardStrs.Add(str4);
            str5 = "Immune to<style=Water> Soaked </style>and<style=Bleed> Bleeding </style>";
            charStateCardStrs.Add(str5);
        }

        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnSOT -= ApplyRoundFX;          
        }
        void OverLapRuleBurning()
        {

            if (charController.charStateController.HasCharState(CharStateName.BurnHighDOT))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.BurnHighDOT).castTime;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.BurnHighDOT).SetCastTime(castTime + 1);
            }
            if (charController.charStateController.HasCharState(CharStateName.BurnLowDOT))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.BurnLowDOT).castTime;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.BurnLowDOT).SetCastTime(castTime + 1);
            }
        }
    }
}