using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using Common;
using System;

namespace Town
{
    public class BuildView : MonoBehaviour, IPanel ,iHelp
    {

        [SerializeField] HelpName helpName;
        public BuildingNames BuildingName;

        [Header("To be ref")]
        [SerializeField] Transform btnContainer;
        [SerializeField] Transform NPCIntPanel;
        [SerializeField] Transform charIntPanel;

        [Header("Not to be ref")]
        [SerializeField] Transform BGSpriteContainer;

        [Header("House Interact Panels: To be ref")]
        [SerializeField] Transform BuildInteractPanel;

        [Header("Day and Night BG Sprites")]
        [SerializeField] Sprite dayBG;
        [SerializeField] Sprite nightBG;

        [SerializeField] Button exit;


        [Header("Talk and trade Panel")]
        public Transform talkNTradeBtns; 
        public Transform TradePanel;
        public Transform TalkPanel;

        BuildingSO buildSO;
        [SerializeField]BuildingModel buildModel;
        TimeState timeState;
        void OnEnable()
        {
            BGSpriteContainer = transform.GetChild(0);
            exit.onClick.AddListener(UnLoad);          
        }
        public void Init()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
            buildSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingName);

            timeState = CalendarService.Instance.currtimeState;
            buildModel = BuildingIntService.Instance.GetBuildModel(BuildingName);
            InitBuildIntBtns( buildModel); 
            FillBuildBG();
            InitBuildIntPanels();
            InitNPCNCharIntPanels();
            BuildingIntService.Instance.On_BuildInit(buildModel, this);
            DialogueService.Instance.OnDialogueStart -= ShowBtnContainer;
            DialogueService.Instance.OnDialogueStart += ShowBtnContainer;

            DialogueService.Instance.OnDialogueEnd -= HideBtnContainer;
            DialogueService.Instance.OnDialogueEnd += HideBtnContainer;


            btnContainer.gameObject.SetActive(true);
        }

        void ShowBtnContainer(DialogueNames dialogueName)
        {
            btnContainer.gameObject.SetActive(false);
        }
        void HideBtnContainer()
        {
            btnContainer.gameObject.SetActive(true);
        }
        public void InitBuildIntBtns(BuildingModel _buildModel)
        {
            btnContainer = gameObject.GetComponentInChildren<BuildInteractBtnView>(true).transform;
            btnContainer.GetComponent<BuildInteractBtnView>().InitInteractBtns(this, _buildModel);
        }
        public void FillBuildBG()
        {
            if (CalendarService.Instance.currtimeState == TimeState.Night)
            {
                BGSpriteContainer.GetComponent<Image>().sprite = nightBG;
            }
            else
            {
                BGSpriteContainer.GetComponent<Image>().sprite = dayBG;
            }

            foreach (Transform child in BGSpriteContainer)
            {
                BuildBaseEvents baseEvents = child?.GetComponent<BuildBaseEvents>();
              //  Debug.Log(" build model" + buildModel.buildingName); 
                if(baseEvents != null)
                    baseEvents.Init(this, buildModel);
            }
        }
        public void InitBuildIntPanels()
        {
            BuildInteractPanel = gameObject.GetComponentInChildren<BuildInteract>(true).transform; 
            talkNTradeBtns = gameObject.GetComponentInChildren<TalkNTradeBtnView>(true).transform;

            foreach (Transform child in BuildInteractPanel)
            {
                child.GetComponent<IPanel>().Init(); // interact panels initialized here 
            }
            talkNTradeBtns.GetComponent<TalkNTradeBtnView>().HideBtns();
        }
        public virtual Transform GetBuildInteractPanel(BuildInteractType buildInteract)
        {
            return null; 
        }

        void InitNPCNCharIntPanels()
        {
            // get parent first then find the 
            NPCIntPanel = gameObject.GetComponentInChildren<BuildNPCIntView>(true).transform; 
            charIntPanel = gameObject.GetComponentInChildren<BuildCharIntView>(true).transform;  

            NPCIntPanel.GetComponent<BuildNPCIntView>().InitIntPorts(this, buildModel);
            charIntPanel.GetComponent<BuildCharIntView>().InitIntPorts(this, buildModel); 
        }

        public Transform GetNPCInteractPanel(IntType npcInteract)
        {
            switch (npcInteract)
            {
                case IntType.Talk:
                    return TalkPanel;
                case IntType.Trade:
                    return TradePanel;
                default:
                    break;
            }
            Debug.Log("Build interaction panel not found" + npcInteract);
            return null;
        }

        public void OnPortSelect(CharIntData charIntData, NPCIntData nPCIntData)
        {
            if(charIntData != null)
            {
                talkNTradeBtns.GetComponent<TalkNTradeBtnView>().InitTalkNTrade(charIntData, this); 
            }
            if (nPCIntData != null)
            {
                talkNTradeBtns.GetComponent<TalkNTradeBtnView>().InitTalkNTrade(nPCIntData, this);
            }
        }
        public void OnPortHide()
        {
            // use events to control this
        }
        public void OnPortShow()
        {

        }
        public void CloseTalkNTrade()
        {

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
            BuildingIntService.Instance.On_BuildUnload(buildModel, this);

        }

        public HelpName GetHelpName()
        {
            return helpName;
        }
    }
}


//[Header("Building CLICKS")]

//[Header("HOUSE")]
//[SerializeField] Button houseDayBtn;
//[SerializeField] Button houseNightBtn;

//[Header("TEMPLE")]
//[SerializeField] Button templeDayBtn;
//[SerializeField] Button templeNightBtn;

//[Header("MARKET PLACE")]
//[SerializeField] Button marketDayBtn;
//[SerializeField] Button marketNightBtn;


//[Header("Building Panels")]
//[SerializeField] Transform housePanel;
//[SerializeField] Transform marketPanel;
//[SerializeField] Transform templePanel;

//// use Ipanel to open and close 

//void Start()
//{
//    templeDayBtn.onClick.AddListener(OnTempleBtnPressed);
//    templeNightBtn.onClick.AddListener(OnTempleBtnPressed);

//    marketDayBtn.onClick.AddListener(OnMarketBtnPressed);
//    marketNightBtn.onClick.AddListener(OnMarketBtnPressed);

//    houseDayBtn.onClick.AddListener(OnHouseBtnPressed);
//    houseNightBtn.onClick.AddListener(OnHouseBtnPressed);

//}

//void OnTempleBtnPressed()
//{
//    if (CalendarService.Instance.currtimeState == TimeState.Day)
//        UIControlServiceGeneral.Instance.TogglePanel(templePanel.gameObject, true);
//    //locked at night  
//}

//void OnMarketBtnPressed()
//{
//    UIControlServiceGeneral.Instance.TogglePanel(marketPanel.gameObject, true);
//    if (CalendarService.Instance.currtimeState == TimeState.Day)
//    {
//        marketPanel.GetChild(0).gameObject.SetActive(true);
//        marketPanel.GetChild(1).gameObject.SetActive(false);
//    }
//    else
//    {
//        marketPanel.GetChild(0).gameObject.SetActive(false);
//        marketPanel.GetChild(1).gameObject.SetActive(true);
//    }
//}

//void OnHouseBtnPressed()
//{
//    UIControlServiceGeneral.Instance.TogglePanel(housePanel.gameObject, true);
//    if (CalendarService.Instance.currtimeState == TimeState.Day)
//    {
//        housePanel.GetChild(0).gameObject.SetActive(true);
//        housePanel.GetChild(1).gameObject.SetActive(false);
//    }
//    else
//    {
//        housePanel.GetChild(0).gameObject.SetActive(false);
//        housePanel.GetChild(1).gameObject.SetActive(true);
//    }
//}