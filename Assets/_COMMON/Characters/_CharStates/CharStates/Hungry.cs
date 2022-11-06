//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//namespace Common
//{
//    public class Hungry : CharStatesBase
//    {
//        CharController charController;
//        public GameObject causedByGO;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Hungry;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }
//        float currHP; 
//        private void Start()
//        {
//            charController = GetComponent<CharController>();
//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this);
//            StateInit(-1, TimeFrame.None, null);
//        }

//      //  Hp regen drops to 0 (if there is one)	Half hp regain from a combat victory

//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
//            //currHP = charController.GetStat(StatsName.health).baseValue; 
//            //charController.SetStat(StatsName.health, 0f, true); 
           
//            charController.OnStatChanged += Tick2;

//        }
//        // Between %60 - %100 Hunger	heal hunger further below 60%
       
//        protected override void Tick()
//        {



//        }


//        // start:Between %60 - %100 Hunger.............end: heal hunger further below 60%
//        void Tick2(CharModData charModData)  //  change Stat subscribe 
//        {
//            if (charModData.statModfified != StatsName.hunger)
//                return;

//            float maxL = charController.GetStatChanceData(StatsName.hunger).maxLimit;  // i..e 60 
//            if (charController.GetStat(StatsName.hunger).currValue < maxL *0.6f)  // Exit condition 
//            {
//                EndState();
//            }

//        }
//        public override void EndState()
//        {

//            //charController.SetStat(StatsName.health, currHP); 
          
//            charController.OnStatChanged -= Tick2;

//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 

//            Destroy(this);
//        }



//    }
//}