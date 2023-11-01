using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    [CreateAssetMenu(fileName = "AllCharStateSO1", menuName = "Common/AllCharStateSO1")]

    public class AllCharStateSO : ScriptableObject
    {
        public List<CharStateSO1> allCharStateSO = new List<CharStateSO1>();

        public CharStateSO1 GetCharStateSO(CharStateName charStateName)
        {
            int index = allCharStateSO.FindIndex(t=>t.charStateName == charStateName);
            if(index != -1)
            {
                return allCharStateSO[index];   
            }
            else
            {
                Debug.Log("charState SO not found" +charStateName); 
                return null; 
            }
        }
    }
}