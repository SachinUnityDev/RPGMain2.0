using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class Shocked : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Shocked;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
            //-3 Focus.... immune to Poison...	+24 earth res....+1-2 armor....
            //.Cannot use move skill Incli

            charController.charStateController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, StatsName.focus, -3, charStateModel.timeFrame, charStateModel.castTime, true);

            charController.charStateController.ApplyBuff(CauseType.CharState, (int)charStateName
                     , charID, StatsName.earthRes, +24, charStateModel.timeFrame, charStateModel.castTime, true);

            charController.charStateController.ApplyBuffOnRange(CauseType.CharState, (int)charStateName
                       , charID, StatsName.armor, +1, +2, charStateModel.timeFrame, charStateModel.castTime, true);
                

            CharStatesService.Instance.AddImmunity(charController.gameObject, CharStateName.PoisonedHighDOT);
            CharStatesService.Instance.AddImmunity(charController.gameObject, CharStateName.PoisonedLowDOT);

        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "<style=States> Shocked </style>";

            str1 = $"-3<style=Attributes> Focus </style>";

            str2 = "+1-2<style=Attributes> Armour </style>";

            str3 = $"+24<style=Earth> Earth Res </style>";

            str4 = "Immune to <style=Poison> Poisoned </style>";

            str5 = "Can not use<style=Move> Move </style>skills"; 
            //SkillServiceView.Instance.skillCardData.descLines.Add(str1);

            // str0 = "<style=Morale> "; 
            // add to charStateModel .. use this during skill Init
            // relieve charController from string duty ...
        }

        public override void EndState()
        {
            base.EndState();
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.PoisonedLowDOT);
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.PoisonedHighDOT);

        }
    }
}

//        CharController charController;
//        public GameObject causedByGO;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Shocked;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }

//        private void Start()
//        {
//            charController = GetComponent<CharController>();
//            charID = charController.charModel.charID; 
//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this);
//            StateInit(-1, TimeFrame.None, null);
//        }

//       // -4 init....	+(3-4) armor.... immune to poison.....	+24 earth res....	-2 focus

//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, -4);
//            charController.ChangeStatRange(CauseType.CharState, (int)charStateName, charID, StatsName.armor, 3, 4);

//            charController.charModel.Immune2CharStateList.Add(CharStateName.PoisonedHighDOT);
//            charController.charModel.Immune2CharStateList.Add(CharStateName.PoisonedMedDOT);
//            charController.charModel.Immune2CharStateList.Add(CharStateName.PoisonedLowDOT);

//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.earthRes, 24);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -2);           

//        }

//        protected override void Tick()
//        {



//        }

//        public override void EndState()
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, -4);
//            charController.ChangeStatRange(CauseType.CharState, (int)charStateName, charID, StatsName.armor,  3, 4);

//            charController.charModel.Immune2CharStateList.Remove(CharStateName.PoisonedHighDOT);
//            charController.charModel.Immune2CharStateList.Remove(CharStateName.PoisonedMedDOT);
//            charController.charModel.Immune2CharStateList.Remove(CharStateName.PoisonedLowDOT);

//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.earthRes, 24);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -2);

//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 
//            Destroy(this);
//        }

