using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Soaked : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Soaked;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
        
            //-2 morale.... immune to burning...	+24 fire res....-40 air res
            charController.charStateController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, StatsName.morale, -2, charStateModel.timeFrame, charStateModel.castTime, true);

            charController.charStateController.ApplyBuff(CauseType.CharState, (int)charStateName
                     , charID, StatsName.fireRes, +24, charStateModel.timeFrame, charStateModel.castTime, true);

            charController.charStateController.ApplyBuff(CauseType.CharState, (int)charStateName
                     , charID, StatsName.airRes, -40, charStateModel.timeFrame, charStateModel.castTime, true);


            CharStatesService.Instance.AddImmunity(charController.gameObject, CharStateName.BurnLowDOT);
            CharStatesService.Instance.AddImmunity(charController.gameObject, CharStateName.BurnHighDOT);

        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "<style=States> Soaked </style>";

            str1 = $"-2<style=Attributes> Morale </style>";

            str2 = $"+24<style=Fire> Fire Res </style>, -40<style=Air> Air Res </style>";

            str3 = "Immune to <style=Burn> Burning </style>";
            //SkillServiceView.Instance.skillCardData.descLines.Add(str1);

            // str0 = "<style=Morale> "; 
            // add to charStateModel .. use this during skill Init
            // relieve charController from string duty ...
        }

        public override void EndState()
        {
            base.EndState();
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.BurnHighDOT);
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.BurnLowDOT);

        }
    }
}

//        CharController charController;

//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Soaked;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }
//        float currentAirRes; 
//        private void Start()
//        {
//            charController = GetComponent<CharController>();
//            charID = charController.charModel.charID; 
//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this,1f);
//            castTime = 2; 
//            StateInit(-1, TimeFrame.None, null);
//        }
//          //-2 morale.... immune to burning...	+24 fire res....-40 air res
//  

//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, -3);

//            charController.charModel.Immune2CharStateList.Add(CharStateName.BurnHighDOT);
//            charController.charModel.Immune2CharStateList.Add(CharStateName.BurnLowDOT);
//            charController.charModel.Immune2CharStateList.Add(CharStateName.BurnMedDOT);


//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.fireRes, 24);
//            currentAirRes = charController.GetStat(StatsName.airRes).currValue; 
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.airRes, -60); 
//        }

//        protected override void Tick()
//        {



//        }

//        public override void EndState()
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -2);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, 3);

//            charController.charModel.Immune2CharStateList.Remove(CharStateName.BurnHighDOT);
//            charController.charModel.Immune2CharStateList.Remove(CharStateName.BurnLowDOT);
//            charController.charModel.Immune2CharStateList.Remove(CharStateName.BurnMedDOT);


//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.fireRes, -24);
//            charController.SetStat(StatsName.airRes, currentAirRes);          

//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 
//            Destroy(this);
//        }

