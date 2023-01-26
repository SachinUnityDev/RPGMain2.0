using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{

    [CreateAssetMenu(fileName = "SoupSO", menuName = "Item Service/SoupSO")]
    public class SoupSO : ScriptableObject
    {
        public SoupNames soupName;
        public int maxInvStackSize;
        public int maxWorldInstance;

        [Header("Sprites")]
        public Sprite iconSprite;
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();

        [Header("Recipe Info")]
        public RecipeType recipeType;
        public Sprite recipeSprite;
        public Currency recipePrice;
        private void Awake()
        {
            maxInvStackSize = 1;
            maxWorldInstance = 100;
            recipePrice = new Currency(0, 20); 
        }
    }
}