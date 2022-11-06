//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat; 

//namespace Common
//{
//   // 3 rds	4 dmg per round


//    public class MedPoisonDOT : CharStatesBase
//    {
//        int netDOT = 3;

//        CharController charController; 
//        public override int castTime { get => netDOT; set => base.castTime = value; }
//        public override float dmgPerRound => 4.0f;
//        public override CharStateName charStateName => CharStateName.PoisonedMedDOT;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }
//        void Start()
//        {
//            charController = GetComponent<CharController>();
//            charID = charController.charModel.charID;
//            CombatEventService.Instance.OnEOR += TickState;
//            OverLapRule();
//        }

//        public override void SetCastTime(int value)
//        {
//            netDOT = value;
//        }
//        public void TickState()
//        {

//            timeElapsed++;

//            //CharacterController characterController = gameObject.GetComponent<CharacterController>();

//            StatData statData = charController.GetStat(StatsName.earthRes);

//            if (statData.currValue > 60.0f)
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health
//                                        , Mathf.RoundToInt(-dmgPerRound / 2));
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

//            List<CharStatesBase> otherPoisonStates = new List<CharStatesBase>();
//            var allCharStates = this.gameObject.GetComponents<CharStatesBase>();
//            foreach (var charState in allCharStates)
//            {
//                if ((charState.charStateName == CharStateName.PoisonedLowDOT) || (charState.charStateName == CharStateName.PoisonedMedDOT)
//                    || (charState.charStateName == CharStateName.PoisonedHighDOT))
//                {
//                    if (charState != this)
//                    {
//                        otherPoisonStates.Add(charState);
//                        if (charState.charStateName == this.charStateName)
//                        {
//                            charState.EndState();     // replensis the cast time  

//                        }
//                        if (charState.charStateName > this.charStateName)
//                        {
//                            if (charState.timeRemaining == 1)
//                            {
//                                int temp = charState.castTime + this.castTime - 1;
//                                charState.SetCastTime(temp);
//                            }

//                            this.EndState(); 
//                        }
//                        if (charState.charStateName < this.charStateName)
//                        {
//                            charState.EndState();  // this is  applied
//                        }
//                    }
//                }
//            }

//        }


//    }
//}
