//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//namespace Common
//{
//   // frigid S + Water res is max ST + Water res drops
//   // +3 acc	-2 init	+2 focus 
//    // melee attacker's init is decreased by 3 for 2 rds. stacks up.	lose 30 fire res

//    public class Frigid : CharStatesBase
//    {
//        CharController charController;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Frigid;
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
//        // +3 acc...	-2 init  lose.... ...res +2 focus...30 fire 
//        public override void StateInit(int _castTime, TimeFrame timeFrame, GameObject charGO = null)
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.acc, 3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, -2);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, 2);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.fireRes, -30); 


//            charController.OnStatChanged += Tick2;



//        }

//       void MeleeAttacker(GameObject _causedByGO) { 


//            // +2 focus melee attacker's init is decreased by 3 for 2 rds. stacks up

//        }
//        void Tick2(CharModData charModData)  // end condition 
//        {
//            if (charModData.statModfified != StatsName.waterRes) return;

//            float maxL = charController.GetStatChanceData(StatsName.waterRes).maxLimit; 
//            if (charController.GetStat(charModData.statModfified).currValue < maxL)
//            {
//                EndState();
//            }

//        }

//        public override void EndState()
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.acc, -3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, 2);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -2);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.fireRes, 30);

//            charController.charModel.InCharStatesList.Remove(charStateName);

//            charController.OnStatChanged -= Tick2;

//            Destroy(this);   // remains for only one EOC ; 
//        }

//    }
//}