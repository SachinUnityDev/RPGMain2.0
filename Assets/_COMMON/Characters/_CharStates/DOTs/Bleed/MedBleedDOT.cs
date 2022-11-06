//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat; 

// namespace Common
//{
//   // 3 rds	3 dmg per round


// public class MedBleedDOT : CharStatesBase
// {
//        int timeElapsed = 0;
//        int netDOT = 3;
//        CharController charController;
//        public override int castTime { get => netDOT; set => base.castTime = value; }

//        public override CharStateName charStateName => CharStateName.BleedMedDOT;
//        public override StateFor stateFor => StateFor.Mutual;
//        public override float dmgPerRound => 3.0f;

//        // Start is called before the first frame update
//    void Start()
//    {
//            charController = gameObject.GetComponent<CharController>();
//            charID = charController.charModel.charID; 
//             CombatEventService.Instance.OnEOR += TickState;
//    }

  
//    protected void TickState()
//    {
//        timeElapsed++;      

//        StatData statData = charController.GetStat(StatsName.armor);

//        if (statData.currValue > statData.minRange + 5)
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health
//                    , Mathf.RoundToInt(-dmgPerRound / 2));
//        else
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.health, -dmgPerRound);

//        if (timeElapsed >= netDOT)
//        {
//            EndState();
//        }
//    }

//    public override void EndState()
//    {
//            charController.charModel.InCharStatesList.Remove(charStateName);
//            CombatEventService.Instance.OnEOR -= TickState;
           
//            Destroy(this);
//    }

// }
//}
