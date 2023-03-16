using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class MarketController : MonoBehaviour
    {
        public MarketModel marketModel;

        BuildingSO marketSO;
        [Header("to be ref")]
        public MarketView marketView;
        void Start()
        {
            marketSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Marketplace);
            marketModel = new MarketModel(marketSO);
        }
    }
}