using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    [CreateAssetMenu(fileName = "AllWeekSO", menuName = "Calendar Service/AllWeekSO")]

    public class AllWeekSO : ScriptableObject
    {
        public List<WeekSO> allWeekSO = new List<WeekSO>();
        public WeekSO GetWeekSO(WeekEventsName weekName)
        {
            int index = allWeekSO.FindIndex(t=>t.weekName == weekName);
            if(index !=-1)
            {
                return allWeekSO[index]; 
            }
            Debug.Log("weel SO not found" + weekName);
            return null; 
        }
    }
}