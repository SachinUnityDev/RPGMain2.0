using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{


    [CreateAssetMenu(fileName = "AllTrapMGSO", menuName = "MiniGame/AllTrapMGSO")]

    public class AllTrapMGSO : ScriptableObject
    {
        public List<TrapMGSO> allTrapMGSO = new List<TrapMGSO>();
        public List<TimeNFlashTimeData> timeNFlashTimeDatas = new List<TimeNFlashTimeData>();

        [Header("Default tile")]
        public Sprite defaultTL;

        [Header("Correct Hit")]
        public Sprite greyTL;

        [Header("Correct hit tile")]
        public Sprite correctHitTL;

        [Header("Mistake Hit")]
        public Sprite mistakeHitTL;

        public TrapMGSO GetTrapSO(GameDifficulty gameDifficulty)
        {
            int index  =  allTrapMGSO.FindIndex(t=>t.gameDifficulty == gameDifficulty);
            if(index != -1)
            {
                return allTrapMGSO[index];
            }
            Debug.Log("Trap MG SO Not found" + gameDifficulty.ToString());
            return null; 
        }
    }
}