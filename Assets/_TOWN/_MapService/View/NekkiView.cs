using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public interface ILocation
    {
        public LocationName locationName { get; } 
    }

    public class NekkiView : MonoBehaviour, ILocation
    {
        [Header("Buttons")]
        [SerializeField] Button descTxtBtn;

        [Header("Panels")]
        [SerializeField] Transform descTxtTrans;
        [SerializeField] bool isDescTxtShown = false;

        [Header("Global Var")]        
        MapView mapView;
        public LocationName locationName => LocationName.Nekkisari; 

        private void Start()
        {
            descTxtBtn.onClick.AddListener(OnDescTxtBtnPressed); 
        }
        public void InitTown(MapView mapView)
        {            
            this.mapView = mapView;
        }

        void OnDescTxtBtnPressed()
        {
            if (isDescTxtShown)
                descTxtTrans.gameObject.SetActive(false);
            else
                descTxtTrans.gameObject.SetActive(true);
        }
    }
}