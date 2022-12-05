using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    [CreateAssetMenu(fileName = "FoodSO", menuName = "Item Service/FoodSO")]
    public class FoodSO : ScriptableObject
    {
        public FoodNames foodName;
        public int shelfLife;
        
        [Header("HP RANGE")]
        public int hpHealMin; 
        public int hpHealMax;

        [Header("HUNGER RANGE")]
        public int hungerReliefMin; 
        public int hungerReliefMax;


        [Header("THIRST RANGE")]
        public int thirstReliefMin;
        public int thirstReliefMax;


        [Header("ITEM VARIABLES")]
        public int maxInvStackSize = 0;
        public Currency cost;
        public float fluctuation;
        public int maxWorldInstance;

        public TimeFrame timeFrame;
        public int castTime;
        

        [Header("Sprites")]
        public Sprite iconSprite;
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();

        private void Awake()
        {
            fluctuation = 20;
            maxWorldInstance = 100;
            maxInvStackSize = 1; 
        }

    }
}

