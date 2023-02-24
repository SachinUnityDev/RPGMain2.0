using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    [CreateAssetMenu(fileName = "CharStateSO1", menuName = "Common/CharStateSO1")]
    public class CharStateSO1 : ScriptableObject
    {
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

        [Header("Sprites")]
        public Sprite iconSprite;

        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();
        public GameObject CharStateFX;
    }
}