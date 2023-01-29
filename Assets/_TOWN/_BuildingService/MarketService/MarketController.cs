using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Town
{
    public class MarketController : MonoBehaviour, IBuilding
    {
        public BuildingNames buildingName => BuildingNames.Marketplace;

        public GameObject marketPanelday;
        public GameObject marketPanelNight;
        void Start()
        {
        }
        public void Init()
        {
        }

        public void Load()
        {
            // load the market panel .. 
            // predefined shops...
            // 
        }
        public void UnLoad()
        {
        }

    }



}

