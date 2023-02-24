using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Combat
{


    //2 rds	2 dmg per round



    public class LowPoisonDOT : CharStatesBase
    {

        public override CharStateName charStateName => CharStateName.PoisonedLowDOT;
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
            dmgPerRound = 2 + (strikerLvl / 4);

            bool isBleeding = CharStatesService.Instance.HasCharDOTState(charController.gameObject, CharStateName.BleedLowDOT);
            bool isPoisoned = CharStatesService.Instance.HasCharDOTState(charController.gameObject, CharStateName.PoisonedLowDOT);
            bool isBurning = CharStatesService.Instance.HasCharDOTState(charController.gameObject, CharStateName.BurnLowDOT);

            if (isPoisoned)
            {
                // clear old poison 
                CharStatesService.Instance.ClearDOT(charController.gameObject, CharStateName.PoisonedLowDOT);
                // APPLY DAMAGE CONTROller 5-6 fortitude damage
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
                    , DamageType.StaminaDmg, UnityEngine.Random.Range(4, 6), false);

            }
            if (isBurning)
            {
                //  6-9 earth damage 
                charController.damageController.ApplyDamage(charController, CauseType.CharState, (int)charStateName
                 , DamageType.Earth, UnityEngine.Random.Range(6, 10), false);
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
                    , charID, StatsName.health, Mathf.RoundToInt(-dmgPerRound*0.50f));
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
            str1 = $"-2<style=Poison> Health </style>per rd";
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
             
            // To be modified

            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.Shocked);

        }       
    }
}




//        int netDOT = 2;

//        CharController charController; 

//        public override int castTime { get => netDOT; set => base.castTime = value; }
//        public override float dmgPerRound => 2.0f;
//        public override CharStateName charStateName => CharStateName.PoisonedLowDOT;
//        public override StateFor stateFor => StateFor.Mutual;
//        public override int timeRemaining { get; set; }
//        int timeElapsed = 0; 

//        void Start()
//        {
//            charController = GetComponent<CharController>(); 
//            CombatEventService.Instance.OnEOR += TickState;
//            charID = charController.charModel.charID; 
//            OverLapRule();


//        }

//        public override void SetCastTime(int value)
//        {
//            netDOT = value;
//        }
//        public void TickState()
//        {

//            timeElapsed++;
//           // CharacterController charController = gameObject?.GetComponent<CharacterController>();


//            StatData statData = charController.GetStat(StatsName.earthRes);

//            if (statData.currValue > 60.0f)
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, Mathf.RoundToInt(-dmgPerRound / 2));
//            else
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, -dmgPerRound);

//            if (timeElapsed >= castTime)
//            {
//                Debug.Log("EndState Condition met");
//                EndState();
//            }
//            timeRemaining = castTime - timeElapsed;
//        }

//        public override void EndState()
//        {
//            charController.charModel.InCharStatesList.Remove(charStateName);
//            CombatEventService.Instance.OnEOR -= TickState;
//            Destroy(this);
//        }
//        void OverLapRule()
//        {
//            //List<CharStatesBase> otherPoisonStates = new List<CharStatesBase>();
//            //var allCharStates = this.gameObject.GetComponents<CharStatesBase>();
//            //foreach (var charState in allCharStates)
//            //{
//            //    if ((charState.charStName == CharStateName.PoisonedLowDOT) || (charState.charStName == CharStateName.PoisonedMedDOT)
//            //        || (charState.charStName == CharStateName.PoisonedHighDOT))
//            //    {
//            //        if (charState != this)
//            //        {
//            //            otherPoisonStates.Add(charState);
//            //            if (charState.charStName == this.charStName)
//            //            {
//            //                charState.EndState(); 
//            //                // Destroy(charState);    // replensis the cast time  

//            //            }
//            //            if (charState.charStName > this.charStName)  
//            //            {
//            //                if (charState.timeRemaining == 1)
//            //                {
//            //                    int temp = charState.castTime + this.castTime - 1; 
//            //                    charState.SetCastTime(temp); 
//            //                }

//            //                this.EndState(); 
//            //            }
//            //            if (charState.charStName < this.charStName)
//            //            {
//            //                charState.EndState(); 
//            //            }
//            //        }
//            //    }
//            // }

//        }
//    }
//}

////CharStateName tempCharStateName = charState.charStName + 1;
////CharStatesService.Instance.ApplyACharState(this.gameObject, tempCharStateName);


////Debug.Log(charState.castTime);
////int temp = charState.castTime + this.castTime;                           
////charState.SetCastTime(temp);                            
////Debug.Log(charState.castTime);



////foreach (var poisonDOT in allPoisonStates)
////{
////    if (poisonDOT == highest)
////    {
////        poisonDOT.castTime = maxCastTime;

////    }
////    else if (poisonDOT != this)
////    {
////        Destroy(poisonDOT);
////    }           

////}