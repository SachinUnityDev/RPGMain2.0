using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Combat;
using Common; 


namespace Quest
{
    public class QuestEventService : MonoSingletonGeneric<QuestEventService>
    {
        public event Action OnSOQ; 
        public event Action OnEOQ;

        public event Action<CharController> OnFleeInQuest;
        public event Action<CharController> OnDeathInQuest;

        // Quest => town it should reset the QuestCombatMode to NONE 
        LandscapeNames prevPartyLoc;
        public TimeState questTimeState;
       // public DayNightController dayNightController; Dep 


    // Start is called before the first frame update
        void Start()
        {            
            prevPartyLoc = LandscapeNames.None;
         // gameObject.AddComponent<Common.QuestController>();
         //   dayNightController = gameObject.GetComponent<DayNightController>();
        }

        //public void On_DeathInQuest(CharController charController)
        //{
        //    OnDeathInQuest?.Invoke(charController); 
        //}
        //public void On_FleeInQuest(CharController charController)
        //{
        //    OnFleeInQuest?.Invoke(charController);
        //}

        //public void On_PartySet(List<CharNames> allyPartyList)
        //{
        //    allyPartyList.ForEach(t => CharService.Instance.SpawnCompanions(t)); 
        //}

  
        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    List<CharNames> chars = new List<CharNames>() {CharNames.Rayyan, CharNames.Baran
            //       , CharNames.Cahyo};
            //    On_PartySet(chars);

            //}
        }

    }


}

