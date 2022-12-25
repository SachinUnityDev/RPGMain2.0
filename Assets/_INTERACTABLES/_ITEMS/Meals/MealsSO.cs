using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    [CreateAssetMenu(fileName = "MealSO", menuName = "Item Service/MealSO")]
    public class MealsSO : ScriptableObject
    {
        public MealsNames mealName;
        public int maxInvStackSize; 
        public int maxWorldInstance;

        [Header("Sprites")]
        public Sprite iconSprite;
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();


        private void Awake()
        {
            maxInvStackSize = 1; 
            maxWorldInstance = 100;
        }


    }
    
}
