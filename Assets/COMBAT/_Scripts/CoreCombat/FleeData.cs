using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Combat
{
    // enemies and pets cannot flee only Heroes can flee 
    public class FleeData
    {
        // int combatID may be at a later stage 
        public CharController charController;
        public LandscapeNames landscape;
        public GameState gameState; 
    }

}

