using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    [CreateAssetMenu(fileName = "AllTempTraitSO", menuName = "Common/AllTempTraitSO")]
    public class AllTempTraitSO : ScriptableObject
    {
        public List<TempTraitSO> allTempTraitsSO = new List<TempTraitSO>();


        public TempTraitSO GetTempTraitSO(TempTraitName tempTraitName)
        {
            int index = allTempTraitsSO.FindIndex(t=>t.tempTraitName== tempTraitName);
            if(index != -1)
            {
                return allTempTraitsSO[index];
            }
            else
            {
                Debug.Log("temp trait SO not found" + tempTraitName); 
                return null;
            }
        }
    }
}