using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    [CreateAssetMenu(fileName = "AllArmorSO", menuName = "Interactable/AllArmorSO")]
    public class AllArmorSO : ScriptableObject
    {
        public List<ArmorSO> allArmorSO = new List<ArmorSO>();

        public ArmorSO GetArmorSO(ArmorType armorType)
        {
            int index= allArmorSO.FindIndex(t=>t.armorType==armorType);
            if(index !=-1) 
            {
                return allArmorSO[index];
            }
            else
            {
                Debug.Log("armor SO not found" + armorType); 
                return null;
            }
        }

    }
}