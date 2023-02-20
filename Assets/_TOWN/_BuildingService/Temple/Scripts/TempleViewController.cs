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
    public class TempleViewController : MonoBehaviour, IPanel, IBuildName
    {

        public BuildingNames BuildingName => BuildingNames.Temple;
        [Header("To be ref")]
        [SerializeField] Transform btnContainer;
        [SerializeField] Transform NPCInteractPanel;

        [Header("Not to be ref")]
        [SerializeField] Transform BGSpriteContainer;

        [Header("House Interact Panels: To be ref")]
        [SerializeField] Transform BuildInteractPanel;

        public Transform enchantPanel;
        public Transform clearMindPanel;


        [SerializeField] Button exit;

        BuildingSO templeSO;

     

        void Awake()
        {
            BGSpriteContainer = transform.GetChild(0);
            exit.onClick.AddListener(UnLoad);
        }
        public void Init()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
            templeSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Temple);           
            btnContainer.GetComponent<TempleInteractBtnView>().InitInteractBtns(this);
            FillTempleBG();
            InitInteractPanels();
        }
        public void FillTempleBG()
        {
            if (CalendarService.Instance.currtimeState == TimeState.Night)
            {
                Sprite nightBG = templeSO.buildingData.buildIntDay; 
                BGSpriteContainer.GetComponent<Image>().sprite = nightBG;
            }
            else
            {
                Sprite dayBG = templeSO.buildingData.buildIntDay;
                BGSpriteContainer.GetComponent<Image>().sprite = dayBG;
            }

            foreach (Transform child in BGSpriteContainer)
            {
                child.GetComponent<TempleBaseEvents>().Init(this);
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
                case BuildInteractType.Enchant:
                    return enchantPanel; 
                case BuildInteractType.ClearMind:
                    return clearMindPanel;  
            }
            Debug.Log("Building interaction panel not found" + buildInteract);
            return null; 
        }
        public void Load()
        {
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(this.gameObject, false);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Init(); // for test

            }
        }


    }
}