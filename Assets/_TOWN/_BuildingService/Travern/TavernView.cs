using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class TavernView : BuildView
    {
       // [SerializeField] HelpName helpName;
        //public BuildingNames BuildingName => BuildingNames.Tavern;

        //[Header("To be ref")]
        //[SerializeField] Transform btnContainer;
        //[SerializeField] Transform NPCInteractPanel;

        //[Header("Not to be ref")]
        //[SerializeField] Transform BGSpriteContainer;

        //[Header("Tavern Interact Panels: To be ref")]
        //[SerializeField] Transform BuildInteractPanel;


        //[Header("Day and Night BG Sprites")]
        //[SerializeField] Sprite dayBG;
        //[SerializeField] Sprite nightBG;

        [Header("Build Interaction panel")]
        public Transform bountyBoard;
        public Transform buyDrink;
        public Transform trophy;
        public Transform rest;


        //[SerializeField] Button exitBtn;

        //BuildingSO tavernSO;
        //TimeState timeState;

        //void Awake()
        //{
        //    BGSpriteContainer = transform.GetChild(0);
        //    exitBtn.onClick.AddListener(UnLoad);
        //}
        //public void Init()
        //{
        //    UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        //    AllBuildSO allBuildSO = BuildingIntService.Instance.allBuildSO; 
        //    BuildingSO tavernSO = allBuildSO.GetBuildSO(BuildingNames.Tavern);
        //    dayBG = tavernSO.buildIntDay;
        //    nightBG = tavernSO.buildIntNight; 

        //    timeState = CalendarService.Instance.currtimeState;
        //    btnContainer.GetComponent<BuildInteractBtnView>().InitInteractBtns(this);
        //    FillTavernBG();
        //    InitInteractPanels();
        //}
        //public void FillTavernBG()
        //{
        //    if (CalendarService.Instance.currtimeState == TimeState.Night)
        //    {
        //        BGSpriteContainer.GetComponent<Image>().sprite = nightBG;
        //    }
        //    else
        //    {
        //        BGSpriteContainer.GetComponent<Image>().sprite = dayBG;
        //    }

        //    for (int i = 0; i < BGSpriteContainer.childCount; i++)
        //    {
        //        BGSpriteContainer.GetChild(i).GetComponent<TavernBaseEvents>().Init(this);
        //    }
        //}
        //void InitInteractPanels()
        //{
        //    foreach (Transform child in BuildInteractPanel)
        //    {
        //        child.GetComponent<IPanel>().Init(); // interact panels initialized here 
        //    }
        //}

        public override Transform GetBuildInteractPanel(BuildInteractType buildInteract)
        {
            switch (buildInteract)
            {
                case BuildInteractType.None:
                    return null;
                case BuildInteractType.Bounty:
                    return bountyBoard;
                case BuildInteractType.BuyDrink:
                    return buyDrink;
                case BuildInteractType.Trophy:
                    return trophy;                
                case BuildInteractType.EndDay:
                    return rest;
                default:
                    return null;
            }
        }
        //public void Load()
        //{
           
        //}

        //public void UnLoad()
        //{
        //    UIControlServiceGeneral.Instance.TogglePanelOnInGrp(this.gameObject, false);
        //    foreach (Transform child in BuildInteractPanel)
        //    {
        //        child.GetComponent<IPanel>().UnLoad();
        //    }
        //    TownService.Instance.townViewController.OnBuildDeselect();
        //}
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        Init(); // for test

        //    }
        //}

        //public HelpName GetHelpName()
        //{
        //   return helpName;
        //}
    }
}