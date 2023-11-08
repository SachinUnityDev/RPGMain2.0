using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    [CreateAssetMenu(fileName = "GameDiffSO", menuName = "Common/GameDiffSO")]
    public class GameDiffSO : ScriptableObject
    {
        public GameDifficulty gameDiff;
        public int maxRoundLimit;
        public int maxRoundLimitBoss;
        public float partyLvlFactor;
        public List<FortIncOnEOQ> allFortIncPerEOQ = new List<FortIncOnEOQ>();  
        public List<CharStateNChances> allCharStateNChances = new List<CharStateNChances>();
        [Header("Sprites")]
        public Sprite spriteN; 

    }

    [Serializable]
    public class FortIncOnEOQ
    {
        public QuestMode questMode;
        public int val; 
    }
    [Serializable]
    public class CharStateNChances
    {
        public CharStateName charStateName;
        public int chance1; 
        public int chance2;
        public int chance3;
    }


}