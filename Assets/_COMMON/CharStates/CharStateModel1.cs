using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class CharStateModel1
    {
        [Header("char State Define")]
        public CharStateName charStateName;
        public StateFor stateFor;
        public CharStateType charStateType;
        public CharStateBehavior charStateBehavior;

        [Header("CastTime")]
        public TimeFrame timeFrame;
        public int minCastTime;
        public int maxCastTime;

        [Header("Description")]
        public string CharStateNameStr = "";

        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();

        public CharStateModel1(CharStateSO1 charStateSO)
        {
            charStateName = charStateSO.charStateName;
            stateFor = charStateSO.stateFor;
            charStateType = charStateSO.charStateType;
            charStateBehavior = charStateSO.charStateBehavior;
        }
    }
}