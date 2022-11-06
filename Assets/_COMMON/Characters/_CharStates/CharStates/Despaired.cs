using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    public class Despaired : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Despaired;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {  // .... -5 fortitude regen ...Immune to Inspired
            // -1 fort Org 
           
            //charController.charStateController.ApplyBuff(CauseType.CharState, (int)charStateName
            //           , charID, StatsName.luck, -1, charStateModel.timeFrame, charStateModel.castTime, true);

            CharStatesService.Instance.AddImmunity(charController.gameObject, CharStateName.Inspired);
  
        }

        void EOQTick()
        {
            // let char State Have this 
        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "<style=States> Despaired </style>";
            charStateModel.charStateCardStrs.Add(str0);

            str1 = "-5<style=Fortitude> Fortitude </style> per rd";
            charStateModel.charStateCardStrs.Add(str1);

            str2 = "-20<style=Dark> Dark Res </style>";
            charStateModel.charStateCardStrs.Add(str2);

            str3 = "30% chance to force use Patience";
            charStateModel.charStateCardStrs.Add(str3);

            str4 = "Immune to<style=States> Inspired </style>";
            charStateModel.charStateCardStrs.Add(str4);
        }

 
        public override void EndState()
        {
            base.EndState();
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.Inspired);
        }
    }
}

//        //CharController charController;
//        //public GameObject causedByGO;
//        //public override int castTime { get; set; }
//        //public override float dmgPerRound => 1.0f;
//        //public override CharStateName charStateName => CharStateName.Despaired;
//        //public override StateFor stateFor => StateFor.Mutual;
//        //public int timeElapsed { get; set; }
//        //float currentFortitude =0; 
//        //private void Start()
//        //{
//        //    charController = GetComponent<CharController>();
//        //    charID = charController.charModel.charID; 
//        //    if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//        //        Destroy(this);
//        //    StateInit(-1, TimeFrame.None);

//        //}

//        //public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        //{
//        //    currentFortitude = charController.GetStat(StatsName.fortitude).currValue; 
//        //    CombatEventService.Instance.OnEOR += TickEOR; 
//        //    CombatEventService.Instance.OnEOC += Tick; // EOC
//        //    charController.OnStatChanged += Tick2;  // end condition T + S + Morale 0	ST + Morale increase

//        //}
//        ////-3 fortitude per rd	-1 fort origin until eoq.Stackable.


//        //protected override void Tick()
//        //{
//        //    charController.charModel.fortitudeOrg--; 

//        //}
//        //void TickEOR()
//        //{
//        //    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.fortitude, -3); 
//        //}

//        //void Tick2(CharModData charModData)  //  change Stat subscribe 
//        //{
//        //    if (charModData.statModfified != StatsName.morale)
//        //        return;

//        //    float minL = charController.GetStatChanceData(StatsName.morale).minLimit; 
//        //    if (charController.GetStat(StatsName.focus).currValue > minL)  // Exit condition 
//        //    {
//        //        EndState();

//        //    }

//        //}
//        //public override void EndState()
//        //{
//        //    if (charController!= null)
//        //    {
//        //        charController.SetStat(StatsName.fortitude, currentFortitude);
//        //        charController.charModel.fortitudeOrg = 0;
//        //        CombatEventService.Instance.OnEOR -= TickEOR;
//        //        CombatEventService.Instance.OnEOC -= Tick; // EOC
//        //        charController.OnStatChanged -= Tick2;
//        //        charController.charModel.InCharStatesList.Remove(charStateName); // added by charStateService 
//        //        Destroy(this);


//        //    }

//        //}


