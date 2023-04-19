using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class ShipController : MonoBehaviour
    {
        public ShipModel shipModel;
        [Header("to be ref")]
        public ShipView shipView;


        private void Start()
        {
            BuildingSO shipSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Ship);
            shipModel = new ShipModel(shipSO);
        }

        public void OnBuildUnLock()
        {
            shipModel.buildState = BuildingState.Available;
            shipModel.unLockedOnDay = CalendarService.Instance.dayInGame;
        }

        public void UpdateBuildState()
        {
            if (shipModel.buildState == BuildingState.Locked) return;
            // weekly name to be done .....



         
            shipModel.buildState = BuildingState.Available;
        }

    }
}