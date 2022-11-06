using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{


    public class Rooted : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Rooted;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {  // ...Immune to Lightfooted... cannot use Move type of skills 
           // ....-5 dodge ... melee attackes only if you are in pos 1 to pos 1   

            CharStatesService.Instance.AddImmunity(charController.gameObject, CharStateName.Lightfooted);

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
            str0 = "<style=States> Rooted </style>";

            str1 = "-5<style=Fortitude> Fortitude </style> per rd";

            str2 = "-1<style=Fortitude> Fortitude Org </style>until EOQ";

            str3 = "Immune to<style=States> Inspired </style>";
        }


        public override void EndState()
        {
            base.EndState();
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.Lightfooted);
        }
    }
}


//        CharController charController;
//        public GameObject causedByGO;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Rooted;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }
//        float currentDodge; 
//        private void Start()
//        {
//            charController = GetComponent<CharController>();
//            charID = charController.charModel.charID;
//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this);
//            StateInit(-1, TimeFrame.None, null);
//        }      


//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
//            currentDodge = charController.GetStat(StatsName.dodge).currValue;
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.dodge, 1);
//            charController.SetStat(StatsName.dodge, 0, true);


//            charController.OnStatChanged += Tick2;

//        }

//        protected override void Tick()
//        {



//        }


//       // T + S + Init 0	ST + init increase

//        void Tick2(CharModData charModData)  //  change Stat subscribe 
//        {
//            if (charModData.statModfified != StatsName.haste)
//                return;

//            float minL = charController.GetStatChanceData(StatsName.haste).minLimit;
//            if (charController.GetStat(StatsName.focus).currValue > minL)  // Exit condition 
//            {
//                EndState();
//            }

//        }
//        public override void EndState()
//        {         

//            charController.SetStat(StatsName.dodge, currentDodge, false);

//            charController.OnStatChanged -= Tick2;

//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 
//            Destroy(this);
//        }