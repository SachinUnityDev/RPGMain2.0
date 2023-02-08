using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using Combat;
using Interactables;
using Town;

namespace Common
{
    public class GameEventService : MonoSingletonGeneric<GameEventService>
    {
        public Action OnGameStart;
        public Action OnGameEnd;

        public Action OnIntroBegin;
        public Action OnIntroEnd; 
        public Action<LocationName> OnTownEnter;
        public Action<LocationName> OnTownExit;
        public Action OnQuestBegin;
        public Action OnQuestEnd;
        public Action OnCombatBegin;  
        public Action OnCombatEnd; 



        public Action<GameState> OnStateStart;
        public bool isGame = true;
        void Start()
        {

        }
        public void On_IntroBegin()
        {
            // sound Service.init 
            // Setting Service. init
            // save service 

            OnIntroBegin?.Invoke();
        }
        public void On_IntroEnd()
        {
            OnIntroEnd?.Invoke();
        }
        public void On_TownEnter(LocationName locationName)
        {


            CalendarService.Instance.Init();

            CharService.Instance.Init();

            BestiaryService.Instance.Init();
            ItemService.Instance.Init();
            FameService.Instance.Init();
            LevelService.Instance.Init();
            TownService.Instance.Init(locationName);
            OnTownEnter?.Invoke(locationName);

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
     
      
     
        public void On_QuestBegin()
        {
            OnQuestBegin?.Invoke(); 
        }
        public void On_QuestEnd()
        {
            OnQuestEnd?.Invoke(); 
        }
        public void On_CombatBegin()
        {
            OnCombatBegin?.Invoke(); 
        }
        public void On_CombatEnd()
        {
            OnCombatEnd?.Invoke();
        }

       


    }


}

