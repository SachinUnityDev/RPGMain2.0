using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common;

namespace Interactables
{
    [CreateAssetMenu(fileName = "PotionSO", menuName = "Interactable/PotionSO")]

    public class PotionSO : ScriptableObject
    {
        public PotionNames potionName;
        public float potionAddict = 0f;
        public int maxInvStackSize;
        [Header("Cost Data")]
        public Currency cost; 
        public float fluctuation;

        public TimeFrame timeFrame;
        public int minCastTime;
        public int maxCastTime;

        public int maxWorldInstance;

        [Header("Description")]
        public string desc = ""; 

        [Header("Sprites")]
        public Sprite iconSprite; 
        [TextArea (5,10)]
        public List<string> allLines = new List<string>();

        private void Awake()
        {
         

        }

    }



}

