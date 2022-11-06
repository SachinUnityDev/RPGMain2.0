//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat; 

//namespace Common
//{

//    public class Calloused: CharStatesBase 
//    {
//        //CharController charController;
//        //public override int castTime { get; set; } 
//        //public override float dmgPerRound => 1.0f;
//        //public override CharStateName charStateName => CharStateName.Calloused;
//        //public override StateFor stateFor => StateFor.Mutual;
//        //public int timeElapsed { get; set; }

//        //float currentArmour; 
//        //void Start()
//        //{
//        //    charController = gameObject.GetComponent<CharController>();
//        //    charID = charController.charModel.charID;
//        //    if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//        //        Destroy(this);
            
//        //    StateInit(1, TimeFrame.EndOfDay);           

//        //}
//        ////armor is double	-2 init	+3 luck 
//        ////when drinks Elixir of Hardening gains +1 hp regen for 1 day. (Stackable)  *****
//        ////lose 30 air res 
//        ////immune to poison

//        ////S + Earth res is max	ST + Earth res drops
//        //public override void StateInit(int _castTime, TimeFrame timeFrame, GameObject charGO = null)
//        //{
//        //    castTime = _castTime;            
//        //   // QuestEventService.Instance.OnDayChange += Tick;
//        //    CombatEventService.Instance.OnEOR += Tick2; 
//        //    currentArmour = charController.GetStat(StatsName.armor).currValue;

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.armor
//        //                        , +currentArmour,false);  // DOUBLE ARMOR SHOULD NOT LOOP 

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, -2);

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, 3);

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.airRes, -30);

//        //    charController.charModel.Immune2CharStateList.Add(CharStateName.PoisonedLowDOT);

//        //    charController.charModel.Immune2CharStateList.Add(CharStateName.PoisonedMedDOT);

//        //    charController.charModel.Immune2CharStateList.Add(CharStateName.PoisonedHighDOT);
//        //}

//        //protected override void Tick()
//        //{


//        //    // when drinks Elixir of Hardening gains + 1 hp regen for 1 day. (Stackable) * ****

//        //}

//        //void Tick2()
//        //{
//        //     float maxL = charController.GetStatChanceData(StatsName.earthRes).maxLimit;

//        //    if (charController.GetStat(StatsName.earthRes).currValue < maxL)
//        //    {
//        //        EndState(); 

//        //    }
//        //}
//        //public override void EndState() 
//        //{ 
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.armor
//        //                    , -currentArmour, false);

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, +2);

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, -3);

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.airRes, 30);

//        //    charController.charModel.Immune2CharStateList.Remove(CharStateName.PoisonedLowDOT);

//        //    charController.charModel.Immune2CharStateList.Remove(CharStateName.PoisonedMedDOT);

//        //    charController.charModel.Immune2CharStateList.Remove(CharStateName.PoisonedHighDOT);

//        //    charController.charModel.InCharStatesList.Add(CharStateName.Calloused);  // added by charStateService 


//        //    charController.charModel.InCharStatesList.Remove(charStateName);
//        //    QuestEventService.Instance.OnDayChange -= Tick;
//        //    CombatEventService.Instance.OnEOR -= Tick2;
//        //    Destroy(this);   // remains for only one EOC ; 
//        //}


//    }
//}