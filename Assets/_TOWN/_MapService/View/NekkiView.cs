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
        [SerializeField] bool isDescTxtShown = true;

        [Header("Global Var")]        
        MapView mapView;
        public LocationName locationName => LocationName.Nekkisari; 

        private void Start()
        {
           isDescTxtShown = true;
            descTxtTrans.gameObject.SetActive(true);
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
            isDescTxtShown = !isDescTxtShown;
        }
    }
}