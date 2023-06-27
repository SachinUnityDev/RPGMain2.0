using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class WorldView : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] Button descTxtBtn;
        [SerializeField] Button showTwnsBtn;

        [Header("Panels")]
        [SerializeField] Transform descTxtTrans;
        [SerializeField] Transform townsTrans;
        [SerializeField] bool isDescTxtShown= false;
        [SerializeField] bool isTownTxtShown = false;


        [Header("Global Var")]
        MapView mapView;
        private void Start()
        {
            descTxtBtn.onClick.AddListener(OnDescTxtBtnPressed);
            showTwnsBtn.onClick.AddListener(OnShowTownBtnPressed); 
        }
        public void InitTown(MapView mapView)
        {            
            this.mapView = mapView;
            foreach (Transform child in transform)
            {
                WorldMapBtnPtrEvents mapBtnPtrEvents = child.GetComponent<WorldMapBtnPtrEvents>();
                if (mapBtnPtrEvents != null)
                {
                    mapBtnPtrEvents.InitMapBtnEvents(mapView);
                }
            }
        }



        void OnDescTxtBtnPressed()
        {
            if(isDescTxtShown)
                descTxtTrans.gameObject.SetActive(true);
            else
                descTxtTrans.gameObject.SetActive(false);
        }
        void OnShowTownBtnPressed()
        {
            if (isTownTxtShown)
                townsTrans.gameObject.SetActive(true);
            else
                townsTrans.gameObject.SetActive(false);
        }



    }
}