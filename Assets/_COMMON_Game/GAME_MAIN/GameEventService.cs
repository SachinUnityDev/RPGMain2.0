﻿using System.Collections;
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
        public Action OnGameStart;
        public Action OnGameEnd;

        public Action<LocationName> OnTownEnter;
        public Action<LocationName> OnTownExit;

        public Action<GameState> OnGameStateChg; 
        public event Action<QuestMode> OnQuestModeChg;

        public Action<GameState> OnStateStart;
       
        void Start()
        {
            OnGameStateChg += On_QuestRoomStart;
            OnGameStateChg += (GameState gameState) => On_TownEnter(LocationName.Nekkisari, gameState); 
        }
        
        public void On_TownEnter(LocationName locationName, GameState gameState)
        {
            // Anything initialised by SO to be put here 
            // any initialisation during scene swap to be checked 
            // model init to be implemented after the save service connection 

            if (gameState != GameState.InTown)
                return; 
        
                
                CalendarService.Instance.Init();
                TownService.Instance.Init(locationName);
                BuildingIntService.Instance.InitBuildIntService();
                EncounterService.Instance.EncounterInit();
                CharService.Instance.Init();
                UIControlServiceGeneral.Instance.InitUIGeneral();
             
                EcoServices.Instance.InitEcoServices();
                DialogueService.Instance.InitDialogueService();
                BarkService.Instance.InitBarkService();
                JobService.Instance.JobServiceInit();
                BestiaryService.Instance.Init();
                ItemService.Instance.Init();
                TradeService.Instance.InitTradeService();
                FameService.Instance.Init();
                LevelService.Instance.Init();
                
                OnTownEnter?.Invoke(locationName);
                QuestMissionService.Instance.InitQuestMission();
                MapService.Instance.InitMapService(); // to be put below questmission
                LootService.Instance.InitLootService();
            if (WelcomeService.Instance.isQuickStart)
                WelcomeService.Instance.On_QuickStart();
            else
                WelcomeService.Instance.InitWelcome();

        }
        public void On_QuestRoomStart(GameState gameState)
        {
            if (gameState != GameState.InQuestRoom)
                return;
            CurioService.Instance.InitCurioService();
           
        }
        public void On_QuestRoomEnd()
        {

        }


        public void On_TownExit(LocationName locationName)
        {
            OnTownExit?.Invoke(locationName);
        }

        public void On_GameStart()
        {
            OnGameStart?.Invoke();
        }
        public void On_GameEnd()
        {
            OnGameEnd?.Invoke(); 
        }
    }


}

