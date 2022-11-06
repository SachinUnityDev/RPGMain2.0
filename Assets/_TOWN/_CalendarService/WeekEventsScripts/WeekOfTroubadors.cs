using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class WeekOfTroubadors :  WeekEventBase
{

        public override WeekName weekName => WeekName.WeekOfTroubadors; 

        public string weekNameStr = "Week Of Troubadors";
        public string weekDesc = "Welcome Desciption";

       

        public override void OnWeekEnter(CharController charController)
        {
            // update UI 
            OnWeekTick(charController);
        }

        public override void OnWeekTick(CharController characterController)
        {
            FameService.Instance.fameModifier = 2;
        }

        public override void OnWeekExit(CharController charController)
        {
            FameService.Instance.fameModifier = 1;

        }
    }

}

