using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    [CreateAssetMenu(fileName = "IngredSO", menuName = "Interactable/IngredSO")]
    public class IngredSO : ScriptableObject
    {
        public IngredNames ingredName;
        public int maxInvStackSize = 0;
        public Currency cost;
        public float fluctuation;
        public int maxWorldInstance;

        [Header("Sprites")]
        public Sprite iconSprite;
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();


        private void Awake()
        {
            fluctuation = 20;
            maxWorldInstance = 100;
        }
    }
}
