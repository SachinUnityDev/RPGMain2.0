//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//namespace Common
//{

//    public class Blinded : CharStatesBase
//    {
//        //CharController charController; 
//        //public override int castTime { get; set; }
//        //public override float dmgPerRound => 1.0f;
//        //public override CharStateName charStateName => CharStateName.Blinded;
//        //public override StateFor stateFor => StateFor.Mutual;
//        //public int timeElapsed { get; set; }

//        //float dodgeOnStart;
//        //float accOnStart;

//        //// Start is called before the first frame update
//        //void Start()
//        //{
//        //    charController = gameObject.GetComponent<CharController>();
//        //    charID = charController.charModel.charID;
//        //    if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//        //        Destroy(this);

//        //    StateInit(-1, TimeFrame.None); 

//        //}
//        ////dodge, acc =0 and focus becomes -3	immune to horrified	+12% dark res	damage doubles
//        //public override void StateInit(int _castTime, TimeFrame timeFrame, GameObject CharGO =null)
//        //{
//        //    castTime = _castTime;
//        //    charController.OnStatChanged += Tick;
//        //    dodgeOnStart = charController.GetStat(StatsName.dodge).currValue; 
//        //    accOnStart = charController.GetStat(StatsName.acc).currValue;
           
//        //    charController.SetStat(StatsName.dodge, 0);
//        //    charController.SetStat(StatsName.acc, 0);
           
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -3);

//        //    charController.charModel.Immune2CharStateList.Add(CharStateName.Horrified);
            
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.lightRes, 12);

//        //}
//        ////dodge, acc and focus becomes -3	immune to horrified	+12% light res	damage doubles

//        //void Tick(CharModData charModData)
//        //{
            
//        //    // striker perspective .. changes the base dmg value 
//        //    // if you are blinded .... if you are warrior .... your dmg value 8-10 will be double to 16-20 

//        //    //  DOUBLES DAMAGE
//        //    //if (_statsName == StatsName.health)
//        //      // charController.ChangeStat(StatsName.health, _value, 0, 0, false);  // no self cycle

//        //}

//        //public override void EndState()   // ST+S
//        //{
//        //    // increase by same amt
           
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.dodge, dodgeOnStart,false);
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.acc, accOnStart, false);

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, 3);

//        //    charController.charModel.Immune2CharStateList.Remove(CharStateName.Horrified);

//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID,  StatsName.lightRes, -12);
            
//        //    charController.charModel.InCharStatesList.Remove(charStateName);
//        //    charController.OnStatChanged -= Tick;

//        //    Destroy(this); 

//        //}



//    }
//}