using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class CharStateModel
    {
        public int charStateID;

        public CharStateName charStateName;
        public CharStateBehavior statebehavior;
        public StateFor stateFor;
        public CharStateType stateType;
        public TimeFrame timeFrame;
        public int castTime; //-5 for NA
        public GameObject CharStateFX;

        public CharStateModel(CharStateSO1 charStateSO, int charStateID)
        {
            charStateName = charStateSO.charStateName;
            statebehavior = charStateSO.charStateBehavior;
            stateFor = charStateSO.stateFor;
            stateType = charStateSO.charStateType;
            
            this.charStateID = charStateID; 
        }
    }
}