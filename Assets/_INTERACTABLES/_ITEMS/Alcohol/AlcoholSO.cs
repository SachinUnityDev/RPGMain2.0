using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    [CreateAssetMenu(fileName = "AlcoholSO", menuName = "Item Service/AlcoholSO")]
    public class AlcoholSO : ScriptableObject
    {
        public AlcoholNames alcoholName;
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
        }
    }
}