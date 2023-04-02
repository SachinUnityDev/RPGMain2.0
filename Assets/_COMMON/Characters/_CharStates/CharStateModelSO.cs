using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 



namespace Common
{
    [CreateAssetMenu(fileName = "CharStatesIconSO", menuName = "Common/CharStatesIconSO")]
    public class CharStateModelSO : ScriptableObject
    {
        public List<CharStateModel> allCharStatesModels = new List<CharStateModel>();

        private void Awake()
        {
            if (allCharStatesModels.Count > 0) return; 
            for (int i = 1; i < Enum.GetNames(typeof(CharStateName)).Length; i++)    // 0 is none
            {
                CharStateModel charStatesData = new CharStateModel(); 
                charStatesData.charStateName = (CharStateName)i;
                allCharStatesModels.Add(charStatesData); 
            }
        }
    }
    [System.Serializable]
    public class CharStateModel
    {
        public CharStateName charStateName;
        public Sprite charStateSprite;
        public CharStateBehavior charStatebehavior;
        public StateFor stateFor;
        public CharStateType charStateClass;
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

