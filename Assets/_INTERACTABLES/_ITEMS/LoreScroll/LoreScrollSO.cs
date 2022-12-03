using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    [CreateAssetMenu(fileName = "LoreScrollSO", menuName = "Item Service/LoreScrollSO")]
    public class LoreScrollSO :ScriptableObject
    {
        public LoreScrollName LoreScrollName;        

        public int inventoryStack;
        public Currency cost;
        public float fluctuation;
        public int maxWorldInstance;

        public int expGainMin;
        public int expGainMax;

        [Header("Sprites")]
        public Sprite iconSprite;
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();

        private void Awake()
        {          
            inventoryStack = 6;
            fluctuation = 0;
            maxWorldInstance = 100;
        }
    }
}
