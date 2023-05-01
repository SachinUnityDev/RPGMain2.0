using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [CreateAssetMenu(fileName = "AllCityESO", menuName = "Quest/AllCityESO")]

    public class AllCityESO : ScriptableObject
    {
        public List<CityEncounterSO> allCityESO = new List<CityEncounterSO>();  

        public CityEncounterSO GetCityEncounterSO(CityENames cityEncounterNames)
        {
            int index = allCityESO.FindIndex(t => t.cityEName == cityEncounterNames); 
            if(index !=-1)
            {
                return allCityESO[index];
            }
            else
            {
                Debug.Log(" city E not found" + cityEncounterNames);
                return null;
            }
        }
    }
}