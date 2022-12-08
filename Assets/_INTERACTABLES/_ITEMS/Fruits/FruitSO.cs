using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    [CreateAssetMenu(fileName = "FruitSO", menuName = "Item Service/FruitSO")]

    public class FruitSO : ScriptableObject
    {
        public FruitNames fruitName;
        public int shelfLife;

        [Header("HP AND STAMINA REGEN")]
        public int hpRegen;
        public int staminaRegen;

        public TimeFrame timeFrameRegen;
        public int regenCastTime;

        [Header("HUNGER RANGE")]
        public int hungerReliefMin;
        public int hungerReliefMax;


        [Header("THIRST RANGE")]
        public int thirstReliefMin;
        public int thirstReliefMax;


        [Header("SICKNESS AND ITS WEIGHTS")]
        public TempTraitName tempTraitName;
        public int weightOfSickeness;

        [Header("ITEM VARIABLES")]
        public int inventoryStack = 0;
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
            timeFrameRegen = TimeFrame.EndOfRound;
            //string str = "";

            //if (hpRegen != 0 && staminaRegen != 0)
            //    str = $"{hpRegen} Hp Regen, {staminaRegen} Stamina Regen, {regenCastTime} rds";
            //else if (hpRegen != 0)
            //    str = $"{hpRegen} Hp Regen, {regenCastTime} rds";
            //else if (staminaRegen != 0)
            //    str = $"{staminaRegen} Stamina Regen, {regenCastTime} rds";

            //allLines.Add(str);
        }
    }
}
