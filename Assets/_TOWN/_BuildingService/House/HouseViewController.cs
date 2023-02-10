using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using Common;
using Interactables;

namespace Town
{
 
    public class HouseViewController : MonoBehaviour, IBuilding
    {
        public BuildingNames buildingName => BuildingNames.House;

        [Header("To be ref")]
        [SerializeField] Transform btnContainer;

        [Header("Not to be ref")]
        [SerializeField] Transform BGSpriteContainer;

        [Header("House Interact Panels: To be ref")]
        [SerializeField] Transform InteractPanelContainer; 

        public Transform buyPanel;
        public Transform provisionPanel;
        public Transform healingPanel;
        public Transform restPanel;
        public Transform stashPanel;
        public Transform brewingPanel;


        [SerializeField] Button exit; 

        BuildingSO houseSO;
        TimeState timeState; 

        void Start()
        {
            BGSpriteContainer = transform.GetChild(0);
            Init(); // for test
        }
        public void Init()
        {            
            houseSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.House);
            timeState = CalendarService.Instance.currtimeState;
            btnContainer.GetComponent<HouseInteractBtnView>().InitInteractBtns(this); 
            FillHouseBG();
            InitInteractPanels();
        }
        public void FillHouseBG()
        {
            for (int i = 0; i < 4; i++)
            {
                BGSpriteContainer.GetChild(i).GetComponent<HouseBaseEvents>().Init(this);  
            }
        }
        void InitInteractPanels()
        {
            foreach (Transform child in InteractPanelContainer)
            {
                child.GetComponent<IPanel>().Init(); 
            }
        }

        public Transform GetInteractPanel(BuildInteractType buildInteract)
        {
            switch (buildInteract)
            {
                case BuildInteractType.None:
                    return null;                    
                case BuildInteractType.Purchase:
                    return buyPanel;
                case BuildInteractType.Chest:
                    return stashPanel; 
                case BuildInteractType.Fermentation:
                    return brewingPanel; 
                case BuildInteractType.Music:
                    return null;                    
                case BuildInteractType.EndDay:
                    return restPanel; 
                case BuildInteractType.Provision:
                    return provisionPanel; 
                default:
                    return null;
                    
            }


        }
        public void Load()
        {
        }

        public void UnLoad()
        {
        }
    }
}

