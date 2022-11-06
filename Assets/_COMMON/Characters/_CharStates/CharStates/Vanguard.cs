//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat; 

//namespace Common
//{

//    public class Vanguard : CharStatesBase
//    {

//        public override CharStateName charStateName => throw new System.NotImplementedException();

//        public override CharController charController { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
//        public override int charID { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

//        public override StateFor stateFor => throw new System.NotImplementedException();

//        public override int castTime { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }

//        public override void StateApplyFX()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void StateApplyVFX()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void StateDisplay()
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}

////CharController charController;
////public GameObject causedByGO;
////public override int castTime { get; set; }
////public override float dmgPerRound => 1.0f;
////public override CharStateName charStateName => CharStateName.Vanguard;
////public override StateFor stateFor => StateFor.Mutual;
////public int timeElapsed { get; set; }

////private void Start()
////{
////    charController = GetComponent<CharController>();
////    if (charController.charModel.Immune2CharStateList.Contains(charStateName))
////        Destroy(this);

////}

////public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
////{

////    CombatEventService.Instance.OnEOC += Tick;
////}

////protected override void Tick()
////{

////    EndState();


////}
////public override void EndState()
////{

////    charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 

////    Destroy(this);
////}