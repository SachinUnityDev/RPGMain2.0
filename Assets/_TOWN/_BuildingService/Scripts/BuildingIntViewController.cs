using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class BuildingIntViewController : MonoBehaviour
    {
        [Header("Building CLICKS")]

        [Header("TEMPLE")]
        [SerializeField] Button templeDayBtn;
        [SerializeField] Button templeNightBtn;


        [Header("MARKET PLACE")]
        [SerializeField] Button marketDayBtn;
        [SerializeField] Button marketNightBtn;


        [Header("Building Panels")]
        [SerializeField] GameObject BuildingIntParent; 

        public GameObject marketPlacePanel;// public for outside interactions
        public GameObject templePanel; 
        // use Ipanel to open and close 

        void Start()
        {
            templeDayBtn.onClick.AddListener(OnTempleBtnPressed);
            templeNightBtn.onClick.AddListener(OnTempleBtnPressed);

            marketDayBtn.onClick.AddListener(OnMarketPlaceBtnPressed);
            marketNightBtn.onClick.AddListener(OnMarketPlaceBtnPressed);
        }

        void OnTempleBtnPressed()
        {
            // open temple panel 

        }

        void OnMarketPlaceBtnPressed()
        {

        }


    }




}
