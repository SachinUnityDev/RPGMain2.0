using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class FeebleMinded : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Feebleminded;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {  // ...Immune to LuckyDuck... cannot use Patience type of skills 
           // ....curio related stuff to be done later  

            CharStatesService.Instance.AddImmunity(charController.gameObject, CharStateName.LuckyDuck);

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
            str0 = "<style=States> FeebleMinded </style>";

            str1 = "-5<style=Fortitude> Fortitude </style> per rd";

            str2 = "-1<style=Fortitude> Fortitude Org </style>until EOQ";

            str3 = "Immune to<style=States> Inspired </style>";
        }


        public override void EndState()
        {
            base.EndState();
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.LuckyDuck);
        }
    }
}


//        public CharController charController;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Feebleminded;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }

//        void Start()
//        {
//            charController = gameObject.GetComponent<CharController>();

//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this);

//            StateInit(-1, TimeFrame.None, null); 

//        }

//        public override void StateInit(int _castTime, TimeFrame timeFrame, GameObject charGO = null)
//        {

//            charController.OnStatChanged += Tick2; 

//        }
//       // T + S + Luck 0	ST + Luck increase

//        void Tick2(CharModData charModData)
//        {
//            if (charModData.statModfified != StatsName.luck) return; 
//            if (charController.GetStat(charModData.statModfified).currValue > 0)
//            {
//                EndState(); 
//            }

//        }

//        public override void EndState()
//        {            
//            //charController.OnStatChanged -= Tick2;

//            Destroy(this, 1f);   // remains for only one EOC ; 
//        }