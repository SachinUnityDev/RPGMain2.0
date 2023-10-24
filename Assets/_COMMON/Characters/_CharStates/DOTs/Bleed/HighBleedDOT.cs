using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    public class HighBleedDOT : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.BleedHighDOT;
        public override CharController charController { get; set; }
        public override int charID { get; set; }
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
       
             if(!charController.charStateController.HasCharDOTState(CharStateName.BurnHighDOT))
             {             
                dmgPerRound = 3 + (strikerLvl / 4);
                ApplyFX();                
                CombatEventService.Instance.OnSOT += ApplyFX;
          
              //  CombatEventService.Instance.OnEOR1 += DOTTick;
                if (charController.charStateController.HasCharDOTState(CharStateName.PoisonedLowDOT))
                {
                    OverLapRulePoison();
                }
             }
             
        }

        void DOTTick(int roundNo)
        {
            if (!charController.charStateController.HasCharDOTState(charStateName) && !fxApplied)
             // already has bleed following FX will not stack up 
            {
                // -2 dodge 
                charController.ChangeAttrib(CauseType.CharState, (int)charStateName
                           , charID, AttribName.dodge, -2);

                // stamina regen -1 
                charController.ChangeAttrib(CauseType.CharState, (int)charStateName
                          , charID, AttribName.staminaRegen, -1);
                fxApplied= true;
            }
            // if some other bleed is not reducing fortitude a given round this will reduce it
            if (!charController.charStateController.HasCharDOTState(charStateName))
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatName.fortitude, -2);
        }

        void ApplyFX()
        {
            if (CombatService.Instance.currCharOnTurn.charModel.charID != charID) return;
            AttribData statData = charController.GetAttrib(AttribName.armorMin);

            if (statData.currValue > 4)   // apply damage here
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatName.health, Mathf.RoundToInt(-dmgPerRound*0.40f));
            else
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatName.health, -dmgPerRound);
           
        }
 
        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            int dmg = Mathf.RoundToInt(dmgPerRound);
            str0 = "<style=Bleed> Bleeding </style>";
            charStateCardStrs.Add(str0);
            str1 = $"-3<style=Bleed> Health </style>per rd";
            charStateCardStrs.Add(str1);
            str2 = "-1<style=Stamina> Stamina Regen </style>";
            charStateCardStrs.Add(str2);
            str3 = "-2<style=Fortitude> Fortitude </style>per rd";
            charStateCardStrs.Add(str3);
            str4 = "-2<style=Attributes> Dodge </style>";
            charStateCardStrs.Add(str4);
        }

        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnSOT -= ApplyFX;
         //   CombatEventService.Instance.OnEOR1 -= DOTTick;

            // -2 dodge 
            charController.ChangeAttrib(CauseType.CharState, (int)charStateName
                        , charID, AttribName.dodge, -2);

            // stamina regen -1 
            charController.ChangeAttrib(CauseType.CharState, (int)charStateName
                        , charID, AttribName.staminaRegen, -1);
            fxApplied = false;            
        }
        void OverLapRulePoison()
        {

            if (charController.charStateController.HasCharState(CharStateName.PoisonedHighDOT))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.PoisonedHighDOT).castTime ;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.PoisonedHighDOT).IncrCastTime(1); 
            }
            if (charController.charStateController.HasCharState(CharStateName.PoisonedLowDOT))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.PoisonedLowDOT).castTime;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.PoisonedLowDOT).IncrCastTime(1);
            }
        }
    }
}


//    //4 rds	5 dmg per round

//    public class HighBleedDOT : CharStatesBase
//    {
//        int timeElapsed = 0;

//        int netDOT = 4;
//        CharController charController; 
//        public override int castTime { get => netDOT ; set => base.castTime = value; }        
//        public override CharStateName charStateName => CharStateName.BleedHighDOT;
//        public override StateFor stateFor => StateFor.Mutual;
//        public override float dmgPerRound => 5.0f;
//        // Start is called before the first frame update
//        void Start()
//        {
//            charController = gameObject?.GetComponent<CharController>();
//            charID = charController.charModel.charID;
//            CombatEventService.Instance.OnEOR += TickState;
//        }
//        protected void TickState()
//        {
//            timeElapsed++;

//            //CharacterController characterController = gameObject.GetComponent<CharacterController>();

//            StatData statData = charController.GetStat(StatsName.armor);

//            if (statData.currValue > statData.minRange + 5)
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, Mathf.RoundToInt(-dmgPerRound / 2));
//            else
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, -dmgPerRound);

//            if (timeElapsed >= castTime)
//            {
//                EndState();
//            }
//        }

//        public override void EndState()
//        {
//            charController.charModel.InCharStatesList.Remove(charStateName);
//            CombatEventService.Instance.OnEOR -= TickState;
//            Destroy(this);
//        }


