using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class WoodStocker0 : CityEncounterBase
    {
        public override CityENames encounterName => CityENames.Woodstocker;

        public override int seq => 0; 

        public override void OnChoiceASelect()
        {
            resultStr = "Wait, wait,he quickly changed his tone to a softer one. Young sir, i would like to buy some firewood. You should listen to my offer... As you walked away the man insisted you no more and went on his way.";
            strFX = "";
        }

        public override void OnChoiceBSelect()
        {
            resultStr = "Sure good sir,you answered with a fake grin on your face. Tell me what do you need? I need some firewood, he replied.Hmm, you rubbed your hands together, a trader's gesture you learned from Amish.For the right price, why not?";
            strFX = "";
        }

        public override bool PreReqChk()
        {
            return false;

        }

        public override bool UnLockCondChk()
        {
            return false;
        }
    }
}