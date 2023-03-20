using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


namespace Town
{
    public class MarketView : MonoBehaviour, IPanel, IBuildName
    {

        public BuildingNames BuildingName => BuildingNames.Marketplace;

        [Header("To be ref")]
        [SerializeField] Transform btnContainer;
        [SerializeField] Transform NPCInteractPanel;

        [Header("Not to be ref")]
        [SerializeField] Transform BGSpriteContainer;

        [Header("House Interact Panels: To be ref")]
        [SerializeField] Transform BuildInteractPanel;

        public Transform craftPotionPanel;
        public Transform fortifyPanel;

        [Header("Day and Night BG Sprites")]
        [SerializeField] Sprite dayBG;
        [SerializeField] Sprite nightBG;

        [SerializeField] Button exit;

        BuildingSO marketSO;
        TimeState timeState;
        void Awake()
        {
            BGSpriteContainer = transform.GetChild(0);
            exit.onClick.AddListener(UnLoad);
        }
        public void Init()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
            marketSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.House);
            timeState = CalendarService.Instance.currtimeState;
           // btnContainer.GetComponent<HouseInteractBtnView>().InitInteractBtns(this);
            FillMarketBG();
            InitInteractPanels();

        }
        public void FillMarketBG()
        {
            if (CalendarService.Instance.currtimeState == TimeState.Night)
            {
                BGSpriteContainer.GetComponent<Image>().sprite = nightBG;
            }
            else
            {
                BGSpriteContainer.GetComponent<Image>().sprite = dayBG;
            }

            for (int i = 0; i < 4; i++)
            {
                //  BGSpriteContainer.GetChild(i).GetComponent<HouseBaseEvents>().Init(this);
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
                case BuildInteractType.CraftPotion:
                    return craftPotionPanel;
                case BuildInteractType.Fortify:
                    return fortifyPanel;                    
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

        }


    }

}