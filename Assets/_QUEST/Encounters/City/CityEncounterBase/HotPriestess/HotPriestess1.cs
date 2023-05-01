using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class HotPriestess1 : CityEncounterBase
    {
        public override CityENames encounterName => CityENames.HotPriestess; 

        public override int seq => 1;

        public override void OnChoiceASelect()
        {
            resultStr = "She has gone with the flow but when you stopped she had a suspicious grin on her face.I have given you some of my fire, said softly. But i have sealed your lips with it. You are now warded off against water. She explains that from now on, Day of Water blessing will not work on you. Though, it is a decent pay off of gaining permanent Fire Res buff.";
            strFX = "Abbas gained +6 Fire Res permanently";
        }

        public override void OnChoiceBSelect()
        {
            resultStr = "Here, as i promised, she whispers. A Nusku priestess always stays true to her word. She hands in some items and adds: However, my dear friend Minami wasn't happy about our last meeting. Now that i rewarded you these items, I think she will not clear your mind for a while. A jealous little woman she is! I hope that's ok for you. You uncheerfully smile: Well, what choice do i have?";
            strFX = "Loot gained";
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