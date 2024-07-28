using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    [CreateAssetMenu(fileName = "AllMonthSO", menuName = "Calendar Service/AllMonthSO")]
    public class AllMonthSO : ScriptableObject
    {
        public List<MonthSO> allMonths = new List<MonthSO>();

        public MonthSO GetMonthSO(MonthName _monthName)
        {
           int index = allMonths.FindIndex(x => x.monthName == _monthName);
           if(index != -1)
           {
               return allMonths[index];
           }
            Debug.Log(" Month SO not found"+ _monthName); 
            return null; 
        }

    }
}