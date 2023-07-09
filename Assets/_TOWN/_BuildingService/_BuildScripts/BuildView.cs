using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI; 
using Common; 

namespace Town
{
    public class BuildView : MonoBehaviour
    {
        [SerializeField] HelpName helpName;
      

        [Header("To be ref")]
        [SerializeField] Transform btnContainer;
        [SerializeField] Transform NPCInteractPanel;

        [Header("Not to be ref")]
        [SerializeField] Transform BGSpriteContainer;

        [Header("House Interact Panels: To be ref")]
        [SerializeField] Transform BuildInteractPanel;

        [Header("Day and Night BG Sprites")]
        [SerializeField] Sprite dayBG;
        [SerializeField] Sprite nightBG;

        [SerializeField] Button exit;

        BuildingSO buildSO;
        BuildingModel buildModel;
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