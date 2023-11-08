using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    [System.Serializable]
    public class GameDiffModel
    {
        public GameDifficulty gameDiff;
        public int maxRoundLimit;
        public int maxRoundLimitBoss;
        public float partyLvlFactor;
        public List<FortIncOnEOQ> allFortIncPerEOQ = new List<FortIncOnEOQ>();
        public List<CharStateNChances> allCharStateNChances = new List<CharStateNChances>();

        public GameDiffModel(GameDiffSO gameDiffSO)
        {
            gameDiff = gameDiffSO.gameDiff;
            maxRoundLimit = gameDiffSO.maxRoundLimit;
            maxRoundLimitBoss = gameDiffSO.maxRoundLimitBoss;
            partyLvlFactor = gameDiffSO.partyLvlFactor;
            allFortIncPerEOQ = gameDiffSO.allFortIncPerEOQ;
            allCharStateNChances = gameDiffSO.allCharStateNChances;
        }
    }
}