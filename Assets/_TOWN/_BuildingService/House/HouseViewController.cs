using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;


namespace Town
{
    public class HouseViewController : MonoBehaviour, IBuilding
    {
        public BuildingNames buildingName => BuildingNames.House;

        public Button arrowBtn;
        [SerializeField] Button buy;
        [SerializeField] Button stash;
        [SerializeField] Button music;


        [SerializeField] GameObject buyPanel;
        [SerializeField] Button exit; 

        public GameObject optionsPanel;

        void Start()
        {
            arrowBtn.onClick.AddListener(OnArrowBtnPressed);
            buy.onClick.AddListener(OnBuybtnPressed);
            exit.onClick.AddListener(OnExitPressed);
            optionsPanel.transform.DOScale(0, 0.2f);
            buyPanel.transform.DOScale(0, 0.2f);
        }
        public void Init()
        {
           
        }

        public void Load()
        {

          

        }

        public void UnLoad()
        {

        }

        void OnArrowBtnPressed()
        {

            //arrowBtn.transform.DOMoveX(10, 0.15f);

            //if (optionsPanel.GetComponent<RectTransform>().localScale == Vector3.one)
            //    optionsPanel.transform.DOScale(0, 0.2f);
            //else
            //    optionsPanel.transform.DOScale(1, 0.2f);

        }
        void OnExitPressed()
        {
            buyPanel.transform.DOScale(0, 0.1f);
        }
        void OnBuybtnPressed()
        {
            buyPanel.transform.DOScale(1, 0.2f);
        }


        // [SerializeField] GameObject namePlank;

     
    }



}

