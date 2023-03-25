using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Interactables
{

    [CreateAssetMenu(fileName = "AllArmorSO", menuName = "Interactable/AllArmorSO")]
    public class AllArmorSO : ScriptableObject
    {
        public List<ArmorSO> allArmorSO = new List<ArmorSO>();


        public ArmorSO GetArmorSOWithCharName(CharNames charName)
        {
            foreach (ArmorSO armorSO in allArmorSO)
            {
                if (armorSO.charNames.Any(t => t.Equals(charName)))
                {
                    return armorSO; 
                }
            }
            Debug.Log(" Armor SO not found" + charName); 
            return null;
        }

        public ArmorSO GetArmorSOWithType(ArmorType armorType)
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