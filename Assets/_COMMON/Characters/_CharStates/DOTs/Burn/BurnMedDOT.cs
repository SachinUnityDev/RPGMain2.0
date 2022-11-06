//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat; 

//namespace Common
//{           //  3 rds	4 dmg per round

//    public class BurnMedDOT : CharStatesBase
//    {
//        int netDOT = 3;

//        CharController charController; 
//        public override int castTime { get => netDOT; set => base.castTime = value; }
//        public override float dmgPerRound => 4.0f;
//        public override CharStateName charStateName => CharStateName.BurnMedDOT;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }
//        void Start()
//        {
//            charController = gameObject?.GetComponent<CharController>();
//            charID = charController.charModel.charID;
//            CombatEventService.Instance.OnEOR += TickState;
//            SurpassRule();

//        }

//        public override void SetCastTime(int value)
//        {
//            netDOT = value;
//        }

//        protected void TickState()
//        {

//            timeElapsed++;

//            //CharacterController characterController = gameObject?.GetComponent<CharacterController>();

//            StatData statData = charController.GetStat(StatsName.fireRes);

//            if (statData.currValue > 60.0f)
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, Mathf.RoundToInt(-dmgPerRound / 2));
//            else
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, -dmgPerRound);

//            if (timeElapsed >= castTime)
//            {
//                Debug.Log("EndState Condition met");
//                EndState();
//            }
//        }

//        public override void EndState()
//        {
//            charController.charModel.InCharStatesList.Remove(charStateName);

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
//                            if (charState.charStateName == CharStateName.BurnHighDOT)
//                            {
//                                charState.EndState();
//                            }
//                            else
//                            {
//                                CharStatesService.Instance.SetCharState(this.gameObject
//                                    , gameObject.GetComponent<CharController>(), charStateName + 1);

//                                charState?.EndState();
//                                this?.EndState();
//                            }

//                            // replensis the cast time  

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
//    }
//}
