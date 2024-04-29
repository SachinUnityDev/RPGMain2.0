using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    [CreateAssetMenu(fileName = "AllGameDiffSO", menuName = "Common/AllGameDiffSO")]
    public class AllGameDiffSO : ScriptableObject
    {
        public List<GameDiffSO> allGameDiffSO = new List<GameDiffSO>();

        public GameDiffSO GetGameDiffSO(GameDifficulty gameDifficulty)
        {
            int index = allGameDiffSO.FindIndex(t=>t.gameDiff  == gameDifficulty);   
            if(index != -1)
            {
                return allGameDiffSO[index];
            }
            return null; 
        }

    }
}