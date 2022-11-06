//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//namespace Common
//{
//    public class Hexed : CharStatesBase
//    {
//        //-3 Focus	-3 Luck
//        CharController charController;
//        public GameObject causedByGO;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Hexed;
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
      
//       // -3 Focus	-3 Luck

//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
          
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, -3);
//        }    

//        protected override void Tick()
//        {



//        }
//        public override void EndState()
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, 3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, 3);

//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 

//            Destroy(this);
//        }




//    }
//}