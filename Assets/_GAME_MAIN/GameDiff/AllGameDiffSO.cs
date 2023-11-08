using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    [CreateAssetMenu(fileName = "AllGameDiffSO", menuName = "Common/AllGameDiffSO")]
    public class AllGameDiffSO : ScriptableObject
    {
        public List<GameDiffSO> allGameDiffSO = new List<GameDiffSO>();
    }
}