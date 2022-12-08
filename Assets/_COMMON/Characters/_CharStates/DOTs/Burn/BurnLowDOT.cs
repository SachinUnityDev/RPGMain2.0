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
                        , charID, StatsName.dodge, +2, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffs.Add(buffID);

            buffID= 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, StatsName.waterRes, +24, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffs.Add(buffID);

            // change to buff 
            charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName, charID
               , CharStateName.Soaked, TimeFrame.Infinity, 1, true);            
            charController.charStateController.ApplyDOTImmunityBuff(CauseType.CharState, (int)charStateName, charID
               , CharStateName.BleedLowDOT, TimeFrame.Infinity, 1, true);
            
        }

        void ApplyRoundFX()
        {
            if (CombatService.Instance.currCharOnTurn.charModel.charID != charID) return;
            StatData statData = charController.GetStat(StatsName.fireRes);

            if (statData.currValue > 60f)   // apply damage here
                charController.ChangeStat(CauseType.CharState, (int)charStateName
                    , charID, StatsName.health, Mathf.RoundToInt(-dmgPerRound * 0.50f));
            else
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                    , StatsName.health, -dmgPerRound);

            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                                , StatsName.fortitude, -6);

        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            int dmg = Mathf.RoundToInt(dmgPerRound);
            str0 = "<style=Burn> Burning </style>";
            charStateModel.charStateCardStrs.Add(str0);
            str1 = $"-{dmg}<style=Burn> Health </style>per rd";
            charStateModel.charStateCardStrs.Add(str1);
            str2 = "+2<style=Attributes> Dodge </style>";
            charStateModel.charStateCardStrs.Add(str2);
            str3 = "+24<style=Water> Water Res </style>";
            charStateModel.charStateCardStrs.Add(str3);
            str4 = "-6<style=Fortitude> Fortitude </style>per rd";
            charStateModel.charStateCardStrs.Add(str4);
            str5 = "Immune to<style=Water> Soaked </style>and<style=Bleed> Bleeding </style>";
            charStateModel.charStateCardStrs.Add(str5);
        }

        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnSOT -= ApplyRoundFX;
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.Soaked);
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.BleedLowDOT);
        }
        void OverLapRuleBurning()
        {

            if (CharStatesService.Instance.HasCharState(charController.gameObject, CharStateName.BurnHighDOT))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.BurnHighDOT).castTime;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.BurnHighDOT).SetCastTime(castTime + 1);
            }
            if (CharStatesService.Instance.HasCharState(charController.gameObject, CharStateName.BurnLowDOT))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.BurnLowDOT).castTime;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.BurnLowDOT).SetCastTime(castTime + 1);
            }
        }


    }
}




//        int netDOT = 2;
//        CharController charController; 
//        public override int castTime { get => netDOT ; set => base.castTime = value; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.BurnLowDOT;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }
//        void Awake()
//        {
//            charController = gameObject?.GetComponent<CharController>();
//            charID = charController.charModel.charID; 
//            CombatEventService.Instance.OnEOR += TickState;
//            SurpassRule();
//            timeElapsed = 0; 
//        }

//        public override void SetCastTime(int value)
//        {
//            netDOT = value;
//        }

//        public void TickState()
//        {



//            //CharacterController characterController = gameObject?.GetComponent<CharacterController>();

//            StatData statData = charController.GetStat(StatsName.fireRes);

//            if (statData.currValue > 60.0f)
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health
//                    , Mathf.RoundToInt(-dmgPerRound / 2));
//            else
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, -dmgPerRound);

//            if (timeElapsed >= castTime)
//            {
//                Debug.Log("EndState Condition met");
//                EndState();
//            }
//            timeElapsed++;
//        }

//        public override void EndState()
//        {
//            charController.charModel.InCharStatesList.Remove(charStateName); // added by charStateService 
//            CombatEventService.Instance.OnEOR -= TickState;
//            Destroy(this);
//        }

//        void SurpassRule()
//        {

//            List<CharStatesBase> otherPoisonStates = new List<CharStatesBase>();
//            var allCharStates = this.gameObject.GetComponents<CharStatesBase>();
//            foreach (var charState in allCharStates)
//            {
//                if ((charState.charStateName == CharStateName.BurnLowDOT) || (charState.charStateName == CharStateName.BurnMedDOT)
//                    || (charState.charStateName == CharStateName.BurnHighDOT))
//                {
//                    if (charState != this)
//                    {
//                        otherPoisonStates.Add(charState);
//                        if (charState.charStateName == this.charStateName)
//                        {                                           // upgrade to next level 
//                            if(charState.charStateName == CharStateName.BurnHighDOT)
//                            {
//                                charState.EndState();
//                            } else
//                            {
//                                CharStatesService.Instance.SetCharState(this.gameObject
//                                    ,gameObject.GetComponent<CharController>(), charStateName + 1); 

//                                charState?.EndState();
//                                this?.EndState(); 
//                            }

//                             // replensis the cast time  

//                        }
//                        if (charState.charStateName > this.charStateName)
//                        {
//                            this?.EndState(); 
//                        }
//                        if (charState.charStateName < this.charStateName)
//                        {
//                            charState?.EndState();
//                        }
//                    }
//                }
//            }

//        }