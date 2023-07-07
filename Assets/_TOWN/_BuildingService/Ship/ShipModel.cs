using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    [Serializable]
    public class ShipModel : BuildingModel
    {
        public int unLockedOnDay = 0;
        public int lastAvailDay = 0;
        public int lastUnAvailDay = 0; 
       
        public ShipModel(BuildingSO shipSO)
        {
            buildingName = shipSO.buildingName;
            buildState = shipSO.buildingState;

            buildIntTypes = shipSO.buildIntTypes.DeepClone();
            npcInteractData = shipSO.npcInteractData.DeepClone();
            charInteractData = shipSO.charInteractData.DeepClone();

        }
    }
}