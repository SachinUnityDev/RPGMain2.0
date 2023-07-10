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
    public class TempleView : BuildView
    {
       // [SerializeField] HelpName helpName;

       // public BuildingNames BuildingName => BuildingNames.Temple;
        //[Header("To be ref")]
        //[SerializeField] Transform btnContainer;
        //[SerializeField] Transform NPCInteractPanel;

        //[Header("Not to be ref")]
        //[SerializeField] Transform BGSpriteContainer;

        //[Header("Temple Interact Panels: To be ref")]
        //[SerializeField] Transform BuildInteractPanel;

        public Transform enchantPanel;
        public Transform clearMindPanel;

        //public Transform TradePanel;
        //public Transform TalkPanel; 


        //[SerializeField] Button exit;

        //BuildingSO templeSO;
        //BuildingModel templeModel;
        //void Awake()
        //{
        //    BGSpriteContainer = transform.GetChild(0);
        //    exit.onClick.AddListener(UnLoad);
        //}
        //public void Init()
        //{
        //    Load();
        //    templeSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Temple);
        //    btnContainer.GetComponent<TempleInteractBtnView>().InitInteractBtns(this);
        //    FillTempleBG();
        //    InitInteractPanels();
           
        //}
        //public void FillTempleBG()
        //{
        //    if (CalendarService.Instance.currtimeState == TimeState.Night)
        //    {
        //        Sprite nightBG = templeSO.buildIntDay; 
        //        BGSpriteContainer.GetComponent<Image>().sprite = nightBG;
        //    }
        //    else
        //    {
        //        Sprite dayBG = templeSO.buildIntDay;
        //        BGSpriteContainer.GetComponent<Image>().sprite = dayBG;
        //    }

        //    foreach (Transform child in BGSpriteContainer)
        //    {
        //        child.GetComponent<TempleBaseEvents>().Init(this);
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
                case BuildInteractType.Enchant:
                    return enchantPanel; 
                case BuildInteractType.ClearMind:
                    return clearMindPanel;  
            }
            Debug.Log("Building interaction panel not found" + buildInteract);
            return null; 
        }

 

        //public void InitNpcInteractPanel(NPCInteractData npcInteractData)
        //{

        //}

        //public void Load()
        //{
        //    UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        //}

        //public void UnLoad()
        //{
        //    UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        //    TownService.Instance.townViewController.OnBuildDeselect();
        //}
        //public HelpName GetHelpName()
        //{
        //    return helpName;
        //}
    }
}