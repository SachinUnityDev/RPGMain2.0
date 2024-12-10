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
        public event Action OnStartOfQuest; 
        public event Action OnEndOfQuest;

        public event Action<CharController> OnQuestFlee;
        public event Action<CharController> OnDeathInQuest;

        

        public void On_FleeInQuest(CharController charController)
        {
            OnQuestFlee?.Invoke(charController);
        }

        

    }


}

