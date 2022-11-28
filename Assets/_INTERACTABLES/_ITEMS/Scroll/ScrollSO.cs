using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    [CreateAssetMenu(fileName = "ScrollSO", menuName = "Item Service/ScrollSO")]

    public class ScrollSO : ScriptableObject
    {
        public ScrollName scrollName;
        public ScrollType scrollType;

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
            timeFrame = TimeFrame.EndOfDay;
            inventoryStack = 6;
            fluctuation = 30;
            maxWorldInstance = 12; 
        }
    }
}