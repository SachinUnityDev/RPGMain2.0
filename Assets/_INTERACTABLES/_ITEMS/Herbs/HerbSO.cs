using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Interactables
{
    [CreateAssetMenu(fileName = "HerbSO", menuName = "Interactable/HerbSO")]

    public class HerbSO : ScriptableObject
     {
        public HerbNames herbName;
        [Header("Desc")]
        public string desc = "";
        public int maxInvStackSize;
        public Currency cost;
        public float fluctuation;
        public int maxWorldInstance;
        
        [Header("Buff on Abzazulus Consumption")]
        public int HpRegenVal;
        public int bufftimeInRds; 



        [Header("Sprites")]
        public Sprite iconSprite;
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();

        private void Awake()
        {
          
        }

    }
}

