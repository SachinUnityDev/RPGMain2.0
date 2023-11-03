using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    //4 rds	7 dmg per round

    public class BurnHighDOT : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.BurnHighDOT;
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
            dmgPerRound = 6 + (strikerLvl / 3);

            bool isBleeding = charController.charStateController.HasCharDOTState(CharStateName.BleedLowDOT);
            bool isPoisoned = charController.charStateController.HasCharDOTState(CharStateName.PoisonedLowDOT);               
            bool isBurning = charController.charStateController.HasCharDOTState(CharStateName.BurnHighDOT);

            if (isPoisoned && !isBurning)
            {
                castTime++;
            }
            if (isPoisoned && isBurning)
            {
                OverLapRuleBurning();
            }


            if (isBurning)
            {
                charController.damageController.ApplyDamage(charController, CauseType.CharState, (int)charStateName
                           , DamageType.Fire, UnityEngine.Random.Range(4, 9));

                charController.damageController.ApplyDamage(charController, CauseType.CharState, (int)charStateName
                             , DamageType.FortitudeDmg, UnityEngine.Random.Range(4, 9));
            }
            else
            {
                ApplyRoundFX();
                CombatEventService.Instance.OnSOT += ApplyRoundFX;
                ApplyBurn();
            }

            if (isBleeding)
            {
                charController.charStateController.ClearDOT(CharStateName.BleedLowDOT);  
            }
        }

        void ApplyBurn()
        {
            int buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                        , charID, AttribName.dodge, +2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, AttribName.waterRes, +24, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            int immuneBuffID = 
            charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName, charID
                    , CharStateName.Soaked, TimeFrame.Infinity, 1);
            allImmunityBuffs.Add(immuneBuffID);

            List<int> immuneDOTBuffIDs =
            charController.charStateController.ApplyDOTImmunityBuff(CauseType.CharState, (int)charStateName, charID
                    , CharStateName.BleedLowDOT, TimeFrame.Infinity, 1, true);
            allImmunityBuffs.AddRange(immuneDOTBuffIDs);
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
            str0 = $"-{dmg}<style=Burn> Health </style>per rd";
            allStateFxStrs.Add(str0);
            str1 = "+2 Dodge";
            allStateFxStrs.Add(str1);
            str2 = "+24<style=Water> Water Res</style>";
            allStateFxStrs.Add(str2);
            str3 = "-6<style=Fortitude> Fortitude </style>per rd";
            allStateFxStrs.Add(str3);
            str4 = "Immune to<style=Water> Soaked </style>and<style=Bleed> Bleeding </style>";
            allStateFxStrs.Add(str4);
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
                                    .Find(t => t.charStateName == CharStateName.BurnHighDOT).IncrCastTime(1);
            }
            if (charController.charStateController.HasCharState(CharStateName.BurnLowDOT))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.BurnLowDOT).castTime;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.BurnLowDOT).IncrCastTime(1);
            }
        }

    }
}

