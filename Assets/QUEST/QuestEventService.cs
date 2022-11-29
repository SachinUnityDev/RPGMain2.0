using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Combat; 

namespace Common
{
    public class QuestEventService : MonoSingletonGeneric<QuestEventService>
    {
        public event Action OnSOQ; 
        public event Action OnEOQ;
        public event Action <DayName> OnStartOfDay;// can broadcast day, week  and month too here
        public event Action <DayName> OnStartOfNight;
        public event Action<WeekName> OnStartOfTheWeek;
        public event Action<MonthName> OnStartOfTheMonth;


        public event Action<LandscapeNames> OnPartyLocChange;       
        public event Action<QuestCombatMode> OnQuestModeChange;
        public event Action<CharController> OnFleeInQuest;
        public event Action<CharController> OnDeathInQuest;
        public event Action<List<CharController>> OnPartySet; 

        // Quest => town it should reset the QuestCombatMode to NONE 
        LandscapeNames prevPartyLoc;
        public TimeState questTimeState;
       // public DayNightController dayNightController; Dep 


    // Start is called before the first frame update
        void Start()
        {            
            prevPartyLoc = LandscapeNames.None; 
            gameObject.AddComponent<QuestController>();
         //   dayNightController = gameObject.GetComponent<DayNightController>();
        }

        public void ChangePartyLoc(LandscapeNames _partyLoc)
        {
            OnPartyLocChange?.Invoke(_partyLoc);           
        } 

        public void ChangeQCMode(QuestCombatMode qcMode)
        {
            OnQuestModeChange?.Invoke(qcMode); 
        }
        public void On_DeathInQuest(CharController charController)
        {
            OnDeathInQuest?.Invoke(charController); 
        }
        public void On_FleeInQuest(CharController charController)
        {
            OnFleeInQuest?.Invoke(charController);
        }

        public void On_PartySet(List<CharNames> allyPartyList)
        {
            allyPartyList.ForEach(t => CharService.Instance.SpawnCompanions(t)); 
        }

        public void On_StartOfTheDay(DayName dayName)
        {
            OnStartOfDay?.Invoke(dayName); 
        }
        public void On_StartOfTheNight(DayName dayName)
        {
            OnStartOfNight?.Invoke(dayName);
        }

        public void On_StartOfTheWeek(WeekName weekName)
        {
            OnStartOfTheWeek?.Invoke(weekName); 
        }

        public void On_StartOfTheMonth(MonthName monthName)
        {
            OnStartOfTheMonth?.Invoke(monthName);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                List<CharNames> chars = new List<CharNames>() {CharNames.Rayyan, CharNames.Baran
                   , CharNames.Cahyo};
                On_PartySet(chars);

            }
        }

    }


}

