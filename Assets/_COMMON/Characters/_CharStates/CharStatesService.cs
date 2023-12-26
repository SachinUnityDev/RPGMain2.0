using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Combat;




namespace Common
{
    public class CharStatesService : MonoSingletonGeneric<CharStatesService>
    {
        // apply n remove char States....Start Here.. 
        // event and view control 
        // view control .. hover upon a Icon the scripts directs here with controller name and stateName
        // view controll logic easy access point 

        public event Action<CharStateModData> OnCharStateStart;  // events will drive the animations
        public event Action<CharStateModData> OnCharStateEnd;   
        public event Action<CharStateName, CharController> OnStateHovered; 


        public List<CharStateSO1> allCharStateSOs = new List<CharStateSO1>();

        public AllCharStateSO allCharStateSO; 
        public CharStatesFactory charStateFactory;

        public List<CharStateModel> allCharStateModel = new List<CharStateModel>(); // All char State In Use

        void Start()
        {
            charStateFactory = GetComponent<CharStatesFactory>();         
        }

        public void On_CharStateStart(CharStateModData charStateModData)
        {
            OnCharStateStart?.Invoke(charStateModData); 
        }

        public void On_CharStateEnd(CharStateModData charStateModData)
        {
            OnCharStateEnd?.Invoke(charStateModData);
        }

        public CharStatesBase GetNewCharState(CharStateName charStateName)
        {
            CharStatesBase charStateBase = charStateFactory.GetCharState(charStateName);
            // create a charState Model and Add to the list
            return charStateBase;   
        }

    }


}