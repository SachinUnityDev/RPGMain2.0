using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Town
{

    [CreateAssetMenu(fileName = "AllDaySO", menuName = "Calendar Service/AllDaySO")]

    public class AllDaySO : MonoBehaviour
    {
        public List<DaySO> allDaySO = new List<DaySO>();

        public DaySO GetDaySO(DayName dayName)
        {
            int index = allDaySO.FindIndex(t=>t.dayName == dayName);

            if(index != -1)
            {
                return allDaySO[index];
            }
            else
            {
                Debug.Log("Day SO not found" + dayName);
                return null; 
            }
        }
    }
}