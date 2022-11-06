//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat; 

//namespace Common
//{

//    public class FirstBlood : CharStatesBase
//    {
//        CharController charController;
//        public GameObject causedByGO;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.FirstBlood;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }

//        private void Start()
//        {
//            charController = GetComponent<CharController>();
//            charID = charController.charModel.charID;
//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this);

//            StateInit(-1, TimeFrame.None, null); 

//        }
//       // +1 to utility stats until eoc......	+2 fort origin until eoq

//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, 1);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, 1);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, 1);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, 1);


//            CombatEventService.Instance.OnEOC += Tick;
//        }

//        protected override void Tick()
//        {

//            EndState();


//        }
//        public override void EndState()
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -1);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, -1);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, -1);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, -1);

//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 
//            Destroy(this);
//        }




//    }
//}