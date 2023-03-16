using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Interactables
{
    

    [CreateAssetMenu(fileName = "ArmorSO", menuName = "Interactable/ArmorSO")]

    public class ArmorSO : ScriptableObject
    {
        public ArmorType armorType;       
        public Currency fortifyCost;
        public string armorTypeStr = "";
        public int minArmor; 
        public int maxArmor;

        public int nosOfDays; 
        [TextArea(5,10)]
        public List<string> allLines=new List<string>();
    }



}