using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using UnityEngine.SceneManagement;


namespace Town
{
    //interface IBuildName
    //{
    //    BuildingNames BuildingName { get; set; }    
    //}
    public class TownViewController : MonoBehaviour, iHelp
    {
        [Header("HelpName")]
        [SerializeField] HelpName helpName; 
        [Header("Cloud")]
        [SerializeField] Transform cloudTrans; 

        public BuildingNames selectBuild;
        [Header("NTBR")]
        [SerializeField] Transform buildContainer;
        [SerializeField] Image townBGImage;
        [Header("TBR")]
        [SerializeField] Transform buildIntContainer;
        [Header("TBR : Build Bark container")]
        public Transform buildBarkContainer;

        [Header("Build Model")]
        [SerializeField] BuildingModel buildModel;

        private void Start()
        {
          //  CalendarService.Instance.OnChangeTimeState += TownViewInit;
          //  SceneManager.sceneLoaded += OnSceneLoaded; 
        }
        private void OnDisable()
        {
        //    CalendarService.Instance.OnChangeTimeState -= TownViewInit;
           // SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        //{
        //    if (scene.name == "TOWN")
        //    {
        //        TimeState timeState = CalendarService.Instance.currtimeState;
        //        TownViewInit(timeState);
        //    }
        //}
        public void OnBuildSelect(BuildingNames buildName)
        {
            selectBuild = buildName; 
            int index = (int)buildName - 1; // correction for none
            
            for (int i = 0; i < buildContainer.childCount; i++)
            {
                BuildBasePtrEvents buildBase = buildContainer.GetChild(i).GetComponent<BuildBasePtrEvents>();
                if (buildBase == null) continue; 
                  if (buildBase.buildingName == buildName)
                  {
                      buildBase.OnSelect();                   
                  }
                  else
                  {
                      buildBase.OnDeSelect();
                  }
            }

            buildModel = BuildingIntService.Instance.GetBuildModel(buildName);
            if (buildModel == null) return; 
            if(buildModel.buildState == BuildingState.UnAvailable || buildModel.buildState == BuildingState.Locked)
            {
                ShowBuildBarks(buildName, buildModel.buildState);
                OnBuildDeselect(); return;
            }

            // Interior Init
            foreach (Transform child in buildIntContainer)
            {
                BuildView buildView = child.GetComponent<BuildView>();
                if(buildView != null)
                if(child.GetComponent<BuildView>().BuildingName == selectBuild)
                {
                    child.GetComponent<IPanel>().Init();                     
                }
            }
        }

        public void OnBuildDeselect()
        {
            
            for (int i = 0; i < buildContainer.childCount; i++)
            {
                BuildBasePtrEvents buildBase = buildContainer.GetChild(i).GetComponent<BuildBasePtrEvents>();
                              
                if(buildBase != null)
                {
                    buildBase.OnDeSelect();                  
                }
            }
            selectBuild = BuildingNames.None;
        }
        public void TownViewInit(TimeState timeState)
        {
           
                
            buildContainer = transform.GetChild(0);
            townBGImage = transform.GetChild(0).GetComponent<Image>();
            FillTownBG(); 
            foreach (Transform child in buildContainer)
            {
               child.GetComponent<BuildBasePtrEvents>().Init(this);
            }
          
            if (timeState == TimeState.Day 
                && GameService.Instance.gameModel.gameState == GameState.InTown)
                cloudTrans.gameObject.SetActive(true);
            else
                cloudTrans.gameObject.SetActive(false);

        }
        void FillTownBG()
        {
            if (CalendarService.Instance.currtimeState == TimeState.Night)
                townBGImage.sprite = TownService.Instance.allbuildSO.TownBGNight;
            else
                townBGImage.sprite = TownService.Instance.allbuildSO.TownBGDay;
            
        }

        public HelpName GetHelpName()
        {
            return helpName; 
        }
        public void ShowBuildBarks(BuildingNames buildName, BuildingState buildState)
        {
            foreach (Transform trans in transform.GetChild(1))
            {                
                BuildBarkPtrEvents buildBarks = trans.GetComponent<BuildBarkPtrEvents>();
                if (buildBarks == null) continue; 
                if(buildBarks.buildName == buildName)
                {
                    // Get Build Models  ... 
                    buildBarks.ShowBark(buildName, buildState); 
                }
            }
           


        }

    }



}
