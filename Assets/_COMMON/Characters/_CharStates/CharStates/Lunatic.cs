//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Common
//{



//    public class Lunatic : CharStatesBase
//    {
//        CharController charController;
//        public GameObject causedByGO;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Lunatic;
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
//        //teammates has -1 focus and morale....	+4 luck to self ....+1 stamina regen at night

//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
//            foreach (CharController c in CharacterService.Instance.allyInPlayControllers)
//            {
//                if (c != charController)
//                {
//                    c.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -1);
//                    c.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, -1);
//                }
//            }
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, 4);
//            charController.OnStatChanged += StaminaIncr; 
//            charController.OnStatChanged += Tick2;

//        }
       
//        void StaminaIncr(CharModData charModData)   // TO BE REVISITED 
//        {
//            if (charModData.statModfified != StatsName.stamina) return; 
//            if (QuestEventService.Instance.questTimeState == TimeState.Night)
//            {
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.stamina, 1, false); 

//            }          


//        }
//        protected override void Tick()
//        {



//        }
//       // S + Dark res is max ST + Dark res drops


//        void Tick2(CharModData charModData)  //  change Stat subscribe 
//        {
//            if (charModData.statModfified != StatsName.darkRes)
//                return;

//            float maxL = charController.GetStatChanceData(StatsName.darkRes).maxLimit;
//            if (charController.GetStat(StatsName.focus).currValue < maxL)  // Exit condition 
//            {
//                EndState();
//            }

            
//        }
//        public override void EndState()
//        {
//            foreach (CharController c in CharacterService.Instance.allyInPlayControllers)
//            {
//                if (c != charController)
//                {
//                    c.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -1,false);
//                }
//            }
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, 1,false);

//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 

//            charController.OnStatChanged -= Tick2;
//            Destroy(this);
//        }

//    }
//}