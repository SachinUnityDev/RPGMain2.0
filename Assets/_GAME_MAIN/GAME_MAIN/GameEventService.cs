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
   

        public Action<LocationName> OnTownEnter;
        public Action<LocationName> OnTownExit;

        public Action OnIntroLoaded;
        public Action OnIntroUnLoaded;
        public Action OnCombatEnter;
        public Action OnCombatExit;

        public Action<GameScene> OnGameSceneChg; 
        public event Action<QuestMode> OnQuestModeChg;

        public Action<GameScene> OnStateStart;

        [Header(" Game State chg")]
        public Action<GameState> OnGameStateChg;

        public void On_TownLoaded()
        {   
            if (GameService.Instance.gameState == GameState.OnNewGameStart)
            {
                TownService.Instance.Init();  // new game
                BuildingIntService.Instance.InitNGBuildIntService();
                EncounterService.Instance.EncounterInit();
                CalendarService.Instance.Init();

                CharService.Instance.Init();
                CharStatesService.Instance.Init();
                RosterService.Instance.Init();
                UIControlServiceGeneral.Instance.InitUIGeneral();

                EcoService.Instance.InitEcoServices();
                BarkService.Instance.InitBarkService();
                JobService.Instance.JobServiceInit();
                BestiaryService.Instance.Init();
                ItemService.Instance.Init();
                TradeService.Instance.InitTradeService();// new game 
                FameService.Instance.Init();
                LevelService.Instance.Init();
                WeaponService.Instance.Init();
                ArmorService.Instance.Init();

                OnTownEnter?.Invoke(LocationName.Nekkisari);
                QuestMissionService.Instance.InitQuestMission();
                LandscapeService.Instance.InitLandscape();
                MapService.Instance.Init(); // to be put below questmission
                LootService.Instance.InitLootService();
                if (GameService.Instance.gameController.isQuickStart)
                    WelcomeService.Instance.On_QuickStart();
                else
                    WelcomeService.Instance.InitWelcome();


                if (GameService.Instance.gameController.isQuickStart)
                {
                    CharService.Instance.GetAllyController(CharNames.Abbas).charModel.classType
                        = GameService.Instance.currGameModel.abbasClassType;
                    
                    // Set job NAME SELECT HERE 
                }         
               SkillService.Instance.Init();
                Set_GameState(GameState.OnNewGameStartComplete);
            }
            if(GameService.Instance.gameState == GameState.OnLoadGameStart)
            {
                TownService.Instance.LoadState();  // new game
                BuildingIntService.Instance.LoadState();
                EncounterService.Instance.LoadState();
                CalendarService.Instance.LoadState();

                CharService.Instance.LoadState();
                CharStatesService.Instance.LoadState();
                RosterService.Instance.LoadState();
                UIControlServiceGeneral.Instance.InitUIGeneral();

                EcoService.Instance.LoadState();
                BarkService.Instance.InitBarkService();
                JobService.Instance.JobServiceInit();
                BestiaryService.Instance.LoadState();
                ItemService.Instance.Init();
                TradeService.Instance.InitTradeService();// new game 
                FameService.Instance.LoadState();
                LevelService.Instance.Init();
                WeaponService.Instance.Init();
                ArmorService.Instance.LoadState();

                OnTownEnter?.Invoke(LocationName.Nekkisari);
                QuestMissionService.Instance.InitQuestMission();  // build a load state
                LandscapeService.Instance.InitLandscape();  // build a load State
                MapService.Instance.LoadState(); // To be Put below Quest Mission ..../// build a load state
                LootService.Instance.InitLootService();
                SkillService.Instance.LoadState(); 
                Set_GameState(GameState.OnLoadGameStartComplete);
            }
         
        }        
    
        public void On_CombatEnter()
        {
            OnCombatEnter?.Invoke();                    
        }
        public void On_CombatExit()
        {
            OnCombatExit?.Invoke();
        }
   
        public void On_TownExit(LocationName locationName)
        {
            OnTownExit?.Invoke(locationName);
        }
                
        public void On_CoreSceneLoaded()
        {
            
        }
        public void On_IntroLoaded()
        {
            GameService.Instance.Init();
            UIControlServiceGeneral.Instance.InitUIGeneral();
            OnIntroLoaded?.Invoke();
        }
        public void On_IntroUnLoaded()
        {  
            OnIntroUnLoaded?.Invoke();
        }

        #region GAME STATE RELATED
        public void Set_GameState(GameState gameState)
        {
            GameService.Instance.gameState = gameState;
            OnGameStateChg?.Invoke(gameState);  
            switch (gameState)
            {
                case GameState.OnIntroAnimStart:                   
                    break;
                case GameState.OnNewGameStart:
                    break;
                case GameState.OnNewGameStartComplete:
                    break;
                case GameState.OnLoadGameStart:
                    break;
                case GameState.OnLoadGameStartComplete:
                    break;
                case GameState.OnGameQuit:
                    break;
                default:
                    break;
            }
        }

        public void On_GameSceneChg(GameScene gameScene)
        {
           OnGameSceneChg?.Invoke(gameScene);
        }
    }
    #endregion

}

