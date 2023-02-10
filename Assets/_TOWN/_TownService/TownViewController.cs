using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;
namespace Town
{
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
        [SerializeField] Image townBGImage; 

        void Start()
        {
            buildContainer = transform.GetChild(0);
            townBGImage = transform.GetChild(0).GetComponent<Image>();
            CalendarService.Instance.OnStartOfDay +=(int day)=> TownViewInit();
            CalendarService.Instance.OnStartOfNight += (int day) => TownViewInit();
        }
        public void OnBuildSelect(int index)
        {
            selectBuild = (BuildingNames)(index + 1); // correction for none
            for (int i = 0; i < buildContainer.childCount; i++)
            {                
              buildContainer.GetChild(i).GetComponent<BuildingPtrEvents>().OnDeSelect();                
            }
        }
        public void TownViewInit()
        {
            FillTownBG(); 
            foreach (Transform child in buildContainer)
            {
               child.GetComponent<BuildingPtrEvents>().Init(this);
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
