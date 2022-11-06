using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 


namespace Common
{
    public class TownEventService : MonoSingletonGeneric<TownEventService>
    {

        public event Action OnQuestBegin;
        public event Action OnQuestEnd;
        public event Action<List<CharController>> OnCharsCreated;

        public event Action<List<CharController>> OnCharUnLocked;
        public GameObject charGOTEST; 

        // Start is called before the first frame update
        void Start()
        {

        }

        public void On_CharCreated(List<CharController> charList)
        {

            OnCharsCreated.Invoke(charList); 

        }
        public void On_CharAvailable(List<CharController> charList)
        {


        }

        void OnQuest_Begin()
        {
            OnQuestBegin.Invoke(); 

        }

        private void Update()
        {
            
        }


    }


}

