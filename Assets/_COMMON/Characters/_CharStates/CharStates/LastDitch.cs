//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat; 

//namespace Common
//{
//    public class LastDitch : CharStatesBase
//    {
//        CharController charController;
//        public GameObject causedByGO;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.LastDitch;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }

//        private void Start()
//        {
//            charController = GetComponent<CharController>();
//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this);

//            StateInit(-1, TimeFrame.None, null);

//        }

//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
//            CombatEventService.Instance.OnEOC += Tick;
//        }

//        protected override void Tick()
//        {
//            EndState();
//        }
//        public override void EndState()
//        {
//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 
//            Destroy(this);
//        }



//    }
//}