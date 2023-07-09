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
    //public class BuildView
    //{

    //}



    public class HouseView : MonoBehaviour, IPanel, IBuildName, iHelp
    {
        [SerializeField] HelpName helpName;
        public BuildingNames BuildingName => BuildingNames.House;

        [Header("To be ref")]
        [SerializeField] Transform btnContainer;
        [SerializeField] Transform NPCInteractPanel;

        [Header("Not to be ref")]
        [SerializeField] Transform BGSpriteContainer;

        [Header("House Interact Panels: To be ref")]
        [SerializeField] Transform BuildInteractPanel; 

        public Transform buyPanel;
        public Transform provisionPanel;
        public Transform healingPanel;
        public Transform restPanel;
        public Transform stashPanel;
        public Transform brewingPanel;

        [Header("Day and Night BG Sprites")]
        [SerializeField] Sprite dayBG;
        [SerializeField] Sprite nightBG; 

        [SerializeField] Button exit; 

        BuildingSO houseSO;
        HouseModel houseModel; 
        TimeState timeState;
        void Awake()
        {
            BGSpriteContainer = transform.GetChild(0);
            exit.onClick.AddListener(UnLoad); 
        }
        public void Init()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
            houseSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.House);
            timeState = CalendarService.Instance.currtimeState;
            houseModel = BuildingIntService.Instance.houseController.houseModel;
            btnContainer.GetComponent<HouseInteractBtnView>().InitInteractBtns(this, houseModel); 
            FillHouseBG();
            InitInteractPanels();

        }
        public void FillHouseBG()
        {
            if(CalendarService.Instance.currtimeState == TimeState.Night)
            {
                BGSpriteContainer.GetComponent<Image>().sprite = nightBG; 
            }
            else
            {
                BGSpriteContainer.GetComponent<Image>().sprite = dayBG;
            }

            for (int i = 0; i < 4; i++)
            {
                BGSpriteContainer.GetChild(i).GetComponent<HouseBaseEvents>().Init(this);  
            }
        }
        void InitInteractPanels()
        {
            foreach (Transform child in BuildInteractPanel)
            {
                child.GetComponent<IPanel>().Init(); // interact panels initialized here 
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
                case BuildInteractType.CureSickness:
                    return healingPanel;
                default:
                    return null;                    
            }
        }
        public void Load()
        {            
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(this.gameObject, false);
            foreach (Transform child in BuildInteractPanel)
            {
                child.GetComponent<IPanel>().UnLoad();  
            }
            TownService.Instance.townViewController.OnBuildDeselect();
        }

        public HelpName GetHelpName()
        {
            return helpName;
        }
    }
}

