using UnityEngine;
using UnityEngine.UI;
using Common;


namespace Town
{
    public class ShipView : BuildView
    {
    //    [SerializeField] HelpName helpName;
    //  //  public BuildingNames BuildingName => BuildingNames.Ship;

    //    [Header("To be ref")]
    //    [SerializeField] Transform btnContainer;
    //    [SerializeField] Transform NPCInteractPanel;

    //    [Header("Not to be ref")]
    //    [SerializeField] Transform BGSpriteContainer;

    //    [Header("House Interact Panels: To be ref")]
    //    [SerializeField] Transform BuildInteractPanel;

        public Transform smugglePanel ;
    

        //[Header("Day and Night BG Sprites")]
        //[SerializeField] Sprite dayBG;
        //[SerializeField] Sprite nightBG;

        //[SerializeField] Button exit;

        //BuildingSO shipSO;
        //TimeState timeState;
        //void Awake()
        //{
        //    BGSpriteContainer = transform.GetChild(0);
        //    exit.onClick.AddListener(UnLoad);
        //}
        //public void Init()
        //{
        //    UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        //    shipSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.House);
        //    timeState = CalendarService.Instance.currtimeState;
        //   // btnContainer.GetComponent<HouseInteractBtnView>().InitInteractBtns(this);
        //    FillShipBG();
        //    InitInteractPanels();

        //}
        //public void FillShipBG()
        //{
        //    if (CalendarService.Instance.currtimeState == TimeState.Night)
        //    {
        //        BGSpriteContainer.GetComponent<Image>().sprite = nightBG;
        //    }
        //    else
        //    {
        //        BGSpriteContainer.GetComponent<Image>().sprite = dayBG;
        //    }

        //    for (int i = 0; i < 4; i++)
        //    {
        //       // BGSpriteContainer.GetChild(i).GetComponent<HouseBaseEvents>().Init(this);
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
                case BuildInteractType.Smuggle:
                    return smugglePanel;                
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

        //public HelpName GetHelpName()
        //{
        //    return helpName;
        //}
    }
}