using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Common;
using Interactables;
using Intro;
using UnityEngine.SceneManagement;

namespace Town
{
    public class TempleController : MonoBehaviour
    {
        [Header("Temple Model")]
        public TempleModel templeModel;
        public TempleView templeView;
        private void Start()
        {
            CalendarService.Instance.OnChangeTimeState += (TimeState timeStart) => UpdateBuildState();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TOWN")
            {
                templeView = FindObjectOfType<TempleView>(true);
            }
        }
        public void InitTempleController()
        {
            BuildingSO templeSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Temple);
            templeModel = new TempleModel(templeSO);
            BuildingIntService.Instance.allBuildModel.Add(templeModel);
       
        }

        public void UpdateBuildState()
        {
            if (templeModel.buildState == BuildingState.Locked) return;
            DayName dayName = CalendarService.Instance.currDayName;
            TimeState timeState = CalendarService.Instance.currtimeState;

            if (dayName == DayName.DayOfWater)
            {
                templeModel.buildState = BuildingState.UnAvailable;
                return; 
            }
                
            if(timeState == TimeState.Night)            
                templeModel.buildState = BuildingState.UnAvailable;            
            else            
                templeModel.buildState = BuildingState.Available;
            
        }
        public void UnLockBuildIntType(BuildInteractType buildIntType, bool unLock)
        {
            templeView = FindObjectOfType<TempleView>(true);
        
            foreach (BuildIntTypeData buildData in templeModel.buildIntTypes)
            {
                if (buildData.BuildIntType == buildIntType)
                {
                    buildData.isUnLocked = unLock;
                    templeView.InitBuildIntBtns(templeModel as BuildingModel);
                }
            }
        }

        // [Header("Top Panel")]
        // [SerializeField] GameObject topPortraits;

        // [Header("MINAMI and Soothsayer Btns")]
        // [SerializeField] Button soothSayerBtn;
        // [SerializeField] Button fountainBtn;    


        // [Header("Interaction panel Btns")]
        // public  GameObject buttonsPanel; //accessed by events
        // [SerializeField] GameObject buttonSelected; 

        //// public GameObject talkPrefab;

        // public CharNames currCharSelected;
        // public List<CharController> charInTemple = new List<CharController>();

        // [SerializeField] TalkPanelController talkPanelController;
        // void Start()
        // {
        //     soothSayerBtn.onClick.AddListener(OnSoothSayerBtnPressed);
        //     fountainBtn.onClick.AddListener(OnFountainBtnPressed);
        //    // talkPanelController = talkPrefab.GetComponent<TalkPanelController>(); // Dialogue Prefab

        // }

        // void OnSoothSayerBtnPressed()
        // {
        //     // talkPrefab.. Opne 
        //     // NPC SO from Char Service, Build Up Panel Init TalkController
        //     DialogueService.Instance.StartDialogue(DialogueNames.MeetMinami);

        // }

        // public void ClearMind()
        // {

        // }

        // void ShowClearMindPanel()
        // {
        //     charInTemple = CharService.Instance.charsInPlayControllers; 
        //     // populate temp trait (mental)using traits service



        // }


        // void OnMeetMinamiDiaCompletion()
        // {
        //     // put in the Rayyan Portrait.. 
        //     // get HEx portrait from the hex

        // }

        // void OnFountainBtnPressed()
        // {
        //     Debug.Log("Fountain Pressed");
        // }
        // public void Load()
        // {

        // }

        // public void UnLoad()
        // {

        // }

        // public void Init()
        // {
        //     // get building SO ,, get interactions Populate the interaction Prefabs 
        //     // init when clicked
        // }
        // void PopulateBtns()
        // {


        // }

        // // HELPERS 
        // public string GetBtnName(int i)
        // {

        //     return ""; 
        // }

    }
}
