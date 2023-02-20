using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;
namespace Town
{
    interface IBuildName
    {
        BuildingNames BuildingName { get; }    
    }
    public class TownViewController : MonoBehaviour
    {

        [Header("Left Town Btns")]
        [SerializeField] Button rosterBtn;
        [SerializeField] Button jobBtn;
        [SerializeField] Button inventoryBtn;


        [Header("Right Town Btns")]
        [SerializeField] Button eventBtn;
        [SerializeField] Button questScrollBtn;
        [SerializeField] Button mapBtn;

        [SerializeField] GameObject InteractionPanel;

        [SerializeField] List<GameObject> allPanels = new List<GameObject>();


        public BuildingNames selectBuild;
        [SerializeField] Transform buildContainer;
        [Header("TBR")]
        [SerializeField] Transform buildIntContainer; 
        [SerializeField] Image townBGImage; 

        void Start()
        {
            buildContainer = transform.GetChild(0);
            townBGImage = transform.GetChild(0).GetComponent<Image>();
            CalendarService.Instance.OnStartOfDay +=(int day)=> TownViewInit();
            CalendarService.Instance.OnStartOfNight += (int day) => TownViewInit();
        }
        public void OnBuildSelect(BuildingNames buildName)
        {
            selectBuild = buildName; // correction for none
            int index = (int)buildName - 1; 
            for (int i = 0; i < buildContainer.childCount; i++)
            {
                BuildBasePtrEvents buildBase = buildContainer.GetChild(i).GetComponent<BuildBasePtrEvents>(); 
              if (buildBase.buildingName == buildName)
              {
                buildContainer.GetChild(i).GetComponent<BuildBasePtrEvents>().OnSelect();                   
              }
              else
              {
                buildContainer.GetChild(i).GetComponent<BuildBasePtrEvents>().OnDeSelect();
              }
            }

            foreach (Transform child in buildIntContainer)
            {
                if(child.GetComponent<IBuildName>().BuildingName == selectBuild)
                {
                    child.GetComponent<IPanel>().Init(); 
                }
            }


        }
        public void TownViewInit()
        {
            FillTownBG(); 
            foreach (Transform child in buildContainer)
            {
               child.GetComponent<BuildBasePtrEvents>().Init(this);
            }
        }
        void FillTownBG()
        {
            if (CalendarService.Instance.currtimeState == TimeState.Night)
                townBGImage.sprite = TownService.Instance.allbuildingSO.TownBGNight;
            else
                townBGImage.sprite = TownService.Instance.allbuildingSO.TownBGDay;
            
        }
    }



}
