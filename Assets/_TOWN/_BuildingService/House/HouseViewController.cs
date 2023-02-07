using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using Common;

namespace Town
{
    public class HouseViewController : MonoBehaviour, IBuilding
    {
        public BuildingNames buildingName => BuildingNames.House;

        [SerializeField] Transform btnContainer;
             
        [SerializeField] Button intBtn1;
        [SerializeField] Button intBtn2;
        [SerializeField] Button intBtn3;
        [SerializeField] Button intBtn4;
        [SerializeField] Button intBtn5;
        [SerializeField] Button intBtn6;
        [SerializeField] Button intBtn7;

        [SerializeField] GameObject buyPanel;
        [SerializeField] Button exit; 

        public GameObject optionsPanel;

        BuildingSO houseSO;
        TimeState timeState; 

        void Start()
        {
            
        }
        public void Init()
        {
            // get the interactions unlocked
            houseSO = BuildingService.Instance.allBuildSO.GetBuildSO(BuildingNames.House);
            timeState = CalendarService.Instance.currtimeState; 
            // get house interaction unloacked from controller

        }

        public void Load()
        {

          

        }

        public void UnLoad()
        {

        }

   

     
    }



}

