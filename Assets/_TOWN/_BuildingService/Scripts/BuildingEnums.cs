using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{

    public class BuildingEnums
    {

        public enum BuildingStatus
        { 
            None, 
            Locked, // locked in the current game
            UnLocked, // Buildings UnLock as Quest progress
            Available, // Can be Opened 
            UnAvailable, // UnLocked but due to current game condition
                         // as in certain days in a week cannot access the building         
        }


        
    }
}