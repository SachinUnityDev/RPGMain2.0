using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{

    public class LandModel
    {
        public LandscapeNames landscapeName;

        [TextArea(5, 10)]
        public string hazardStr = "";
     

        [Header("Hunger and Thirst")]
        public int hungerMod = 0;     
        public int thirstMod = 0;
        public LandModel(LandscapeSO landSO) 
        { 
            landscapeName = landSO.landscapeName; 
            hungerMod= landSO.hungerMod;
            thirstMod= landSO.thirstMod;
          
        } 
    }
}