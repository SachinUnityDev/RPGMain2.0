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
       public ScrollNames scrollName;
        [Header("Desc")]
        public string desc = "";
        public GemNames enchantmentGemName;

        public int maxInvStackSize = 0;
        public Currency cost;
        public float fluctuation;
        public int maxWorldInstance;

        public TimeFrame timeFrame;
        public int castTime;

        public int rechargeExpMin; 
        public int rechargeExpMax;

        [Header("Sprites")]
        public Sprite iconSprite;
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();


   
        private void Awake()
        {
            timeFrame = TimeFrame.EndOfDay;
            maxInvStackSize = 6;
            fluctuation = 30;
            maxWorldInstance = 12;
            rechargeExpMax = 10; 
            rechargeExpMin = 5;

        }
    }
}