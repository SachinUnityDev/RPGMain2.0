using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class CharStateModel
    {
        public CharStateName charStateName;
        public Sprite charStateSprite;
        public CharStateBehavior statebehavior;
        public StateFor stateFor;
        public CharStateType stateType;
        public TimeFrame timeFrame;
        public int castTime; //-5 for NA
        public GameObject CharStateFX;

        [Header("NOT TO FILLED IN INSPECTOR")]
        public int timeRemaining;
        public int startRound = 0;

        public CharNames effectedChar;
        public int effectedCharID;

        public List<string> charStateCardStrs = new List<string>();
        public int charStateID;
    }
}