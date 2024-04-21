using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using Combat;
using Interactables;
using Town;
using Quest;
namespace Common
{
    public class GameEventService : MonoSingletonGeneric<GameEventService>
    {
        public Action OnMainGameStart;
        public Action OnMainGameEnd;

        public bool hasGameStarted = false; 


        public Action<LocationName> OnTownEnter;
        public Action<LocationName> OnTownExit;

        public Action OnIntroStart;
        public Action OnIntroEnd; 


        public Action<GameState> OnGameStateChg; 
        public event Action<QuestMode> OnQuestModeChg;

        public Action<GameState> OnStateStart;
       
        void OnEnable()
        {
           // OnGameStateChg += On_QuestRoomStart;
            OnGameStateChg += On_TownEnter; 
        }
        private void OnDisable()
        {
           // OnGameStateChg -= On_QuestRoomStart;
            OnGameStateChg -= On_TownEnter;
        }

        public void On_TownEnter(GameState gameState)
        {
            // Anything initialised by SO to be put here 
            // any initialisation during scene swap to be checked 
            // model init to be implemented after the save service connection 
            if (GameService.Instance.isNewGInitDone)
                return;
            if (gameState != GameState.InTown)
                return; 
                LocationName locationName = LocationName.Nekkisari;
                
               
                TownService.Instance.Init(locationName);
                BuildingIntService.Instance.InitBuildIntService();
                EncounterService.Instance.EncounterInit();
                CharService.Instance.Init();
                UIControlServiceGeneral.Instance.InitUIGeneral();
                CalendarService.Instance.Init();

                EcoServices.Instance.InitEcoServices();
                BarkService.Instance.InitBarkService();
                JobService.Instance.JobServiceInit();
                BestiaryService.Instance.Init();
                ItemService.Instance.Init();
                TradeService.Instance.InitTradeService();
                FameService.Instance.Init();
                LevelService.Instance.Init();
                WeaponService.Instance.Init();
                ArmorService.Instance.Init(); 

                OnTownEnter?.Invoke(locationName);
                QuestMissionService.Instance.InitQuestMission();
                LandscapeService.Instance.InitLandscape(); 
                MapService.Instance.InitMapService(); // to be put below questmission
                LootService.Instance.InitLootService();
            if (WelcomeService.Instance.isQuickStart)
                WelcomeService.Instance.On_QuickStart();
            else
                WelcomeService.Instance.InitWelcome();
            On_MainGameStart();
            if (!GameService.Instance.isNewGInitDone)
            {
                if (GameService.Instance.gameController.isQuickStart)
                {
                    CharService.Instance.GetAbbasController(CharNames.Abbas).charModel.classType
                        = GameService.Instance.gameModel.abbasClassType;

                    // Set job NAME SELECT HERE 
                }               
                GameService.Instance.isNewGInitDone = true;
            }
        }
        public void On_TownExit(LocationName locationName)
        {
            OnTownExit?.Invoke(locationName);
        }

        public void On_MainGameStart()
        {
            hasGameStarted = true; 
            OnMainGameStart?.Invoke();
        }
        public void On_MainGameEnd()
        {
            hasGameStarted= false;
            OnMainGameEnd?.Invoke(); 
        }
        public void On_IntroStart()
        {
            DialogueService.Instance.InitDialogueService();
            OnIntroStart?.Invoke();
        }
        public void On_IntroEnd()
        {            
            OnIntroEnd?.Invoke();
        }
    }


}

