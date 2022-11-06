//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Common
//{
//    public class Radiant : CharStatesBase
//    {
//        CharController charController;
//        public GameObject causedByGO;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Radiant;
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

//       // +2 morale to allies	-2 self luck


//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
            
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, 2);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, -2);

//            charController.OnStatChanged += Tick2;

//        }
      
//        protected override void Tick()
//        {



//        }


//       // S + Light res is max ST + Light res drops

//        void Tick2(CharModData charModData)  //  change Stat subscribe 
//        {
//            if (charModData.statModfified != StatsName.lightRes)
//                return;

//            float maxL = charController.GetStatChanceData(StatsName.lightRes).maxLimit;
//            if (charController.GetStat(StatsName.focus).currValue < maxL)  // Exit condition 
//            {
//                EndState();
//            }

//        }
//        public override void EndState()
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, -2, false);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, 2, false);
          

//            charController.OnStatChanged -= Tick2;

//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 
//            Destroy(this);
//        }
//    }
//}