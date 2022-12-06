using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Combat;

namespace Common
{
    public class LowBleedDOT : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.BleedLowDOT;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public CharController strikerController;

        public override void StateApplyFX()
        {
            strikerController = CombatService.Instance.currCharOnTurn;
            int strikerLvl = strikerController.charModel.charLvl;
            if (!charController.charStateController.HasCharDOTState(CharStateName.BurnHighDOT))
            {
                dmgPerRound = 3 + (strikerLvl / 4);
                ApplyFX(); 
                CombatEventService.Instance.OnSOT += ApplyFX;
                if (!charController.charStateController.HasCharDOTState(charStateName))   // already has bleed following FX will not stack up 
                {
                    int buffID = 
                    charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                        , charID, StatsName.dodge, -2, charStateModel.timeFrame, charStateModel.castTime, true);
                    allBuffs.Add(buffID);
                    // Stamina regen add to buff controller
                    buffID = 
                    charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                          , charID, StatsName.staminaRegen, -2, charStateModel.timeFrame, charStateModel.castTime, true);
                    allBuffs.Add(buffID);
                }
            }
            else if (charController.charStateController.HasCharDOTState(CharStateName.PoisonedLowDOT))
            {
                OverLapRulePoison();
            }
        }

        void ApplyFX()
        {
            if (CombatService.Instance.currCharOnTurn.charModel.charID != charID) return; 

            StatData statData = charController.GetStat(StatsName.armor);

            if (statData.currValue >  4)
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, -Mathf.RoundToInt(dmgPerRound *0.40f));
            else
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, -Mathf.RoundToInt(dmgPerRound));

             charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.fortitude, -2); 

        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            int dmg = Mathf.RoundToInt(dmgPerRound); 
            str0 = "<style=Bleed> Bleeding </style>";
            charStateModel.charStateCardStrs.Add(str0);
            str1 = $"-3<style=Bleed> Health </style>per rd";
            charStateModel.charStateCardStrs.Add(str1);
            str2 = "-1<style=Stamina> Stamina Regen </style>";
            charStateModel.charStateCardStrs.Add(str2);
            str3 = "-2<style=Fortitude> Fortitude </style>per rd";
            charStateModel.charStateCardStrs.Add(str3);
            str4 = "-2<style=Attributes> Dodge </style>";
            charStateModel.charStateCardStrs.Add(str4);

        }

        void OverLapRulePoison()
        {
            if (CharStatesService.Instance.HasCharState(charController.gameObject, CharStateName.PoisonedHighDOT))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.PoisonedHighDOT).castTime;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.PoisonedHighDOT).SetCastTime(castTime + 1);
            }
            if (CharStatesService.Instance.HasCharState(charController.gameObject, CharStateName.PoisonedLowDOT))
            {
                int castTime = charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.PoisonedLowDOT).castTime;
                charController.charStateController.allCharBases
                                    .Find(t => t.charStateName == CharStateName.PoisonedLowDOT).SetCastTime(castTime + 1);
            }
        }
    }
}

//int timeElapsed = 0;
//int netDOT = 2;
//CharController charController;
//public override int castTime { get => netDOT; set => base.castTime = value; }

//public override CharStateName charStateName => CharStateName.BleedLowDOT;
//public override StateFor stateFor => StateFor.Mutual;
//public override float dmgPerRound => 2.0f;

//// Start is called before the first frame update
//void Start()
//{
//    charController = GetComponent<CharController>();
//    charID = charController.charModel.charID;
//    CombatEventService.Instance.OnEOR += TickState;

//}


//protected void TickState()
//{
//    timeElapsed++;

//    //CharacterController characterController = gameObject.GetComponent<CharacterController>();

//    StatData statData = charController.GetStat(StatsName.armor);
//    if (statData.currValue > (statData.minRange + 5))
//        charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, Mathf.RoundToInt(-2 / 2));
//    else
//        charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, -2);

//    if (timeElapsed >= castTime)
//    {
//        EndState();
//    }
//}

//public override void EndState()
//{
//    charController.charModel.InCharStatesList.Remove(charStateName);
//    CombatEventService.Instance.OnEOR -= TickState;
//    Destroy(this);
//}
