//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat; 

//namespace Common
//{
//    // -1 focus	+3 init
//    //melee attacker is shocked for 1 rd 
//    //melee attacker receives receives 1-7 air dmg   
//    //lose 30 earth res 
//    //immune to shock

//    public class Charged :CharStatesBase
//    {
//        //CharController charController;
//        //public GameObject causedByGO; 
//        //public override int castTime { get; set; }
//        //public override float dmgPerRound => 1.0f;
//        //public override CharStateName charStateName => CharStateName.Charged;
//        //public override StateFor stateFor => StateFor.Mutual;
//        //public int timeElapsed { get; set; }

//        //private void Start()
//        //{
//        //    charController = GetComponent<CharController>();
//        //    charID = charController.charModel.charID;
//        //    if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//        //        Destroy(this);
//        //    StateInit(-1, TimeFrame.None, null); 
//        //}

//        //public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        //{
//        //    // -1 focus	+3 init

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID,StatsName.focus, -1);

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, +3);

//        //    //lose 30 earth res   immune to shock

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.earthRes, -30);

//        //    charController.charModel.Immune2CharStateList.Add(CharStateName.Shocked);
//        //    CombatEventService.Instance.OnEOR += Tick; 

//        //}

//        //protected override void Tick()
//        //{
           
//        //    //melee attacker is shocked for 1 rd 


//        //}
//        //// S + Air res is max
//        ////ST + Air res drops

//        //void Tick2()
//        //{
//        //    float maxL = charController.GetStatChanceData(StatsName.airRes).maxLimit;

//        //    if (charController.GetStat(StatsName.earthRes).currValue < maxL)
//        //    {

//        //        EndState();

//        //    }
//        //}
//        //public override void EndState()
//        //{
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, 1);

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, -3);

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.earthRes, 30);

//        //    charController.charModel.Immune2CharStateList.Remove(CharStateName.Shocked);
//        //    charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 
//        //    Destroy(this); 
//        //}


//    }
//}