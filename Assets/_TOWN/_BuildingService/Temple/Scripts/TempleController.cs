using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Common;
using Interactables;
using Intro;
namespace Town
{
    public class TempleController : MonoBehaviour
    {

        [Header("Temple Model")]
        public TempleModel templeModel;

   
        public TempleViewController templeView;


        private void Start()
        {
            BuildingSO templeSO = BuildingIntService.Instance.allBuildSO.GetBuildSO(BuildingNames.Temple);
            templeModel = new TempleModel(templeSO); 
         
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
