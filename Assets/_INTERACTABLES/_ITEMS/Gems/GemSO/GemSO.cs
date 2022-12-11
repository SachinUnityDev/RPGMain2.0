using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Interactables
{
    [CreateAssetMenu(fileName = "GemSO", menuName = "Item Service/GemSO")]
    public class GemSO : ScriptableObject
    {
        public GemNames gemName;
        public GemType gemType;
        public List<CharNames> allCharsThatCanBeEnchanted;         
        public int maxInvStackSize;
        public Currency cost;
        public float fluctuation;
        public int maxWorldInstance;
        //public int minSpecrange = 0;
        //public int maxSpecRange = 0; 

        [Header("Desc")]
        public string desc = "";     

        [Header("Sprites")]
        public Sprite iconSprite;
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();

        private void Awake()
        {
            maxInvStackSize = 2;
        }
        // min and max range is defined to modify the spec range 

    }
}

