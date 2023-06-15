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
        public Action OnGameStart;
        public Action OnGameEnd;

        public Action<LocationName> OnTownEnter;
        public Action<LocationName> OnTownExit;

        public Action<GameState> OnGameStateChg; 
        public event Action<QuestMode> OnQuestModeChg;

        public Action<GameState> OnStateStart;
       
        void Start()
        {
            OnGameStateChg += OnQuestStart; 
        }
        
        public void On_TownEnter(LocationName locationName)
        {          
            CalendarService.Instance.Init();
            EncounterService.Instance.EncounterInit();
            CharService.Instance.Init();
            UIControlServiceGeneral.Instance.InitUIGeneral();
            MapService.Instance.InitMapService();   
            
            BestiaryService.Instance.Init();
            ItemService.Instance.Init();
            FameService.Instance.Init();
            LevelService.Instance.Init();
            TownService.Instance.Init(locationName);
            OnTownEnter?.Invoke(locationName);
            QuestMissionService.Instance.InitQuestMission();    
            LootService.Instance.InitLootService();
        }
        void OnQuestStart(GameState gameState)
        {
            if(gameState == GameState.InQuest)
            {
                CurioService.Instance.InitCurioService();
            }
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

