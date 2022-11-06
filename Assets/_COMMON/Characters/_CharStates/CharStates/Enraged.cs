//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat; 
//namespace Common
//{

//    public class Enraged : CharStatesBase
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


//        //CharController charController;
//        //public GameObject causedByGO;
//        //public override int castTime { get; set; }
//        //public override float dmgPerRound => 1.0f;
//        //public override CharStateName charStateName => CharStateName.Enraged;
//        //public override StateFor stateFor => StateFor.Mutual;
//        //public int timeElapsed { get; set; }

//        //float dmgPercentInit;
//        //float hits; 
//        //private void Start()
//        //{
//        //    charController = GetComponent<CharController>();
//        //    charID = charController.charModel.charID;
//        //    if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//        //        Destroy(this);
//        //  //  OnCharStateStart(-1, TimeFrame.None);

//        //}

//        ////dmg increases by 8% for 2 rds each time being attacked by enemy.Stacks up. (get 5 hit in 2 rds, ur dmg increase +40%)
//        ////  +3 dodge	    -2 focus 
//        ////when burning state, increase self dmg +20%
//        ////lose 30 water res


//        //public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        //{


//        //    CombatEventService.Instance.OnEOC += Tick; // EOC

//        //    charController.OnStatChanged += Tick2;  // end condition T + S + Morale 0	ST + Morale increase
//        //    charController.OnStatChanged += DmgInc; dmgPercentInit = 0.08f; hits = 1;
//        //    //  +3 dodge	    -2 focus 
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.dodge, 3);
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -2);
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.waterRes, -30); 


//        //}

//        //void DmgInc(CharModData charModData)
//        //{
//        //    if (charModData.statModfified != StatsName.damage) return;

//        //     if (charController.charModel.InCharStatesList.Contains(CharStateName.BurnHighDOT)
//        //        || charController.charModel.InCharStatesList.Contains(CharStateName.BurnMedDOT)
//        //        || charController.charModel.InCharStatesList.Contains(CharStateName.BurnLowDOT))
//        //     {
//        //        charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.damage
//        //            , 0.2f * charModData.modifiedVal,false); 

//        //     } 

//        //    // nullify
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.damage
//        //        , -charModData.modifiedVal, false);
//        //    // apply inc value
//        //    float newVal = charModData.modifiedVal * dmgPercentInit * hits;
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.damage, newVal, false);
//        //    hits++; 

//        //}

//        //protected override void Tick()
//        //{


//        //}

//        //void Tick2(CharModData charModData)  //  change Stat subscribe 
//        //{
//        //    if (charModData.statModfified != StatsName.fireRes)
//        //        return;

//        //    float maxL = charController.GetStatChanceData(charModData.statModfified).maxLimit;
//        //    if (charController.GetStat(StatsName.focus).currValue < maxL)  // Exit condition 
//        //    {
//        //        EndState();

//        //    }

//        //}
//        //public override void EndState()
//        //{
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.dodge, -3, false);
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, 2,false);
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.waterRes, 30,false);

//        //    //charController.charModel.fortitudeOrg = 0;
//        //    //CombatEventService.Instance.OnEOR -= TickEOR;
//        //    //CombatEventService.Instance.OnEOC -= Tick; // EOC
//        //    charController.OnStatChanged -= Tick2;
//        //    charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 
//        //    Destroy(this);
//        //}

//    }
//}