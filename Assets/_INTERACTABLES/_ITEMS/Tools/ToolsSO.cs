using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 


namespace Interactables
{
    [CreateAssetMenu(fileName = "ToolSO", menuName = "Interactable/ToolSO")]
    public class ToolsSO : ScriptableObject
    {
        public ToolNames toolName;    
        public int maxInvStackSize;
        public Currency cost;
        public float fluctuation;
        public int maxWorldInstance;
        [Header("Uses")]
        public int toolMaxUses =10; 
        [Header("Sprites")]
        public Sprite iconSprite;
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();
    }
}

