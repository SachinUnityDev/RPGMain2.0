using Common;
using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement; 


namespace Town
{
    public class WelcomeService : MonoSingletonGeneric<WelcomeService>
    {
        
        public WelcomeController welcomeController;
        [Header("TBR")]
        public WelcomeView welcomeView;
        [Header("NTBR")]
        [SerializeField]GameObject cornerBtns;

        public bool isWelcomeRun = false;
        public bool isQuickStart = false; 
        [SerializeField] int welcomeStartDay;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoad;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoad;
        }
        void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "TOWN")
            {
                welcomeView = FindObjectOfType<WelcomeView>();
                cornerBtns = GameObject.FindGameObjectWithTag("TownBtns");
                cornerBtns.SetActive(true);

            }
        }

        #region WELCOME TWO DAY GAME PLAY GUIDE
        public void InitWelcome()
        {
            isWelcomeRun = true;

            welcomeController = GetComponent<WelcomeController>();
            cornerBtns = GameObject.FindGameObjectWithTag("TownBtns");
            cornerBtns.SetActive(false);
          //  welcomeStartDay = CalendarService.Instance.dayInGame;
            welcomeView.InitWelcomeView();           
        }
        public void InitWelcomeComplete()
        {
           
            isWelcomeRun = false;
            welcomeController = GetComponent<WelcomeController>();

            // town btns unlocked
            cornerBtns = GameObject.FindGameObjectWithTag("TownBtns");

            cornerBtns.SetActive(true);
            if (isQuickStart)
            {

            }
            else
            {
              
                welcomeView.InitWelcomeView();
            }
            BuildingIntService.Instance.ChgCharState(BuildingNames.Tavern, CharNames.Cahyo, NPCState.UnLockedNAvail, false);

            CalendarService.Instance.OnStartOfCalDate -= GoVisitTemple2dayGap;
            CalendarService.Instance.OnStartOfCalDate += GoVisitTemple2dayGap;


            //            Interactions unlocked:
            //House
            //endday, provision, chest, cure sickness

            //Tavern
            //trophy, buy service, bounties, endday

            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.EndDay, true);
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.Provision, true);
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.Chest, true);
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.CureSickness, true);
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.Purchase, true);

            BuildingIntService.Instance.tavernController.UnLockBuildIntType(BuildInteractType.Trophy, true);
            BuildingIntService.Instance.tavernController.UnLockBuildIntType(BuildInteractType.BuyDrink, true);
            BuildingIntService.Instance.tavernController.UnLockBuildIntType(BuildInteractType.Bounty, true);
            BuildingIntService.Instance.tavernController.UnLockBuildIntType(BuildInteractType.EndDay, true);
        }

        void GoVisitTemple2dayGap(CalDate calDate)
        {
            if(calDate.day >= 28)
            {
                BuildingIntService.Instance
                    .UnLockDiaInBuildNPC(BuildingNames.House, NPCNames.Khalid, DialogueNames.GoVisitTemple, true);
                CalendarService.Instance.OnStartOfCalDate -= GoVisitTemple2dayGap;
            }
        }
        #endregion

        public void On_QuickStart()
        {
            InitWelcomeComplete();
            // unlock buildings
            BuildingIntService.Instance.UnLockABuild(BuildingNames.Tavern, true);
            BuildingIntService.Instance.UnLockABuild(BuildingNames.Marketplace, true);
            BuildingIntService.Instance.UnLockABuild(BuildingNames.House, true);

            // Npc 
            BuildingIntService.Instance.ChgNPCState(BuildingNames.Marketplace, NPCNames.Amish, NPCState.UnLockedNAvail, false);
            BuildingIntService.Instance.ChgNPCState(BuildingNames.Marketplace, NPCNames.Amadi, NPCState.UnLockedNAvail, false);
            BuildingIntService.Instance.ChgNPCState(BuildingNames.Marketplace, NPCNames.Kamila, NPCState.UnLockedNAvail, false);
            BuildingIntService.Instance.ChgNPCState(BuildingNames.Marketplace, NPCNames.Omobolanle, NPCState.UnLockedNAvail, false);

            BuildingIntService.Instance.ChgNPCState(BuildingNames.Tavern, NPCNames.Greybrow, NPCState.UnLockedNAvail, false);

            BuildingIntService.Instance.ChgNPCState(BuildingNames.House, NPCNames.Khalid, NPCState.UnLockedNAvail, false);


            // Dailogue Completed 
            DialogueService.Instance.GetDialogueModel(DialogueNames.MeetKhalid).isPlayedOnce= true;
            DialogueService.Instance.GetDialogueModel(DialogueNames.MeetGreybrow).isPlayedOnce = true;
            DialogueService.Instance.GetDialogueModel(DialogueNames.RetrieveDebt).isPlayedOnce = true;
            DialogueService.Instance.GetDialogueModel(DialogueNames.DebtIsClear).isPlayedOnce = true;
            DialogueService.Instance.GetDialogueModel(DialogueNames.AttendJob).isPlayedOnce = true;

            // Quest model update the first quest completed
            QuestModel questModel = 
                 QuestMissionService.Instance.GetQuestModel(QuestNames.LostMemory);            
            questModel.OnQuestCompleted();

            QuestMissionService.Instance.On_QuestStart(QuestNames.ThePowerWithin);

            CalendarService.Instance.MoveCalendarByDay(2);
        }


    }
}