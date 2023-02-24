using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    public class HighPoisonDOT : CharStatesBase
    {

        public override CharStateName charStateName => CharStateName.PoisonedHighDOT;
        public override CharStateModel charStateModel { get; set; }
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
            dmgPerRound = 6 + (strikerLvl / 3);

            bool isBleeding = charController.charStateController.HasCharDOTState(CharStateName.BleedLowDOT);
            bool isPoisoned = charController.charStateController.HasCharDOTState(CharStateName.PoisonedLowDOT);
            bool isBurning = charController.charStateController.HasCharDOTState(CharStateName.BurnHighDOT);

            if (isPoisoned)
            {                
                charController.charStateController.ClearDOT(CharStateName.PoisonedLowDOT);
                charController.damageController.ApplyDamage(charController, CauseType.CharState, (int)charStateName
                    , DamageType.FortitudeDmg, UnityEngine.Random.Range(5, 7), false);
            }

            ApplyRoundFX();
            CombatEventService.Instance.OnSOT += ApplyRoundFX;
            ApplyPoison();     
            if (isBleeding)
            {
                // 4-5 stamina Damage
                charController.damageController.ApplyDamage(charController, CauseType.CharState, (int)charStateName
                      , DamageType.StaminaDmg, UnityEngine.Random.Range(4, 5), false);       
            }
            if (isBurning)
            {
                //  6-8 earth damage 
                charController.damageController.ApplyDamage(charController, CauseType.CharState, (int)charStateName
                  , DamageType.Earth, UnityEngine.Random.Range(6, 9), false);
            }
        }

        void ApplyPoison()
        {
            int buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                        , charID, StatsName.haste, -2, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffs.Add(buffID);

            buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, StatsName.fireRes, -30, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffs.Add(buffID);

            buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, StatsName.airRes, +20, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffs.Add(buffID);

            
            charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName, charID
                    , CharStateName.Shocked, TimeFrame.Infinity, 1);
         
        }

        void ApplyRoundFX()
        {
            if (CombatService.Instance.currCharOnTurn.charModel.charID != charID) return;
            StatData statData = charController.GetStat(StatsName.earthRes);

            if (statData.currValue > 60f)   // apply damage here
                charController.ChangeStat(CauseType.CharState, (int)charStateName
                    , charID, StatsName.health, Mathf.RoundToInt(-dmgPerRound * 0.50f));
            else
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                    , StatsName.health, -dmgPerRound);


        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            int dmg = Mathf.RoundToInt(dmgPerRound);
            str0 = "<style=Poison> Poisoned </style>";
            charStateModel.charStateCardStrs.Add(str0);
            str1 = $"-{dmg}<style=Poison> Health </style>per rd";
            charStateModel.charStateCardStrs.Add(str1);
            str2 = "-2<style=Attributes> Haste </style>";
            charStateModel.charStateCardStrs.Add(str2);
            str3 = "-30<style=Fire> Fire Res </style>and +20<style=Air> Air Res </style>";
            charStateModel.charStateCardStrs.Add(str3);
            str4 = "Immune to<style=Air> Shocked </style>";
            charStateModel.charStateCardStrs.Add(str4);
        }

        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnSOT -= ApplyRoundFX;

            // to be modified 
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.Shocked);

        }

    }
}



////CharStatesBase highest = this;
//////int maxCastTime = this.castTime - timeElapsed;
////if (otherPoisonStates.Count <= 2) return;

////foreach (var poisonDOT in otherPoisonStates)   // they are all accessed as time remaining
////{
////    if (poisonDOT == this) continue; 

////    if (poisonDOT.timeRemaining > this.timeRemaining)
////        timeRemaining = poisonDOT.timeRemaining;
////    if (poisonDOT.charStName > highest.charStName)
////        highest = poisonDOT;
////}


////if (this != highest)
////{
////    Debug.Log("not highest destroy"); 
////    Destroy(this);
////}
