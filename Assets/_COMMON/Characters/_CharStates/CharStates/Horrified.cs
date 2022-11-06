//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat; 

// namespace Common
// {
//    public class Horrified : CharStatesBase
//    {
//        CharController charController;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Horrified;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }

//        void Start()
//        {
//            charController = gameObject.GetComponent<CharController>();
//            charID = charController.charModel.charID;
//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this);

//            StateInit(-1, TimeFrame.None, null);

//        }
//        //-3 morale, luck and init...  immune to blinded	...+12% Dark res

//        // -8 fortitude per round.... 

//        public override void StateInit(int _castTime, TimeFrame timeFrame, GameObject charGO = null)
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, -3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, -3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, -3);
//            charController.charModel.Immune2CharStateList.Add(CharStateName.Blinded);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.darkRes, 12);

//            CombatEventService.Instance.OnEOR += fortitudeDecr; 
//        }

//        void fortitudeDecr()
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.fortitude, -8); 

//        }
      
//        public override void EndState()
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, 3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, 3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, 3);
//            charController.charModel.Immune2CharStateList.Remove(CharStateName.Blinded);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.darkRes, -12);

//            charController.charModel.InCharStatesList.Remove(charStateName);

//            Destroy(this);   // remains for only one EOC ; 
//        }




//    }
// } 

